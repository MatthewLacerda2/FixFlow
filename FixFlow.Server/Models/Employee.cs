using Microsoft.AspNetCore.Identity;

namespace Server.Models;

public class Employee : IdentityUser
{
    public DateTime CreatedDate { get; }
    public DateTime LastLogin { get; set; }
    public string FullName { get; set; }
    public string CPF { get; set; }

    public float salary { get; set; }

    public bool isDeleted { get; set; } = false;

    public Employee()
    {
        CreatedDate = DateTime.Now;
        LastLogin = DateTime.Now;

        FullName = string.Empty;
        CPF = string.Empty;

    }

    public Employee(string fullname, string cpf, float _salary, string email, string phonenumber)
    {
        CreatedDate = DateTime.Now;
        LastLogin = DateTime.Now;

        FullName = fullname;
        CPF = cpf;
        salary = _salary;

        Email = email;
        PhoneNumber = phonenumber;
    }
}