using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class BusinessRegister
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    /// <summary>
    /// CPF. Must be only precisely 11 numbers
    /// </summary>
    [Length(14, 14)]
    public string CPF { get; set; }

    public string CNPJ { get; set; }

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

    public BusinessRegister(string id, string name, string cpf, string cnpj, string _userName, string phonenumber, string email)
    {
        Id = id;
        Name = name;
        CPF = cpf;
        CNPJ = cnpj;

        UserName = _userName;
        PhoneNumber = phonenumber;
        Email = email;
    }

    public void SetPasswords(string _currentPassword, string _newPassword)
    {
        currentPassword = _currentPassword;
        newPassword = _newPassword;
    }

    public static explicit operator Business(BusinessRegister businessDTO)
    {
        return new Business(businessDTO.Name, businessDTO.CPF, businessDTO.CNPJ, businessDTO.Email!, businessDTO.PhoneNumber!);
    }

    public static explicit operator BusinessRegister(Business business)
    {
        return new BusinessRegister(business.Id, business.Name, business.CPF, business.CNPJ, business.UserName!, business.PhoneNumber!, business.Email!);
    }
}