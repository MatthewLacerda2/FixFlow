namespace Server.Models.DTO;

public class EmployeeRegister
{
    public string Id;
    public string FullName;
    public string CPF;

    public string Email;
    public string PhoneNumber;
    public float salary;

    public string currentPassword;
    public string newPassword;

    public EmployeeRegister(string id, string fullname, string cpf, string email, string phonenumber, float _salary, string _currentPassword, string _newPassword)
    {
        Id = id;
        FullName = fullname;
        CPF = cpf;
        Email = email;
        PhoneNumber = phonenumber;
        salary = _salary;
        currentPassword = _currentPassword;
        newPassword = _newPassword;
    }

    public static explicit operator Employee(EmployeeRegister employeeDTO)
    {
        return new Employee(employeeDTO.FullName, employeeDTO.CPF, employeeDTO.salary, employeeDTO.Email!, employeeDTO.PhoneNumber!);
    }

    public static explicit operator EmployeeRegister(Employee employee)
    {
        return new EmployeeRegister(employee.Id, employee.FullName, employee.CPF, employee.Email!, employee.PhoneNumber!, employee.salary, "", "");
    }
}