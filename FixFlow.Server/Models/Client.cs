using Microsoft.AspNetCore.Identity;

namespace Server.Models;

public class Client : IdentityUser {

    public DateTime LastLogin { get; set; }

    //For faster queries
    public float spentSum { get; set; } = 30f;
    public int appointmentsSum { get; set; } = 1;
    public string additionalNote { get; set; } = string.Empty;

    public Client(string id, string username, string email, string phonenumber){

        Id = id;
        UserName = username;
        Email = email;
        PhoneNumber = phonenumber;
    }
}