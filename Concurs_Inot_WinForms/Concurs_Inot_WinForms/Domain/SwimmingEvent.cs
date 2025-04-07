namespace Concurs_Inot_WinForms.Domain;

public class SwimmingEvent : Entity<long>
{
    public int Distance { get; set; }
    public string Style { get; set; }

    public int nrParticipants { get; set; }


    public SwimmingEvent(int distance, string style)
    {
        Distance = distance;
        this.Style = style;
    }
}