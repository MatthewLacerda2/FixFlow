namespace Server.Models.DTO;

public class EmployeeDTO
{

    public string Id;
    public string FullName;
    public string CPF;
    public string Email;
    public string PhoneNumber;

    public float salary;
    public int appointmentsDone;

    public EmployeeDTO(string id, string fullname, string cpf, string email, string phonenumber, float _salary, int _appointmentsDone)
    {
        Id = id;
        FullName = fullname;
        CPF = cpf;
        Email = email;
        PhoneNumber = phonenumber;
        salary = _salary;
        appointmentsDone = _appointmentsDone;
    }

    public static explicit operator Employee(EmployeeDTO employeeDTO)
    {
        return new Employee(employeeDTO.FullName, employeeDTO.Email!, employeeDTO.CPF, employeeDTO.PhoneNumber!, employeeDTO.salary);
    }

    public static explicit operator EmployeeDTO(Employee employee)
    {
        return new EmployeeDTO(employee.Id, employee.FullName, employee.CPF, employee.Email!, employee.PhoneNumber!, employee.salary, employee.appointmentsDone);
    }
}