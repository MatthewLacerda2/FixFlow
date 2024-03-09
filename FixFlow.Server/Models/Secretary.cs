using Microsoft.AspNetCore.Identity;

namespace Server.Models;

public class Secretary : IdentityUser {

    public DateTime LastLogin { get; set; }

    public float salary { get; set; } = 2000;
    public TimeSpan shift { get; set; }

}