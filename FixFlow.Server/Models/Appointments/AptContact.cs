using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

/// <summary>
/// Data-Structure on when to message the Client asking them to Schedule another appointment
/// </summary>
public class AptContact {

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

	[Required]
	[ForeignKey(nameof(AptLog))]
	public string aptLogId { get; set; }

	/// <summary>
	/// Navigation Property of the AptLog
	/// </summary>
	[JsonIgnore]
	public AptLog aptLog { get; set; }

	/// <summary>
	/// The Date to Contact the Customer
	/// The Time is used because chances are the better time of day to contact them is at the time of the log
	/// But not necessarily that time, thus it's changeable
	/// </summary>
	public DateTime dateTime { get; set; }

	//TODO: public bool beenDone { get; set ;}	//TODO: include in the endpoint

	public AptContact() : this(new AptLog(), DateTime.UtcNow) { }

	public AptContact(AptLog log, DateTime dateTime) {
		Id = Guid.NewGuid().ToString();
		CustomerId = log.CustomerId;
		BusinessId = log.BusinessId;
		aptLogId = log.Id;
		this.dateTime = dateTime;
		Customer = null!;
		aptLog = log;
	}
}
