using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class AptContact {

	[Required]
	public string Id { get; set; }

	/// <summary>
	/// The Id of the Customer to Contact
	/// </summary>
	[Required]
	[ForeignKey(nameof(Customer))]
	public string CustomerId { get; set; }

	/// <summary>
	/// Navigation Property of the Customer
	/// </summary>
	[JsonIgnore]
	public Customer customer { get; set; }

	/// <summary>
	/// The Id of the Business who owns this Contact
	/// </summary>
	[Required]
	[ForeignKey(nameof(Business))]
	public string BusinessId { get; set; }

	/// <summary>
	/// The Id of the Log that precedes this Contact
	/// </summary>
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
	/// The Time is used because, chances are, there is a better Time of the day to contact the Customer
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
		customer = null!;
		aptLog = log;
	}
}
