using Microsoft.AspNetCore.Identity;

namespace Server.Models;

public class Client : IdentityUser
{

    public DateTime LastLogin { get; set; }
    public string FullName { get; set; }
    public string CPF { get; set; }
    public string additionalNote { get; set; }

    public Client()
    {
        FullName = string.Empty;
        CPF = string.Empty;
        additionalNote = string.Empty;
    }

    public Client(string fullname, string cpf, string _phoneNumber, string _email, string _additionalNote)
    {
        FullName = fullname;
        CPF = cpf;
        PhoneNumber = _phoneNumber;
        Email = _email;
        additionalNote = _additionalNote;
    }
}