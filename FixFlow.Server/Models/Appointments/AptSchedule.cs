using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class AptSchedule {

	[Required]
	public string Id { get; set; }

	/// <summary>
	/// The Id of the Customer who made the Schedule
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

	public bool WasContacted { get; set; }

	/// <summary>
	/// The scheduled DateTime of the Appointment
	/// </summary>
	public DateTime dateTime { get; set; }

	public string? Service { get; set; }

	public string? observation { get; set; }

	public float Price { get; set; }

	public AptSchedule() : this(string.Empty, string.Empty, DateTime.UtcNow, 0, null) { }

	public AptSchedule(string clientId, string businessId, DateTime dateTime, float price, string? service) {
		Id = Guid.NewGuid().ToString();
		CustomerId = clientId;
		BusinessId = businessId;
		this.dateTime = dateTime;
		this.Price = price;
		this.Service = service;
		Customer = null!;
	}

	public AptSchedule(CreateAptSchedule createSchedule, string businessId, bool wasContacted) {
		Id = Guid.NewGuid().ToString();
		BusinessId = businessId;
		CustomerId = createSchedule.customerId;
		this.dateTime = createSchedule.dateTime;
		this.Service = createSchedule.Service;
		this.Price = createSchedule.Price;
		WasContacted = wasContacted;
		Customer = null!;
	}
}
