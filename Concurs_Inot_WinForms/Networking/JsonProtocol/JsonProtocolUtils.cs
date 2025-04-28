using Model.Domain;
using Networking.DTO;
using System;

namespace Networking.JsonProtocol
{
    public class JsonProtocolUtils
    {
        public static Response createOkResponse()
        {
            Response resp = new Response();
            resp.Type = ResponseType.OK;
            return resp;
        }

        public static Request createLoginRequest(User user)
        {
            Request req = new Request();
            req.type = RequestType.LOGIN;
            req.user = user;
            return req;
        }

        public static Response createErrorResponse(String errorMessage)
        {
            Response resp = new Response();
            resp.Type = ResponseType.ERROR;
            resp.ErrorMessage = errorMessage;
            return resp;
        }

        public static Request createLogoutRequest(User user)
        {
            Request req = new Request();
            req.type = RequestType.LOGOUT;
            req.user = user;
            return req;
        }

        public static Request createGetSwimmingEventsRequest(User user)
        {
            Request req = new Request();
            req.user = user;
            req.type = RequestType.GET_SWIMMING_EVENTS;
            return req;
        }

        public static Response createGetSwimmingEventsResponse(SwimmingEvent[] events)
        {
            Response resp = new Response();
            resp.SwimmingEvents = DTOUtils.getDTO(events);
            resp.Type = ResponseType.UPDATE_SWIMMING_EVENTS;
            return resp;
        }

        public static Request createGetParticipantsByEventRequest(SwimmingEvent swimmingEvent)
        {
            Request req = new Request();
            req.type = RequestType.GET_PARTICIPANTS_BY_EVENT;
            req.swimmingEvent = DTOUtils.getDTO(swimmingEvent);
            return req;
        }

        public static Request createGetParticipantsByEventRequest(SwimmingEvent swimmingEvent, String nameFilter)
        {
            Request req = new Request();
            req.type = RequestType.GET_PARTICIPANTS_BY_EVENT;
            req.swimmingEvent = DTOUtils.getDTO(swimmingEvent);
            req.nameFilter = nameFilter;
            return req;
        }

        public static Response createGetParticipantsByEventResponse(Participant[] participants)
        {
            Response resp = new Response();
            resp.Participants = DTOUtils.getDTO(participants);
            resp.Type = ResponseType.UPDATE_PARTICIPANTS;
            return resp;
        }

        public static Request createAddParticipantRequest(Participant participant, SwimmingEvent[] swimmingEvents)
        {
            Request req = new Request();
            req.type = RequestType.ADD_PARTICIPANT;
            req.swimmingEvents = DTOUtils.getDTO(swimmingEvents);
            req.participant = DTOUtils.getDTO(participant);
            return req;
        }

        public static Response createAddParticipantResponse()
        {
            Response resp = new Response();
            resp.Type = ResponseType.UPDATE_PARTICIPANTS;
            return resp;
        }

    }
}
