namespace Concurs_Inot.Domain;

public class SwimmingEvent : Entity<long>
{
    public int Distance { get; set; }
    public string Style { get; set; }


    public SwimmingEvent(int distance, string style)
    {
        Distance = distance;
        this.Style = style;
    }
}