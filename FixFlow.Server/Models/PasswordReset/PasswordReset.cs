using System.ComponentModel.DataAnnotations;

namespace Server.Models.PasswordReset;

public class PasswordReset
{

    [Key]
    public string token { get; set; }

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public DateTime dateTime { get; set; }

    public PasswordReset()
    {
        Email = string.Empty;
        token = string.Empty;
        dateTime = DateTime.Now;
    }

    public PasswordReset(string _email, string _token, DateTime datetime)
    {
        Email = _email;
        token = _token;
        dateTime = datetime;
    }

}