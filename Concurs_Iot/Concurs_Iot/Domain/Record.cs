namespace Concurs_Iot.Domain;

public class Record : Entity<long>
{
    private Participant participant { get; set; }
    private SwimmingEvent swimmingEvent  { get; set; }

    public Record(Participant participant, SwimmingEvent swimmingEvent)
    {
        this.participant = participant;
        this.swimmingEvent = swimmingEvent;
    }
}