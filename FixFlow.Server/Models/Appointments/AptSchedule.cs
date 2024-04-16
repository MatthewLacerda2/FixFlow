using System.ComponentModel.DataAnnotations;

namespace Server.Models.Appointments;

public class AptSchedule
{
    public string Id { get; set; }

    [Required]
    public string ClientId { get; set; }

    public string reminderId { get; set; } = string.Empty;

    public DateTime DateTime { get; set; }
    public float Price { get; set; }
    public string Observation { get; set; } = string.Empty;

    public AptSchedule(string clientId, DateTime _dateTime)
    {
        Id = new Guid().ToString();
        ClientId = clientId;
        DateTime = _dateTime;
    }
}