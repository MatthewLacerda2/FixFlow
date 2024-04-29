using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Appointments;

public class AptContact
{
    [Required]
    public string Id { get; set; }

    /// <summary>
    /// The Id of the Client to Contact
    /// </summary>
    [Required]
    [ForeignKey(nameof(Models.Client))]
    public string ClientId { get; set; }

    /// <summary>
    /// Navigation Property of the Client
    /// </summary>
    public Client Client { get; set; }

    /// <summary>
    /// The Id of the Log that precedes this Contact
    /// </summary>
    [Required]
    [ForeignKey(nameof(AptLog))]
    public string aptLogId { get; set; }

    /// <summary>
    /// Navigation Property of the Log
    /// </summary>
    public AptLog aptLog { get; set; }

    /// <summary>
    /// The Date to Contact the Client
    /// The Time is used because, chances are, there is a better Time of the day to contact the Client
    /// </summary>
    public DateTime dateTime { get; set; } = DateTime.Now;

    public AptContact()
    {
        Id = Guid.NewGuid().ToString();
        ClientId = string.Empty;
        Client = null!;
        aptLogId = string.Empty;
        aptLog = null!;
    }

    public AptContact(string _clientId, string _prevAppoint)
    {
        Id = Guid.NewGuid().ToString();
        ClientId = _clientId;
        Client = null!;
        aptLog = null!;
        aptLogId = _prevAppoint;
    }
}