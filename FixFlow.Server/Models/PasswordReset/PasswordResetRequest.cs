using System.ComponentModel.DataAnnotations;

namespace Server.Models.PasswordReset;

public class PasswordResetRequest
{

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string token { get; set; }

    [Required]
    [MinLength(7)]
    public string password { get; set; } = string.Empty;

    [Required]
    [MinLength(7)]
    public string confirmPassword { get; set; } = string.Empty;

    public PasswordResetRequest(string _email, string _token)
    {
        Email = _email;
        token = _token;
    }

}