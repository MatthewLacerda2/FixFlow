using Microsoft.AspNetCore.Identity;

namespace Server.Models;

public class Business : IdentityUser
{
    public DateTime CreatedDate { get; }
    public DateTime LastLogin { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }
    public string CNPJ { get; set; }

    public Business()
    {
        CreatedDate = DateTime.Now;
        LastLogin = DateTime.Now;

        Name = string.Empty;
        CPF = string.Empty;
        CNPJ = string.Empty;
    }

    public Business(string fullname, string cpf, string cnpj, string phonenumber, string email)
    {
        CreatedDate = DateTime.Now;
        LastLogin = DateTime.Now;

        Name = fullname;
        CPF = cpf;
        CNPJ = cnpj;

        PhoneNumber = phonenumber;
        Email = email;
    }
}