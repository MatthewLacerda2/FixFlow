using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class EmployeeDTO
{
    public string Id { get; set; }

    [Required]
    public string FullName { get; set; }

    public string CPF { get; set; }
    public float salary { get; set; }

    public string UserName { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public EmployeeDTO(string id, string fullname, string cpf, string _userName, string email, string phonenumber, float _salary)
    {
        Id = id;
        FullName = fullname;
        CPF = cpf;
        UserName = _userName;
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
        return new EmployeeDTO(employee.Id, employee.FullName, employee.CPF, employee.UserName!, employee.Email!, employee.PhoneNumber!, employee.salary);
    }
}