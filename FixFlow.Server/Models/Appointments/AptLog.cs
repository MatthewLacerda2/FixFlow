using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class AptLog {

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
	public string BusinessId { get; set; }

	/// <summary>
	/// The Id of the Schedule that precedes this Log, if any
	/// </summary>
	[ForeignKey(nameof(AptSchedule))]
	public string? scheduleId { get; set; }

	/// <summary>
	/// The DateTime when the Log was registered
	/// </summary>
	public DateTime dateTime { get; set; } = DateTime.Now;

	public string? Service { get; set; }

	public float Price { get; set; }

	public string? description { get; set; }

	public AptLog() {
		Id = Guid.NewGuid().ToString();
		ClientId = string.Empty;
		BusinessId = string.Empty;
		Client = null!;
	}

	public AptLog(CreateAptLog newLog) {
		Id = Guid.NewGuid().ToString();
		ClientId = newLog.ClientId;
		BusinessId = newLog.BusinessId;
		scheduleId = newLog.scheduleId;
		Service = newLog.Service;

		this.dateTime = newLog.dateTime;
		this.Price = newLog.price;
		this.description = newLog.description;
		Client = null!;
	}
}
