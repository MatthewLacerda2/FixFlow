namespace Server.Models;

public class ScheduledAppointment {
    //Remember, anyone can schedule an appointment
    public Guid Id { get; set; }
    public string clientId { get; set; } = string.Empty;
    
    public string AttendantId { get; set; } = string.Empty;
    public string SecretaryId { get; set; } = string.Empty;

    //These are just expected, not mandatory to be kept
    public float expectedPrice { get; set; } = 30f;
    public DateTime dateTime { get; set; }
    public string observation { get; set;} = string.Empty;

    public ScheduledAppointment(string _clientId){
        clientId = _clientId;
    }
}