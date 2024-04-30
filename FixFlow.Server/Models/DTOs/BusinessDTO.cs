using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class BusinessDTO
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string FullName { get; set; }

    /// <summary>
    /// CPF. Must be only precisely 11 numbers
    /// </summary>
    [Length(11, 11)]
    public string CPF { get; set; }

    public float salary { get; set; }

    /// <summary>
    /// NickName. Must not contain spaces
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Phone Number. Must contain only numbers, and may be preceded by a '+'
    /// </summary>
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public BusinessDTO(string id, string fullname, string cpf, string _userName, string email, string phonenumber, float _salary)
    {
        Id = id;
        FullName = fullname;
        CPF = cpf;
        UserName = _userName;
        Email = email;
        PhoneNumber = phonenumber;
        salary = _salary;
    }

    public static explicit operator Business(BusinessDTO businessDTO)
    {
        return new Business(businessDTO.FullName, businessDTO.CPF, businessDTO.salary, businessDTO.Email!, businessDTO.PhoneNumber!);
    }

    public static explicit operator BusinessDTO(Business business)
    {
        return new BusinessDTO(business.Id, business.FullName, business.CPF, business.UserName!, business.Email!, business.PhoneNumber!, business.salary);
    }
}