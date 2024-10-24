using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class AptLog {

	[Required]
	public string Id { get; set; }

	/// <summary>
	/// The Id of the Customer who took the Appointment
	/// </summary>
	[Required]
	[ForeignKey(nameof(Models.Customer))]
	public string CustomerId { get; set; }

	/// <summary>
	/// Navigation Property of the Customer
	/// </summary>
	[JsonIgnore]
	public Customer Customer { get; set; }

	/// <summary>
	/// The Id of the Business who owns this Contact
	/// </summary>
	[Required]
	[ForeignKey(nameof(Business))]
	public string BusinessId { get; set; }

	/// <summary>
	/// The Id of the Schedule that precedes this Log, if any
	/// </summary>
	[ForeignKey(nameof(AptSchedule))]
	public string? ScheduleId { get; set; }

	/// <summary>
	/// The DateTime when the Log was registered
	/// </summary>
	public DateTime dateTime { get; set; } = DateTime.UtcNow;

	public string? Service { get; set; }

	public float Price { get; set; }

	public string? description { get; set; }

	public AptLog() {
		Id = Guid.NewGuid().ToString();
		CustomerId = string.Empty;
		BusinessId = string.Empty;
		Customer = null!;
	}

	public AptLog(CreateAptLog newLog) {
		Id = Guid.NewGuid().ToString();
		CustomerId = newLog.CustomerId;
		BusinessId = newLog.BusinessId;
		ScheduleId = newLog.scheduleId;
		Service = newLog.Service;
		description = newLog.description;

		this.dateTime = newLog.dateTime;
		this.Price = newLog.price;
		Customer = null!;
	}
}
