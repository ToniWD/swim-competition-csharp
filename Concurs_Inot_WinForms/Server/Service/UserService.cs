using Model.Domain;
using Persistence.Repository.Interfaces;
using Server.Interfaces;
using Service.Interfaces;
using Service.Utils;
using log4net;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Server.Service
{
    public class UserService : IMainService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserService));
        private ParticipantsRepository participantsRepo;
        private SwimmingEventsRepository eventsRepo;
        private RecordsRepository recordsRepo;

        private ConcurrentDictionary<string, IObserver> loggedClients = new ConcurrentDictionary<string, IObserver>();

        public UserService(ParticipantsRepository participantsRepository,SwimmingEventsRepository eventsRepository, RecordsRepository recordsRepository)
        {
            participantsRepo = participantsRepository;
            eventsRepo = eventsRepository;
            recordsRepo = recordsRepository;
        }

        public void addClient(User user, IObserver client)
        {
            if(loggedClients.TryAdd(user.username, client))
            {
                log.Info($"Client {user.username} added");
            }
            else if(loggedClients.TryUpdate(user.username, client, loggedClients[user.username]))
            {
                log.Info($"Client {user.username} updated");
            }
            else
            {
                log.Error($"Failed to add/update client {user.username}");
                throw new ServiceException("Failed to add client");
            }
        }

        public void addParticipant(string firstName, string lastName, int age, IEnumerable<long> swimmingEvents)
        {
            log.Info("Adding participant");
            Participant participant = new Participant(firstName, lastName, age);

            participantsRepo.Save(participant);
            recordsRepo.SaveRecordsForParticipant(participant.Id, swimmingEvents);
            notifyClients();
        }

        private void notifyClients()
        {
            Task.Run(() =>
            {
                log.Info("Notifying clients");
                foreach (var client in loggedClients.Values)
                {
                    try
                    {
                        client.Update();
                    }
                    catch (Exception e)
                    {
                        log.Error("Error notifying client", e);
                    }
                }
            });
        }

        public IEnumerable<Participant> GetParticipantsForEvent(long eventId)
        {
            return participantsRepo.GetParticipantsForEvent(eventId);
        }

        public IEnumerable<Participant> GetParticipantsForEvent(long eventId, string fullName)
        {
            fullName = Regex.Replace(fullName.Trim(), @"\s+", " ");
            return participantsRepo.GetParticipantsForEvent(eventId, fullName);
        }

        public IEnumerable<SwimmingEvent> GetSwimmingEvents()
        {
            return eventsRepo.FindAll();
        }

        public IObserver removeClient(User user)
        {
            return loggedClients.TryRemove(user.username, out IObserver client) ? client : null;
        }
    }
}
