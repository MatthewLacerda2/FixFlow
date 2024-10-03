using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class CreateAptSchedule {

	/// <summary>
	/// The Id of the Customer who made the Schedule
	/// </summary>
	[Required]
	[ForeignKey(nameof(Customer))]
	public string customerId { get; set; }

	/// <summary>
	/// The scheduled DateTime of the Appointment
	/// </summary>
	public DateTime dateTime { get; set; }

	public string? Service { get; set; }

	public string? Observation { get; set; }

	public float Price { get; set; }

	public CreateAptSchedule() : this(string.Empty, DateTime.Now, 0, null, null) { }

	public CreateAptSchedule(string clientId, DateTime _dateTime, float price, string? service, string? observation) {
		customerId = clientId;
		dateTime = _dateTime;
		Price = price;
		Service = service;
		Observation = observation;
	}
}
