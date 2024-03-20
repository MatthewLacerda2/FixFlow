namespace Server.Models;

public class AppointmentLog {
    
    public Guid Id { get; set; }
    public string ClientId { get; set; } = string.Empty;
    public string AttendantId { get; set; } = string.Empty; //Who took the appointment
    
    public CompletedStatus Status { get; set; }
    public float Price { get; set; } = 30f;

    public Guid ScheduleId { get; set; }
    public TimeInterval Interval { get; set; } = new TimeInterval();
    public string Observation { get; set;} = string.Empty;
    public string Place { get; set;} = string.Empty;

    public AppointmentLog(string _clientId, string _AttendantId, CompletedStatus _status, float _price){
        ClientId = _clientId;
        AttendantId = _AttendantId;        
        Status = _status;
        Price = _price;
    }
}