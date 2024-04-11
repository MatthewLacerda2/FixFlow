namespace Server.Models.DTO;

public class EmployeeRegister
{
    public string Id;
    public string FullName;
    public string CPF;

    public string UserName;
    public string Email;
    public string PhoneNumber;
    public float salary;

    public string currentPassword = string.Empty;
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