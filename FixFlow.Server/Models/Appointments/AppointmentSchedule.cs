namespace Server.Models.Appointments;

public class AppointmentSchedule
{
    public string Id { get; set; }
    public string ClientId { get; set; }

    public string reminderId { get; set; } = string.Empty;

    public float Price { get; set; }
    public DateTime DateTime { get; set; }
    public string Observation { get; set; } = string.Empty;

    public AppointmentSchedule(string clientId, DateTime _dateTime)
    {
        Id = new Guid().ToString();
        ClientId = clientId;
        DateTime = _dateTime;
    }
}