using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Appointments;

public class AptSchedule
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
    /// The Id of the Reminder that precedes this Schedule, if applicable
    /// </summary>
    [Required]
    [ForeignKey(nameof(AptReminder))]
    public string? reminderId { get; set; }

    /// <summary>
    /// Navigation Property of the Reminder
    /// </summary>
    public AptReminder? reminder { get; set; }

    /// <summary>
    /// The Date to Contact the Client
    /// </summary>
    public DateTime dateTime { get; set; }

    public float price { get; set; }
    public string observation { get; set; } = string.Empty;

    public AptSchedule()
    {
        Id = Guid.NewGuid().ToString();
        ClientId = string.Empty;
        Client = null!;
        reminder = null!;
    }

    public AptSchedule(string clientId, DateTime _dateTime)
    {
        Id = Guid.NewGuid().ToString();
        ClientId = clientId;
        Client = null!;
        reminder = null!;
        dateTime = _dateTime;
    }
}