using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class IdlePeriod {

	[Required]
	public string businessId = string.Empty;

	public DateTime start;
	public DateTime end;

}
