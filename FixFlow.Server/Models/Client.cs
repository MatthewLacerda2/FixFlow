using Microsoft.AspNetCore.Identity;

namespace Server.Models;

public class Client : IdentityUser
{
    public DateTime CreatedDate { get; set; }
    public DateTime LastLogin { get; set; }
    public string FullName { get; set; }
    public string CPF { get; set; }

    public string additionalNote { get; set; }

    public Client()
    {
        CreatedDate = DateTime.Now;
        LastLogin = DateTime.Now;

        FullName = string.Empty;
        CPF = string.Empty;
        additionalNote = string.Empty;
    }

    public Client(string fullname, string cpf, string _additionalNote, string _phoneNumber, string _email)
    {
        CreatedDate = DateTime.Now;
        LastLogin = DateTime.Now;

        FullName = fullname;
        CPF = cpf;
        additionalNote = _additionalNote;

        PhoneNumber = _phoneNumber;
        Email = _email;
    }
}