using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class CreateAptLog {

	/// <summary>
	/// The Id of the Customer who took the Appointment
	/// </summary>
	[Required]
	public string CustomerId { get; set; }

	/// <summary>
	/// The Id of the Schedule that precedes this Log, if any
	/// </summary>
	public string? ScheduleId { get; set; }

	/// <summary>
	/// The DateTime when the Log was registered
	/// </summary>
	public DateTime dateTime { get; set; }

	public string? Service { get; set; }

	public string? Description { get; set; }

	[Required]
	public float Price { get; set; }

	/// <summary>
	/// The Date when we expect the Customer to schedule another appointment.
	/// We are leaving as DateTime for simplicity but we only need the Date from this class
	/// </summary>
	public DateTime DateToComeback { get; set; }

	public CreateAptLog() : this(string.Empty, null, DateTime.UtcNow, 0, null, null, DateTime.Now.AddDays(90)) { }

	public CreateAptLog(string customerId, string? scheduleId, DateTime dateTime, float price, string? service, string? description, DateTime whenComeBack) {
		this.CustomerId = customerId;
		this.ScheduleId = scheduleId;
		this.dateTime = dateTime;
		this.Price = price;
		this.Description = description;
		Service = service;
		DateToComeback = whenComeBack;
	}
}
