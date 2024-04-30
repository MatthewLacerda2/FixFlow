using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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
    /// The Id of the Schedule that precedes this Log, if any
    /// </summary>
    [ForeignKey(nameof(AptSchedule))]
    public string? scheduleId { get; set; }

    /// <summary>
    /// Navigation Property of the Schedule
    /// </summary>
    [JsonIgnore]
    public AptSchedule? schedule { get; set; }

    /// <summary>
    /// The DateTime when the Log was registered
    /// </summary>
    public DateTime dateTime { get; set; } = DateTime.Now;

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
        businessId = string.Empty;
        business = null!;
    }

    public AptLog(string _clientId, float _price)
    {
        Id = Guid.NewGuid().ToString();
        ClientId = _clientId;
        Client = null!;
        price = _price;
        businessId = string.Empty;
        business = null!;
    }
}