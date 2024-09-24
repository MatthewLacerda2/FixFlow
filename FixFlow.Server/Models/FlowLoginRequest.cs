using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class FlowLoginRequest {

	[Required]
	[EmailAddress]
	public string email { get; set; }

	[Required]
	[MinLength(7)]
	public string password { get; set; }

	public FlowLoginRequest() {
		email = string.Empty;
		password = string.Empty;
	}
}
