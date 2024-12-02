using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class IdlePeriod {

	[Required]
	public string Id { get; set; }

	[Required]
	public string Name { get; set; }

	[Required]
	public string BusinessId { get; set; }

	[Required]
	public DateTime Start { get; set; }

	[Required]
	public DateTime Finish { get; set; }

	public IdlePeriod() : this(string.Empty, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), null!) { }

	public IdlePeriod(string businessId, DateTime start, DateTime finish, string name) {
		Id = Guid.NewGuid().ToString();
		Name = name;
		BusinessId = businessId;
		this.Start = start;
		this.Finish = finish;
	}
}
