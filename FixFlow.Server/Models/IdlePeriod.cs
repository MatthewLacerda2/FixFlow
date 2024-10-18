namespace Server.Models;

public class IdlePeriod {

	public string Id { get; set; }

	public string Name { get; set; }

	public string BusinessId { get; set; }
	public DateTime start { get; set; }
	public DateTime finish { get; set; }

	public IdlePeriod() : this(string.Empty, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), null!) { }

	public IdlePeriod(string businessId, DateTime start, DateTime finish, string name) {
		Id = Guid.NewGuid().ToString();
		Name = name;
		BusinessId = businessId;
		this.start = start;
		this.finish = finish;
	}
}
