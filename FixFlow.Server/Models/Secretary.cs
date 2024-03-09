using Microsoft.AspNetCore.Identity;

namespace Models;

public class Secretary : IdentityUser {

    public float salary { get; set; } = 2000;
    public TimeSpan shift { get; set; }

}