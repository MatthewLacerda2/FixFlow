using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class CreateAptLog {

	[Required]
	public string CustomerId { get; set; }

	public string? ScheduleId { get; set; }

	public DateTime dateTime { get; set; }

	public string? Service { get; set; }

	public string? Description { get; set; }

	[Required]
	public int Price { get; set; }

	/// <summary>
	/// The Date when we expect the Customer to schedule another appointment.
	/// We are leaving as DateTime for simplicity but we only need the Date from this class
	/// </summary>
	public DateTime DateToComeback { get; set; }

	public CreateAptLog() : this(string.Empty, null, DateTime.UtcNow, 0, null, null, DateTime.Now.AddDays(90)) { }

	public CreateAptLog(string customerId, string? scheduleId, DateTime dateTime, int price, string? service, string? description, DateTime whenComeBack) {
		CustomerId = customerId;
		ScheduleId = scheduleId;
		this.dateTime = dateTime;
		Price = price;
		Description = description;
		Service = service;
		DateToComeback = whenComeBack;
	}
}
