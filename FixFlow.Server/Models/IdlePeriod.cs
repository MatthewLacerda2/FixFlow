namespace Server.Models;

public class IdlePeriod {

	public string Id = string.Empty;
	public string businessId = string.Empty;
	public DateTime start, finish;

	public bool isDateWithinIdlePeriod(DateTime date) {
		return date >= start && date <= finish;
	}

}