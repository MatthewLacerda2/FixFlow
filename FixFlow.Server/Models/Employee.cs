using Microsoft.AspNetCore.Identity;
namespace Server.Models;

public class Employee : IdentityUser {

    public DateTime LastLogin { get; set; }
    public string FullName { get; set; }
    public string CPF { get; set; }
    
    public float salary { get; set; }
    public TimeInterval shift { get; set; }

    public int appointmentsDone { get; set; }

    public Employee(string fullname, string email, string cpf, string phonenumber, float _salary, TimeInterval _shift){

        FullName = fullname;
        Email = email;
        PhoneNumber = phonenumber;
        CPF = cpf;

        salary = _salary;
        shift = _shift;
    }        
}