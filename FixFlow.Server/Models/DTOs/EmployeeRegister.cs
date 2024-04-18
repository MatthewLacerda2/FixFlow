using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class EmployeeRegister
{
    [Required]
    public string Id;

    [Required]
    public string FullName;

    /// <summary>
    /// CPF. Must be only precisely 11 numbers
    /// </summary>
    [Length(11, 11)]
    public string CPF;

    public float salary;

    /// <summary>
    /// NickName. Must not contain spaces
    /// </summary>
    public string UserName;

    /// <summary>
    /// Phone Number. Must contain only numbers and/or a '+'
    /// </summary>
    [Required]
    [Phone]
    public string PhoneNumber;

    [EmailAddress]
    public string Email;

    [MinLength(7)]
    public string currentPassword = string.Empty;

    /// <summary>
    /// New Password. Only used when registering the user or changing the password
    /// For Logging in, use FlowLoginRequest instead
    /// </summary>
    [MinLength(7)]
    public string newPassword = string.Empty;

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