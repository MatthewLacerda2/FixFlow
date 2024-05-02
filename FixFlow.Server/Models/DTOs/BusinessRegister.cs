using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class BusinessRegister
{
    [Required]
    public string Id { get; set; }

    /// <summary>
    /// NickName. Must not contain spaces
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// CPF. Must be only precisely 11 numbers
    /// </summary>
    [Length(14, 14)]
    public string CPF { get; set; }

    public string CNPJ { get; set; }

    public string description { get; set; }

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

    public BusinessRegister(string id, string name, string cpf, string cnpj, string _userName, string phonenumber, string email, string _description)
    {
        Id = id;
        Name = name;
        CPF = cpf;
        CNPJ = cnpj;

        Name = _userName;
        PhoneNumber = phonenumber;
        Email = email;
        description = _description;
    }

    public void SetPasswords(string _currentPassword, string _newPassword)
    {
        currentPassword = _currentPassword;
        newPassword = _newPassword;
    }
}