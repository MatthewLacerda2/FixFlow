using System.ComponentModel.DataAnnotations;

namespace Server.Models.PasswordReset;

public class PasswordReset
{

    [Required]
    public string token { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;

    [MinLength(7)]
    [Required]
    public string password { get; set; } = string.Empty;

    [MinLength(7)]
    [Required]
    public string confirmPassword { get; set; } = string.Empty;

    public PasswordReset(string _email, string _token)
    {
        Email = _email;
        token = _token;
    }

}