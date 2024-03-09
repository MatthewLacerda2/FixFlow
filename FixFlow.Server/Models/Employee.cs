using Microsoft.AspNetCore.Identity;

namespace Models;

public class Employee : IdentityUser {
    
    public int appointmentsDone { get; set; } = 0;
    public float salary { get; set; } = 2000;
    public TimeSpan shift { get; set; }
    
}