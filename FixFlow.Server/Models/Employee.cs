using Microsoft.AspNetCore.Identity;

namespace Server.Models;

public class Employee : IdentityUser {

    public DateTime LastLogin { get; set; }
    public string CPF;
    
    public int appointmentsDone { get; set; }
    public float salary { get; set; }
    public TimeInterval shift { get; set; }

    public Employee(string id, string username, string email, string phonenumber, string cpf, float _salary, TimeInterval _shift){

        Id = id;
        UserName = username;
        Email = email;
        PhoneNumber = phonenumber;
        CPF = cpf;

        salary = _salary;
        shift = _shift;
    }
        
}