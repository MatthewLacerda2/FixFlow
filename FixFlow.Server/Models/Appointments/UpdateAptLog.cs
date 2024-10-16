using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class UpdateAptLog {

	[Required]
	public string Id { get; set; }

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

	public string? Description { get; set; }

	public UpdateAptLog(string Id, string? scheduleId, DateTime dateTime, string? service, float price, string? description) {
		this.Id = Id;
		this.ScheduleId = scheduleId;
		this.dateTime = dateTime;
		this.Price = price;
		this.Service = service;
		this.Description = description;
	}
}
