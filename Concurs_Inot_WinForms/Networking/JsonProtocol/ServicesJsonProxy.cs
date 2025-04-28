using Model.Domain;
using Networking.DTO;
using Service.Interfaces;
using Service.Utils;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;

namespace Networking.JsonProtocol
{
    public class ServicesJsonProxy : IServices
    {
        private readonly string _host;
        private readonly int _port;

        private IObserver _client;

        private StreamReader _input;
        private StreamWriter _output;
        private TcpClient _connection;

        private BlockingCollection<Response> _qresponses;
        private volatile bool _finished;
        private User _user;
        private static readonly object _servicesLock = new();

        private static readonly ILog logger = LogManager.GetLogger(typeof(ServicesJsonProxy));

        public ServicesJsonProxy(string host, int port)
        {
            _host = host;
            _port = port;
            _qresponses = new BlockingCollection<Response>();
        }

        private void InitializeConnection()
        {
            try
            {
                _connection = new TcpClient(_host, _port);
                var stream = _connection.GetStream();
                _output = new StreamWriter(stream) { AutoFlush = true };
                _input = new StreamReader(stream);
                _finished = false;
                StartReader();
            }
            catch (IOException e)
            {
                logger.Error(e);
                throw new ServiceException("Error initializing connection: " + e.Message);
            }
        }

        private void CloseConnection()
        {
            _finished = true;
            try
            {
                _input?.Close();
                _output?.Close();
                _connection?.Close();
                _client = null;
            }
            catch (IOException e)
            {
                logger.Error(e);
            }
        }

        private void SendRequest(Request request)
        {
            try
            {
                var reqLine = JsonSerializer.Serialize(request);
                _output.WriteLine(reqLine);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error sending object: " + e.Message);
            }
        }

        private Response ReadResponse()
        {
            try
            {
                return _qresponses.Take();
            }
            catch (Exception e)
            {
                throw new ServiceException("Error reading response: " + e.Message);
            }
        }

        private void StartReader()
        {
            Thread t = new Thread(new ReaderThread(this).Run);
            t.Start();
        }

        private void HandleUpdate(Response response)
        {
            logger.Info("Update callback triggered.");
            _client?.Update();
        }

        private bool IsUpdate(Response response) => response.Type == ResponseType.UPDATE;

        public bool login(string username, string password, IObserver client)
        {
            InitializeConnection();
            _user = new User(username, password);
            var req = JsonProtocolUtils.createLoginRequest(_user);
            SendRequest(req);

            var response = ReadResponse();
            if (response.Type == ResponseType.OK)
            {
                _client = client;
                return true;
            }
            if (response.Type == ResponseType.ERROR)
            {
                CloseConnection();
                throw new ServiceException(response.ErrorMessage);
            }
            return false;
        }

        public IEnumerable<SwimmingEvent> GetSwimmingEvents()
        {
            lock (_servicesLock)
            {
                var req = JsonProtocolUtils.createGetSwimmingEventsRequest(_user);
                SendRequest(req);

                var response = ReadResponse();
                if (response.Type == ResponseType.ERROR)
                    throw new ServiceException(response.ErrorMessage);
                var dtoArray = response.SwimmingEvents;
                return DTOUtils.getFromDTO(dtoArray);
            }
        }

        public IEnumerable<Participant> GetParticipantsForEvent(SwimmingEvent ev)
        {
            lock (_servicesLock)
            {
                var req = JsonProtocolUtils.createGetParticipantsByEventRequest(ev);
                SendRequest(req);

                var response = ReadResponse();
                if (response.Type == ResponseType.ERROR)
                    throw new ServiceException(response.ErrorMessage);

                return DTOUtils.getFromDTO(response.Participants);
            }
        }

        public IEnumerable<Participant> GetParticipantsForEvent(SwimmingEvent ev, string fullName)
        {
            lock (_servicesLock)
            {
                var req = JsonProtocolUtils.createGetParticipantsByEventRequest(ev, fullName);
                logger.Debug("aici intra");
                SendRequest(req);

                var response = ReadResponse();
                if (response.Type == ResponseType.ERROR)
                    throw new ServiceException(response.ErrorMessage);

                return DTOUtils.getFromDTO(response.Participants);
            }
        }

        public void addParticipant(string firstName, string lastName, int age, IEnumerable<SwimmingEvent> events)
        {
            lock (_servicesLock)
            {
                logger.Info("Add participant");
                var participant = new Participant(firstName, lastName, age);
                var eventArray = events.ToArray();

                var req = JsonProtocolUtils.createAddParticipantRequest(participant, eventArray);
                SendRequest(req);

                var response = ReadResponse();
                if (response.Type == ResponseType.ERROR)
                    throw new ServiceException(response.ErrorMessage);

                logger.Info("Participant added");
            }
        }

        public void logout(User user, IObserver client)
        {
            lock (_servicesLock)
            {
                var req = JsonProtocolUtils.createLogoutRequest(user);
                SendRequest(req);
                var response = ReadResponse();
                CloseConnection();

                if (response.Type == ResponseType.ERROR)
                    throw new ServiceException(response.ErrorMessage);
            }
        }

        private class ReaderThread
        {
            private readonly ServicesJsonProxy _proxy;

            public ReaderThread(ServicesJsonProxy proxy)
            {
                _proxy = proxy;
            }

            public void Run()
            {
                while (!_proxy._finished)
                {
                    try
                    {
                        var responseLine = _proxy._input.ReadLine();
                        if (string.IsNullOrEmpty(responseLine)) continue;

                        var response = JsonSerializer.Deserialize<Response>(responseLine);
                        
                        if (response == null) continue;
                        logger.Debug("Response received");
                        if (_proxy.IsUpdate(response))
                        {
                            _proxy.HandleUpdate(response);
                        }
                        else
                        {
                            _proxy._qresponses.Add(response);
                        }
                    }
                    catch (IOException e)
                    {
                        logger.Error("Reading error: " + e);
                    }
                }
            }
        }

        public IObserver GetClient() => _client;

        public void setClient(IObserver observer) => _client = observer;

    }
}
