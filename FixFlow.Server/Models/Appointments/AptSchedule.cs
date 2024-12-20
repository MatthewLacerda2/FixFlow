using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class AptSchedule {

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

	/// <summary>
	/// Was the Customer contacted to make this Schedule?
	/// </summary>
	public bool WasContacted { get; set; }

	public DateTime dateTime { get; set; }

	public string? Service { get; set; }

	public string? Description { get; set; }

	public int price { get; set; }

	public AptSchedule() : this(string.Empty, string.Empty, DateTime.UtcNow, 0, null, false) { }

	public AptSchedule(string clientId, string businessId, DateTime dateTime, int price, string? service, bool wasContacted) {
		Id = Guid.NewGuid().ToString();
		CustomerId = clientId;
		BusinessId = businessId;
		this.dateTime = dateTime;
		this.price = price;
		Service = service;
		Customer = null!;
		WasContacted = wasContacted;
	}

	public AptSchedule(CreateAptSchedule createSchedule, string businessId, bool wasContacted) {
		Id = Guid.NewGuid().ToString();
		BusinessId = businessId;
		CustomerId = createSchedule.CustomerId;
		dateTime = createSchedule.dateTime;
		Service = createSchedule.Service;
		price = createSchedule.Price;
		Description = createSchedule.Description;
		WasContacted = wasContacted;
		Customer = null!;
	}
}
