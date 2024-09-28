using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class CreateAptLog {

	/// <summary>
	/// The Id of the Client who took the Appointment
	/// </summary>
	[Required]
	public string ClientId { get; set; } = string.Empty;

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
	public DateTime dateTime { get; set; } = DateTime.Now;

	public float price { get; set; }

	public string? description { get; set; }

	public DateOnly whenShouldClientComeBack { get; set; }
}
