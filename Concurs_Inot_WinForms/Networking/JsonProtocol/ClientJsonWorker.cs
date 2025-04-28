using Networking.DTO;
using Service.Interfaces;
using Service.Utils;
using log4net;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;

namespace Networking.JsonProtocol
{
    public class ClientJsonWorker : IObserver
    {
        private readonly IServices _services;
        private readonly TcpClient _connection;
        private StreamReader _input;
        private StreamWriter _output;
        private volatile bool _connected;

        private static readonly ILog logger = LogManager.GetLogger(typeof(ClientJsonWorker));

        public ClientJsonWorker(IServices services, TcpClient connection)
        {
            _services = services;
            _connection = connection;

            try
            {
                var stream = connection.GetStream();
                _output = new StreamWriter(stream) { AutoFlush = true };
                _input = new StreamReader(stream);
                _connected = true;
            }
            catch (IOException e)
            {
                logger.Error(e);
            }
        }

        public void Run()
        {
            while (_connected)
            {
                try
                {
                    string requestLine = _input.ReadLine();
                    if (string.IsNullOrEmpty(requestLine)) continue;

                    var request = JsonSerializer.Deserialize<Request>(requestLine);
                    var response = HandleRequest(request);

                    if (response != null)
                        SendResponse(response);
                }
                catch (IOException e)
                {
                    logger.Error(e);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (ThreadInterruptedException e)
                {
                    logger.Error(e);
                }
            }

            try
            {
                _input.Close();
                _output.Close();
                _connection.Close();
            }
            catch (IOException e)
            {
                logger.Error(e);
            }
        }

        private static readonly Response OkResponse = JsonProtocolUtils.createOkResponse();

        private Response HandleRequest(Request request)
        {
            Response response = null;

            if (request.type == RequestType.LOGIN)
            {
                logger.Debug($"Login request ... {request.user?.username}");
                try
                {
                    if (_services.login(request.user.username, request.user.password, this))
                        return OkResponse;
                    else
                        throw new ServiceException("Login failed");
                }
                catch (ServiceException e)
                {
                    _connected = false;
                    return JsonProtocolUtils.createErrorResponse(e.Message);
                }
            }

            if (request.type == RequestType.LOGOUT)
            {
                logger.Debug($"Logout request ... {request.user?.username}");
                try
                {
                    _services.logout(request.user, this);
                    _connected = false;
                    return OkResponse;
                }
                catch (ServiceException e)
                {
                    return JsonProtocolUtils.createErrorResponse(e.Message);
                }
            }

            if (request.type == RequestType.GET_SWIMMING_EVENTS)
            {
                logger.Debug("Get Swimming events request");
                try
                {
                    var events = _services.GetSwimmingEvents().ToArray();
                    return JsonProtocolUtils.createGetSwimmingEventsResponse(events);
                }
                catch (ServiceException e)
                {
                    return JsonProtocolUtils.createErrorResponse(e.Message);
                }
            }

            if (request.type == RequestType.GET_PARTICIPANTS_BY_EVENT)
            {
                logger.Debug("Get Participants request");
                var eventModel = DTOUtils.getFromDTO(request.swimmingEvent);
                string filter = request.nameFilter;

                try
                {
                    var participants = string.IsNullOrEmpty(filter)
                        ? _services.GetParticipantsForEvent(eventModel)
                        : _services.GetParticipantsForEvent(eventModel, filter);

                    return JsonProtocolUtils.createGetParticipantsByEventResponse(participants.ToArray());
                }
                catch (ServiceException e)
                {
                    return JsonProtocolUtils.createErrorResponse(e.Message);
                }
            }

            if (request.type == RequestType.ADD_PARTICIPANT)
            {
                logger.Debug("Add participant request");
                var participant = DTOUtils.getFromDTO(request.participant);
                var events = DTOUtils.getFromDTO(request.swimmingEvents);

                try
                {
                    _services.addParticipant(participant.FirstName, participant.LastName, participant.Age, events.ToList());
                    return JsonProtocolUtils.createAddParticipantResponse();
                }
                catch (ServiceException e)
                {
                    return JsonProtocolUtils.createErrorResponse(e.Message);
                }
            }

            return response;
        }

        private void SendResponse(Response response)
        {
            var json = JsonSerializer.Serialize(response);
            logger.Debug($"Sending response: {response.Type}");

            lock (_output)
            {
                _output.WriteLine(json);
            }
        }

        public void Update()
        {
            logger.Info("Update response for users");
            var response = new Response { Type = ResponseType.UPDATE };
            try
            {
                SendResponse(response);
            }
            catch (IOException e)
            {
                logger.Error(e);
            }
        }
    }
}
