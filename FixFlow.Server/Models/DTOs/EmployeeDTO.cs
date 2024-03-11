namespace Server.Models.DTO;

public class EmployeeDTO {

    public string Id = string.Empty;
    public string FullName = string.Empty;
    public string CPF = string.Empty;
    public string PhoneNumber = string.Empty;
    public TimeInterval shift = new TimeInterval();
    
    public int salary;
    public int appointmentsDone;

    public static explicit operator EmployeeDTO(Employee employee){
        return new EmployeeDTO {
            Id = employee.Id,
            FullName = employee.UserName!,
            PhoneNumber = employee.PhoneNumber!,
            shift = employee.shift
        };
    }
}