namespace Concurs_Iot.Domain;

public class SwimmingEvent : Entity<long>
{
    private int Distance { get; set; }
    private string style { get; set; }


    public SwimmingEvent(int distance, string style)
    {
        Distance = distance;
        this.style = style;
    }
}