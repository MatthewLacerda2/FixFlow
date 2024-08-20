using System.ComponentModel.DataAnnotations;

namespace Server.Models.PasswordReset;

public class PasswordResetRequest {

	[Key]
	public string token { get; set; }

	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	public DateTime dateTime { get; set; }

	public PasswordResetRequest() {
		Email = string.Empty;
		token = string.Empty;
		dateTime = DateTime.Now;
	}

	public PasswordResetRequest(string _email, string _token, DateTime datetime) {
		Email = _email;
		token = _token;
		dateTime = datetime;
	}

}
