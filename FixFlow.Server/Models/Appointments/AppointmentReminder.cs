namespace Server.Models.Appointments;

public class AppointmentReminder
{
    public string Id { get; set; }
    public string ClientId { get; set; }

    public string previousAppointmentId { get; set; }

    public AppointmentReminder(string _clientId, string _prevAppoint)
    {
        Id = new Guid().ToString();
        ClientId = _clientId;
        previousAppointmentId = _prevAppoint;
    }
}