using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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
    public string clientId { get; set; }

    /// <summary>
    /// Navigation Property of the Client
    /// </summary>
    [JsonIgnore]
    public Client Client { get; set; }

    /// <summary>
    /// The Id of the Business who owns this Contact
    /// </summary>
    [Required]
    [ForeignKey(nameof(Models.Business))]
    public string businessId { get; set; }

    /// <summary>
    /// Navigation Property of the Business
    /// </summary>
    [JsonIgnore]
    public Business Business { get; set; }

    /// <summary>
    /// The Id of the Log that precedes this Contact
    /// </summary>
    [Required]
    [ForeignKey(nameof(AptLog))]
    public string aptLogId { get; set; }

    /// <summary>
    /// Navigation Property of the Log
    /// </summary>
    [JsonIgnore]
    public AptLog aptLog { get; set; }

    /// <summary>
    /// The Date to Contact the Client
    /// The Time is used because, chances are, there is a better Time of the day to contact the Client
    /// </summary>
    public DateTime dateTime { get; set; } = DateTime.Now;

    public AptContact()
    {
        Id = Guid.NewGuid().ToString();
        clientId = string.Empty;
        Client = null!;
        aptLogId = string.Empty;
        aptLog = null!;
        businessId = string.Empty;
        Business = null!;
    }

    public AptContact(string _clientId, string _businessId, string _prevAppoint, DateTime _dateTime)
    {
        Id = Guid.NewGuid().ToString();
        clientId = _clientId;
        businessId = _businessId;
        aptLogId = _prevAppoint;
        Client = null!;
        Business = null!;
        aptLog = null!;
        dateTime = _dateTime;
    }
}