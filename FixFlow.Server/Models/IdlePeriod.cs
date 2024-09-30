using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class IdlePeriod {

	public string Id { get; set; }

	public string Description { get; set; }

	public string BusinessId { get; set; }
	public DateTime start { get; set; }
	public DateTime finish { get; set; }

	public IdlePeriod() {
		Id = Guid.NewGuid().ToString();
		Description = string.Empty;
		BusinessId = string.Empty;
		start = DateTime.Now;
		finish = DateTime.Now.AddDays(1);
	}

	public IdlePeriod(string businessId, DateTime start, DateTime finish, string descript) {
		Id = Guid.NewGuid().ToString();
		Description = descript;
		BusinessId = businessId;
		this.start = start;
		this.finish = finish;
	}
}
