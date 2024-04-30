using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class BusinessRegister
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
    /// Phone Number. Must contain only numbers and/or a '+'
    /// </summary>
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [MinLength(7)]
    public string currentPassword = string.Empty;

    /// <summary>
    /// New Password. Only used when registering the user or changing the password
    /// For Logging in, use FlowLoginRequest instead
    /// </summary>
    [MinLength(7)]
    public string newPassword { get; set; } = string.Empty;

    public BusinessRegister(string id, string fullname, string cpf, string _userName, string email, string phonenumber, float _salary)
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

    public static explicit operator Business(BusinessRegister businessDTO)
    {
        return new Business(businessDTO.FullName, businessDTO.CPF, businessDTO.salary, businessDTO.Email!, businessDTO.PhoneNumber!);
    }

    public static explicit operator BusinessRegister(Business business)
    {
        return new BusinessRegister(business.Id, business.FullName, business.CPF, business.UserName!, business.Email!, business.PhoneNumber!, business.salary);
    }
}