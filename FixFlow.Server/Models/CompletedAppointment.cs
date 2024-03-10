namespace Server.Models;

public class CompletedAppointment {

    public Guid Id { get; set; }
    public Guid ScheduleId { get; set; }
    public string AttendantId { get; set; } = string.Empty;
    public string clientId { get; set; } = string.Empty;
    
    public TimeInterval interval { get; set; } = new TimeInterval();
    public CompletedStatus status { get; set; }

    public float price { get; set; } = 30f;    
    public string observation { get; set;} = string.Empty;
    public string location { get; set;} = string.Empty;

    public CompletedAppointment(string _AttendantId, string _clientId, CompletedStatus _status, float _price){
        AttendantId = _AttendantId;
        clientId = _clientId;
        status = _status;
        price = _price;
    }
}