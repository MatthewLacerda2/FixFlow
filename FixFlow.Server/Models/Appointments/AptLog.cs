using System.ComponentModel.DataAnnotations;
namespace Server.Models.Appointments;

public class AptLog
{
    [Required]
    public string Id { get; set; }

    /// <summary>
    /// The Id of the Client who took the Appointment
    /// </summary>
    [Required]
    public string ClientId { get; set; }

    /// <summary>
    /// The Id of the Schedule that precedes the Log, if any
    /// </summary>
    public string ScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// The DateTime when the Log was created
    /// </summary>
    public DateTime DateTime { get; set; } = DateTime.Now;

    public float Price { get; set; }

    /// <summary>
    /// Special information about the Appointment, if applicable
    /// </summary>
    /// <value></value>
    public string Observation { get; set; } = string.Empty;

    public AptLog()
    {
        Id = Guid.NewGuid().ToString();
        ClientId = string.Empty;
    }

    public AptLog(string _clientId, float _price)
    {
        Id = Guid.NewGuid().ToString();
        ClientId = _clientId;
        Price = _price;
    }
}