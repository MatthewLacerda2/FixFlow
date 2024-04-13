using Server.Models.Utils;

namespace Server.Models.Appointments;

public class AppointmentLog
{
    public string Id { get; set; }
    public string ClientId { get; set; }

    public float Price { get; set; }
    public DateTime DateTime { get; set; }
    public string Observation { get; set; } = string.Empty;

    public string ScheduleId { get; set; } = string.Empty;
    public CompletedStatus Status { get; set; }

    public AppointmentLog(string _clientId, float _price)
    {
        Id = new Guid().ToString();
        ClientId = _clientId;
        Price = _price;
    }
}