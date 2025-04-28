using Model.Domain;
using Server.Interfaces;
using Service.Interfaces;
using Service.Utils;

namespace Server.Service
{
    public class MainServices : IServices
    {
        private IAuthService authService;
        private IMainService mainService;

        public MainServices(IAuthService authService, IMainService mainService)
        {
            this.authService = authService;
            this.mainService = mainService;
        }

        public void addParticipant(string firstName, string lastName, int age, IEnumerable<SwimmingEvent> swimmingEvents)
        {
            try
            {
                List<long> swimmingEventIds = swimmingEvents.Select(e => e.Id).ToList();
                mainService.addParticipant(firstName, lastName, age, swimmingEventIds);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error retrieving participants", ex);
            }
        }

        public IEnumerable<Participant> GetParticipantsForEvent(SwimmingEvent ev)
        {
            try
            {
                return mainService.GetParticipantsForEvent(ev.Id);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error retrieving participants", ex);
            }
        }

        public IEnumerable<Participant> GetParticipantsForEvent(SwimmingEvent ev, string fullname)
        {
            try
            {
                return mainService.GetParticipantsForEvent(ev.Id, fullname);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error retrieving participants", ex);
            }
        }

        public IEnumerable<SwimmingEvent> GetSwimmingEvents()
        {
            try
            {
                return mainService.GetSwimmingEvents();
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error retrieving swimming events", ex);
            }
        }

        public bool login(string username, string password, IObserver client)
        {
            if(authService.authentificate(username, password))
            {
                mainService.addClient(new User(username, password), client);
                return true;
            }
            return false;
        }

        public void logout(User user, IObserver client)
        {
            if(authService.authentificate(user.username, user.password))
            {
                IObserver localClient = mainService.removeClient(user);

                if (localClient == null)
                {
                    throw new ServiceException("User not authenticated");
                }
            }
        }

        public void setClient(IObserver client)
        {
            throw new NotImplementedException();
        }
    }
}
