namespace Concurs_Inot_WinForms.Domain;

public class Record : Entity<long>
{
    public Participant participant { get; set; }
    public SwimmingEvent swimmingEvent  { get; set; }

    public Record(Participant participant, SwimmingEvent swimmingEvent)
    {
        this.participant = participant;
        this.swimmingEvent = swimmingEvent;
    }
}