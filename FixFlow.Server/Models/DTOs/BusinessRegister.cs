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

    public string? CNPJ { get; set; }

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
    public string password = string.Empty;

    /// <summary>
    /// Must be identical to 'password'
    /// </summary>
    [MinLength(7)]
    public string confirmPassword { get; set; } = string.Empty;

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
}