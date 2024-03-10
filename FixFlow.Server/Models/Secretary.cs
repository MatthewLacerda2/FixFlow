using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace Server.Models;

public class Secretary : IdentityUser {

    public DateTime LastLogin { get; set; }
    public string CPF;

    public float salary { get; set; }
    public TimeInterval shift { get; set; }

    public Secretary(string id, string username, string email, string phonenumber, string cpf, float _salary, TimeInterval _shift){

        Id = id;
        UserName = username;
        Email = email;
        PhoneNumber = phonenumber;
        CPF = cpf;

        salary = _salary;
        shift = _shift;
    }
}