namespace Server.Models;

public class AppointmentSchedule {
    //Remember, anyone can schedule an appointment
    public Guid Id { get; set; }
    public string ClientId { get; set; } = string.Empty;
    public string AttendantId { get; set; } = string.Empty;
    public string SecretaryId { get; set; } = string.Empty;

    //These are just expected, not mandatory to be kept
    public float ExpectedPrice { get; set; } = 30f;
    public DateTime DateTime { get; set; }
    public string Observation { get; set; } = string.Empty;

    public AppointmentSchedule(string clientId){
        ClientId = clientId;
    }
}