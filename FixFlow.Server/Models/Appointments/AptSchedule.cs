using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Server.Models.Appointments;

public class AptSchedule
{
    [Required]
    public string Id { get; set; }

    /// <summary>
    /// The Id of the Client who took the Appointment
    /// </summary>
    [Required]
    [ForeignKey("Client")]
    public string ClientId { get; set; }

    /// <summary>
    /// The Id of the Reminder that precedes this Schedule, if applicable
    /// </summary>
    public string? reminderId { get; set; }

    /// <summary>
    /// The Date to Contact the Client
    /// </summary>
    public DateTime DateTime { get; set; }

    public float Price { get; set; }
    public string Observation { get; set; } = string.Empty;

    public AptSchedule()
    {
        Id = Guid.NewGuid().ToString();
        ClientId = string.Empty;
    }

    public AptSchedule(string clientId, DateTime _dateTime)
    {
        Id = Guid.NewGuid().ToString();
        ClientId = clientId;
        DateTime = _dateTime;
    }
}