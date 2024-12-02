using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class AptLog {

	[Required]
	public string Id { get; set; }

	[Required]
	[ForeignKey(nameof(Customer))]
	public string CustomerId { get; set; }

	/// <summary>
	/// Navigation Property of the Customer
	/// </summary>
	[JsonIgnore]
	public Customer Customer { get; set; }

	[Required]
	[ForeignKey(nameof(Business))]
	public string BusinessId { get; set; }

	[ForeignKey(nameof(AptSchedule))]
	public string? ScheduleId { get; set; }

	public DateTime dateTime { get; set; } = DateTime.UtcNow;

	public string? Service { get; set; }

	[Required]
	public float Price { get; set; }

	/// <summary>
	/// Anything about the Log that is worth noting
	/// </summary>
	public string? Description { get; set; }

	public AptLog() {
		Id = Guid.NewGuid().ToString();
		CustomerId = string.Empty;
		BusinessId = string.Empty;
		Customer = null!;
	}

	public AptLog(CreateAptLog newLog, string businessId) {
		Id = Guid.NewGuid().ToString();
		CustomerId = newLog.CustomerId;
		BusinessId = businessId;
		ScheduleId = newLog.ScheduleId;
		Service = newLog.Service;
		Description = newLog.Description;
		dateTime = newLog.dateTime;
		Price = newLog.Price;
		Customer = null!;
	}
}
