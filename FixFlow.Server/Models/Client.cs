using Microsoft.AspNetCore.Identity;

namespace Models;

public class Client : IdentityUser {

    //For faster queries
    public float spentSum { get; set; } = 30f;
    public int appointmentsSum { get; set; } = 1;
    public string additionalNote { get; set; } = string.Empty;

}