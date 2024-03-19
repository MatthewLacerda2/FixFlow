namespace Server.Models.DTO;

public class SecretaryDTO {
    
    public string Id;
    public string FullName;
    public string CPF;
    public string Email;    
    public string PhoneNumber;
    public TimeInterval shift;

    public float salary;

    public SecretaryDTO(string id, string fullname, string email, string phonenumber, string cpf, float _salary, TimeInterval _shift){
        Id = id;
        FullName = fullname;
        Email = email;
        PhoneNumber = phonenumber;
        CPF = cpf;
        shift = _shift;
        salary = _salary;
    }

    public static explicit operator Secretary(SecretaryDTO employee){
        return new Secretary( employee.FullName, employee.Email!, employee.PhoneNumber!, employee.CPF, employee.salary, employee.shift );
    }

    public static explicit operator SecretaryDTO(Secretary employee){
        return new SecretaryDTO( employee.Id, employee.FullName, employee.Email!, employee.PhoneNumber!, employee.CPF, employee.salary, employee.shift );
    }
}