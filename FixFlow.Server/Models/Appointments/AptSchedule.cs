using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class AptSchedule {

	[Required]
	public string Id { get; set; }

	/// <summary>
	/// The Id of the Client who made the Schedule
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
	/// The Id of the Contact that precedes this Schedule, if any
	/// </summary>
	[ForeignKey(nameof(AptContact))]
	public string? contactId { get; set; }

	/// <summary>
	/// The scheduled DateTime of the Appointment
	/// </summary>
	public DateTime dateTime { get; set; }

	public string? service { get; set; }

	public string? observation { get; set; }

	public float price { get; set; }

	public AptSchedule() : this(string.Empty, string.Empty, DateTime.Now, 0) { }

	public AptSchedule(string clientId, string businessId, DateTime dateTime, float price) {
		Id = Guid.NewGuid().ToString();
		ClientId = clientId;
		BusinessId = businessId;
		this.dateTime = dateTime;
		this.price = price;
		Client = null!;
	}
}
