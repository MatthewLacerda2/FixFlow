using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class FlowLoginRequest
{

    public string UserName { get; set; }

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(7)]
    public string password { get; set; }

    public FlowLoginRequest(string _username, string _password)
    {
        UserName = _username;
        password = _password;
    }

    public FlowLoginRequest(string _username, string _email, string _password, string _newPassword)
    {
        UserName = _username;
        Email = _email;
        password = _password;
    }

}