using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class CreateAptLog {

	/// <summary>
	/// The Id of the Customer who took the Appointment
	/// </summary>
	[Required]
	public string CustomerId { get; set; } = string.Empty;

	/// <summary>
	/// The Id of the Business who owns this Contact
	/// </summary>
	[Required]
	public string BusinessId { get; set; } = string.Empty;

	/// <summary>
	/// The Id of the Schedule that precedes this Log, if any
	/// </summary>
	public string? scheduleId { get; set; }

	/// <summary>
	/// The DateTime when the Log was registered
	/// </summary>
	public DateTime dateTime { get; set; } = DateTime.UtcNow;

	public float price { get; set; }

	public string? Service { get; set; }

	public string? description { get; set; }

	/// <summary>
	/// The Date when we expect the Customer to schedule another appointment.
	/// We are leaving as DateTime for simplicity but we only need the Date from this class
	/// </summary>
	public DateTime whenShouldCustomerComeBack { get; set; }

	public CreateAptLog() {
		whenShouldCustomerComeBack = DateTime.UtcNow.AddMonths(1);
	}

	public CreateAptLog(string customerId, string businessId, string? scheduleId, DateTime dateTime, float price, string? service, string? description, DateTime whenComeBack) {
		CustomerId = customerId;
		BusinessId = businessId;
		this.scheduleId = scheduleId;
		this.dateTime = dateTime;
		this.price = price;
		this.description = description;
		Service = service;
		whenShouldCustomerComeBack = whenComeBack;
	}
}
