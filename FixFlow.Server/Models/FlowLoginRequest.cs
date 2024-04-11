namespace Server.Models;

public class FlowLoginRequest
{

    public string UserName { get; set; }
    public string Email { get; set; }

    public string password { get; set; }

    public FlowLoginRequest(string username, string email, string _password)
    {
        UserName = username;
        Email = email;
        password = _password;
    }

}