using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Server.Models.Appointments;

public class AptLog
{
    [Required]
    public string Id { get; set; }

    /// <summary>
    /// The Id of the Client who took the Appointment
    /// </summary>
    [Required]
    [ForeignKey(nameof(Models.Client))]
    public string ClientId { get; set; }

    /// <summary>
    /// Navigation Property of the Client
    /// </summary>
    public Client Client { get; set; }

    /// <summary>
    /// The Id of the Schedule that precedes the Log, if any
    /// </summary>
    [Required]
    [ForeignKey(nameof(AptSchedule))]
    public string? scheduleId { get; set; }

    public AptSchedule? schedule { get; set; }

    /// <summary>
    /// The DateTime when the Log was created
    /// </summary>
    public DateTime DateTime { get; set; } = DateTime.Now;

    public float price { get; set; }

    /// <summary>
    /// Special information about the Appointment, if applicable
    /// </summary>
    /// <value></value>
    public string observation { get; set; } = string.Empty;

    public AptLog()
    {
        Id = Guid.NewGuid().ToString();
        ClientId = string.Empty;
        Client = null!;
    }

    public AptLog(string _clientId, float _price)
    {
        Id = Guid.NewGuid().ToString();
        ClientId = _clientId;
        Client = null!;
        price = _price;
    }
}