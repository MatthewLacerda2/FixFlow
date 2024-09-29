namespace Server.Models;

public class IdlePeriod {

	public string Id { get; set; }

	public string description { get; set; }

	public string BusinessId { get; set; }
	public DateTime start { get; set; }
	public DateTime finish { get; set; }

	public IdlePeriod() {
		Id = new Guid().ToString();
		description = "desk";
		BusinessId = "business-id";
		start = DateTime.Now;
		finish = DateTime.Now.AddDays(1);
	}
}
