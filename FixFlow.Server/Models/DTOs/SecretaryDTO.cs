namespace Server.Models.DTO;

public class SecretaryDTO {
    
    public string Id = string.Empty;
    public string Name = string.Empty;
    public string Phone = string.Empty;
    public TimeInterval shift = new TimeInterval();

    public static explicit operator SecretaryDTO(Secretary employee){
        return new SecretaryDTO {
            Id = employee.Id,
            Name = employee.UserName!,
            Phone = employee.PhoneNumber!,
            shift = employee.shift
        };
    }
}