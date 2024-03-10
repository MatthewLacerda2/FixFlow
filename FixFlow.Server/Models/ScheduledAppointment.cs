namespace Server.Models;

public class ScheduledAppointment {

    public Guid Id { get; set; }
    public string clientId { get; set; } = string.Empty;
    
    public string AttendantId { get; set; } = string.Empty;
    public string Secretary { get; set; } = string.Empty;
    public float expectedPrice { get; set; } = 30f;
    public DateTime dateTime { get; set; }
    public string observation { get; set;} = string.Empty;

}