using Microsoft.AspNetCore.Identity;
namespace Server.Models;

public class Secretary : IdentityUser
{

    public DateTime LastLogin { get; set; }
    public string FullName { get; set; }
    public string CPF { get; set; }

    public float salary { get; set; }

    public Secretary()
    {
        FullName = string.Empty;
        CPF = string.Empty;
    }

    public Secretary(string fullname, string email, string phonenumber, string cpf, float _salary)
    {

        FullName = fullname;
        Email = email;
        PhoneNumber = phonenumber;
        CPF = cpf;

        salary = _salary;
    }
}