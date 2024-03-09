namespace Models;

public class CompletedAppointment {

    public Guid Id { get; set; }
    public Guid ScheduleId { get; set; }
    public string AttendantId { get; set; } = string.Empty;
    public string clientId { get; set; } = string.Empty;
    
    public TimeSpan timeSpan { get; set; }
    public CompletedStatus status { get; set; }

    public float price { get; set; } = 30f;    
    public string observation { get; set;} = string.Empty;
    public string location { get; set;} = string.Empty;

}