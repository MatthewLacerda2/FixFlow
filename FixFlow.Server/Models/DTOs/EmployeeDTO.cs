namespace Server.Models.DTO;

public class EmployeeDTO {

    public string Id = string.Empty;
    public string Name = string.Empty;
    public string Phone = string.Empty;
    public TimeInterval shift = new TimeInterval();

    public static explicit operator EmployeeDTO(Employee employee){
        return new EmployeeDTO {
            Id = employee.Id,
            Name = employee.UserName!,
            Phone = employee.PhoneNumber!,
            shift = employee.shift
        };
    }
}