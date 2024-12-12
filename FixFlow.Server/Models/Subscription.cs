using Newtonsoft.Json;

namespace Server.Models;

/// <summary>
/// The monthly payment to use the service
/// </summary>
/// <remarks>
/// Payments must be saved even after the Business was deleted
/// </remarks>
public class Subscription {

	public string Id { get; set; }

	public string BusinessId { get; set; }

	/// <summary>
	/// The name of the Business or Business' owner.
	/// </summary>
	public string BusinessName { get; set; }

	public string BusinessCNPJ { get; set; }

	/// <summary>
	/// The date from when the service started being used
	/// </summary>
	public DateTime dateTime { get; set; }

	public int Price { get; set; }

	public bool Payed { get; set; }

	/// <summary>
	/// Special information about that month's payment
	/// </summary>
	[JsonIgnore]
	public string? AdditionalNote { get; set; }

	/// <summary>
	/// When we deactivate the account, we store the time the user spent, so if he comes back he'll pick up the time left
	/// </summary>
	public TimeSpan timeSpentDeactivated { get; set; }

	public Subscription() : this(string.Empty, string.Empty, string.Empty, DateTime.Now, 0, false, string.Empty) { }

	public Subscription(string businessId, string businessName, string businessCNPJ, DateTime _dateTime, int price, bool payed, string? additionalNote) {
		Id = Guid.NewGuid().ToString();
		BusinessId = businessId;
		BusinessName = businessName;
		BusinessCNPJ = businessCNPJ;
		dateTime = _dateTime;
		Price = price;
		Payed = payed;
		AdditionalNote = additionalNote;
		timeSpentDeactivated = TimeSpan.Zero;
	}
}
