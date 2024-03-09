namespace Models;

public class CompletedAppointment {

    public Guid Id;
    public string AttendantId = string.Empty;
    public string clientId = string.Empty;
    
    public string clientName { get; set;} = string.Empty;
    public float price = 30f;
    public int duration = 1;
    public string observation { get; set;} = string.Empty;
    public DateTime time  { get; set;}
}