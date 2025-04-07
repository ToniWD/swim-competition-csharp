using Concurs_Inot_WinForms.Domain;
using Concurs_Inot_WinForms.Repository;
using Concurs_Inot_WinForms.Repository.Interfaces;
using Concurs_Inot_WinForms.Service.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Concurs_Inot_WinForms.Service
{
    public class MainService : IMainService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainService));
        private ParticipantsRepository participantsRepo;
        private SwimmingEventsRepository eventsRepo;
        private RecordsRepository recordsRepo;

        public MainService(ParticipantsRepository participantsRepository,SwimmingEventsRepository eventsRepository, RecordsRepository recordsRepository)
        {
            this.participantsRepo = participantsRepository;
            this.eventsRepo = eventsRepository;
            this.recordsRepo = recordsRepository;
        }

        public void addParticipant(string firstName, string lastName, int age, IEnumerable<long> swimmingEvents)
        {
            log.Info("Adding participant");
            Participant participant = new Participant(firstName, lastName, age);

            participantsRepo.Save(participant);
            recordsRepo.SaveRecordsForParticipant(participant.Id, swimmingEvents);
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
    }
}
