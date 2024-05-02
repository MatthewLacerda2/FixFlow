using Microsoft.AspNetCore.Identity;
using Server.Models.DTO;

namespace Server.Models;

public class Client : IdentityUser
{
    public DateTime CreatedDate { get; set; }
    public DateTime LastLogin { get; set; }
    public string FullName { get; set; }
    public string CPF { get; set; }

    public string additionalNote { get; set; }

    /// <summary>
    /// Whether or not the Account was registered by a Client
    /// 
    /// If not, this value is false,
    /// thus Client didn't insert a password and this account is not supposed to be logged in
    /// </summary>
    public bool signedUp { get; set; }

    public Client()
    {
        CreatedDate = DateTime.Now;
        LastLogin = DateTime.Now;

        FullName = string.Empty;
        CPF = string.Empty;
        additionalNote = string.Empty;
    }

    public Client(string fullname, string cpf, string _additionalNote, string _phoneNumber, string _email, bool _signedup)
    {
        CreatedDate = DateTime.Now;
        LastLogin = DateTime.Now;

        FullName = fullname;
        CPF = cpf;
        additionalNote = _additionalNote;

        PhoneNumber = _phoneNumber;
        Email = _email;

        signedUp = _signedup;
    }

    public Client(ClientRegister register)
    {
        CreatedDate = DateTime.Now;
        LastLogin = DateTime.Now;

        FullName = register.FullName;
        CPF = register.CPF;
        additionalNote = register.additionalNote;
        signedUp = register.signedUp;
    }
}