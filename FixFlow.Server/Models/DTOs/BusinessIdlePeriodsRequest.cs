namespace Server.Models;

public class BusinessIdlePeriodsRequest {

	public string BusinessId { get; set; } = string.Empty;

	public DateTime Date { get; set; }

	public BusinessIdlePeriodsRequest(string businessId, DateTime date) {
		BusinessId = businessId;
		Date = date;
	}
}
