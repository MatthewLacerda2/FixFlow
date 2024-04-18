using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class EmployeeRegister
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

    public string currentPassword = string.Empty;
    public string newPassword { get; set; } = string.Empty;

    public EmployeeRegister(string id, string fullname, string cpf, string _userName, string email, string phonenumber, float _salary)
    {
        Id = id;
        FullName = fullname;
        CPF = cpf;
        UserName = _userName;
        Email = email;
        PhoneNumber = phonenumber;
        salary = _salary;
    }

    public void SetPasswords(string _currentPassword, string _newPassword)
    {
        currentPassword = _currentPassword;
        newPassword = _newPassword;
    }

    public static explicit operator Employee(EmployeeRegister employeeDTO)
    {
        return new Employee(employeeDTO.FullName, employeeDTO.CPF, employeeDTO.salary, employeeDTO.Email!, employeeDTO.PhoneNumber!);
    }

    public static explicit operator EmployeeRegister(Employee employee)
    {
        return new EmployeeRegister(employee.Id, employee.FullName, employee.CPF, employee.UserName!, employee.Email!, employee.PhoneNumber!, employee.salary);
    }
}