using Microsoft.AspNetCore.Identity;

namespace Server.Models;

public class Client : IdentityUser {

    public DateTime LastLogin { get; set; }
    public string Fullname { get; set; } = string.Empty;
    public string? CPF { get; set; } = string.Empty;
    public string additionalNote { get; set; } = string.Empty;

    public Client(string fullname, string cpf, string phonenumber, string email){
        Fullname = fullname;
        CPF = cpf;
        PhoneNumber = phonenumber;
        Email = email;
    }
}