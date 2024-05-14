using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class AptSchedule
{
    [Required]
    public string Id { get; set; }

    /// <summary>
    /// The Id of the Client who made the Schedule
    /// </summary>
    [Required]
    [ForeignKey(nameof(Models.Client))]
    public string ClientId { get; set; }

    /// <summary>
    /// Navigation Property of the Client
    /// </summary>
    [JsonIgnore]
    public Client Client { get; set; }

    /// <summary>
    /// The Id of the Business who owns this Contact
    /// </summary>
    [Required]
    [ForeignKey(nameof(Business))]
    public string businessId { get; set; }

    /// <summary>
    /// Navigation Property of the Business
    /// </summary>
    [JsonIgnore]
    public Business business { get; set; }

    /// <summary>
    /// The Id of the Contact that precedes this Schedule, if any
    /// </summary>
    [ForeignKey(nameof(AptContact))]
    public string? contactId { get; set; }

    /// <summary>
    /// Navigation Property of the Contact
    /// </summary>
    [JsonIgnore]
    public AptContact? contact { get; set; }

    /// <summary>
    /// The scheduled DateTime of the Appointment
    /// </summary>
    public DateTime dateTime { get; set; }

    public float? price { get; set; }
    public string? observation { get; set; }

    public AptSchedule()
    {
        Id = Guid.NewGuid().ToString();
        ClientId = string.Empty;
        Client = null!;
        contact = null!;
        businessId = string.Empty;
        business = null!;
    }

    public AptSchedule(string clientId, DateTime _dateTime)
    {
        Id = Guid.NewGuid().ToString();
        ClientId = clientId;
        Client = null!;
        contact = null!;
        dateTime = _dateTime;
        businessId = string.Empty;
        business = null!;
    }
}