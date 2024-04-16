using System.ComponentModel.DataAnnotations;
using Server.Models.Utils;
namespace Server.Models.Appointments;

public class AptLog
{
    public string Id { get; set; }

    [Required]
    public string ClientId { get; set; }

    public string ScheduleId { get; set; } = string.Empty;
    public CompletedStatus Status { get; set; }

    public DateTime DateTime { get; set; }
    public float Price { get; set; }
    public string Observation { get; set; } = string.Empty;

    public AptLog(string _clientId, float _price)
    {
        Id = new Guid().ToString();
        ClientId = _clientId;
        Price = _price;
    }
}