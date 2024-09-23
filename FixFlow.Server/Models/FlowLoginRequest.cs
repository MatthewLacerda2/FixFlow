using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class FlowLoginRequest {

	[EmailAddress]
	public string email { get; set; }

	[Required]
	[MinLength(7)]
	public string password { get; set; }

	public FlowLoginRequest(string _email, string _password) {
		email = _email;
		password = _password;
	}
}
