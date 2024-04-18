using System.ComponentModel.DataAnnotations;

namespace Server.Models.Appointments;

public class AptReminder
{
    [Required]
    public string Id { get; set; }

    /// <summary>
    /// The Id of the Client who took the Appointment
    /// </summary>
    [Required]
    public string ClientId { get; set; }

    /// <summary>
    /// The Id of the Appointment Log that precedes this Reminder
    /// </summary>
    [Required]
    public string previousAppointmentId { get; set; }

    /// <summary>
    /// The Date to Contact the Client
    /// </summary>
    public DateTime dateTime { get; set; } = DateTime.Now;

    public AptReminder()
    {
        Id = string.Empty;
        ClientId = string.Empty;
        previousAppointmentId = string.Empty;
    }

    public AptReminder(string _clientId, string _prevAppoint)
    {
        Id = new Guid().ToString();
        ClientId = _clientId;
        previousAppointmentId = _prevAppoint;
    }
}