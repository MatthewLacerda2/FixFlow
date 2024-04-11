namespace Server.Models.DTO;

public class EmployeeDTO
{
    public string Id;
    public string FullName;
    public string CPF;

    public string Email;
    public string PhoneNumber;
    public float salary;

    public EmployeeDTO(string id, string fullname, string cpf, string email, string phonenumber, float _salary)
    {
        Id = id;
        FullName = fullname;
        CPF = cpf;
        Email = email;
        PhoneNumber = phonenumber;
        salary = _salary;
    }

    public static explicit operator Employee(EmployeeDTO employeeDTO)
    {
        return new Employee(employeeDTO.FullName, employeeDTO.CPF, employeeDTO.salary, employeeDTO.Email!, employeeDTO.PhoneNumber!);
    }

    public static explicit operator EmployeeDTO(Employee employee)
    {
        return new EmployeeDTO(employee.Id, employee.FullName, employee.CPF, employee.Email!, employee.PhoneNumber!, employee.salary);
    }
}