using System.ComponentModel.DataAnnotations;

namespace Server.Models.PasswordReset;

public class PasswordResetRequest
{

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string token { get; set; }

    [Required]
    [MinLength(7)]
    public string password { get; set; }

    [Required]
    [MinLength(7)]
    public string newPassword { get; set; }

    public PasswordResetRequest(string _email, string _token, string _password, string _newPassword)
    {
        Email = _email;
        token = _token;
        password = _password;
        newPassword = _newPassword;
    }

}