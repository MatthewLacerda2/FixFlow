using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class IdlePeriod {

	public string Id { get; set; }

	public string Description { get; set; }

	public string BusinessId { get; set; }
	public DateTime start { get; set; }
	public DateTime finish { get; set; }

	public IdlePeriod() : this(string.Empty, DateTime.Now, DateTime.Now.AddDays(1), null!) { }

	public IdlePeriod(string businessId, DateTime start, DateTime finish, string descript) {
		Id = Guid.NewGuid().ToString();
		Description = descript;
		BusinessId = businessId;
		this.start = start;
		this.finish = finish;
	}
}
