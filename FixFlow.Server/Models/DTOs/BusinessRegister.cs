using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class BusinessRegister {

    [Required]
    public string Id { get; set; }

    /// <summary>
    /// NickName. Must not contain spaces
    /// </summary>
    [Required]
    [MinLength(2)]
    public string Name { get; set; }

    /// <summary>
    /// CPF. Must be on format XXX.XXX.XXX-XX
    /// </summary>
    [Length(14, 14)]
    public string CPF { get; set; }
    //TODO: tem que ao menos CPF *OU* CNPJ

    /// <summary>
    /// CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
    /// </summary>
    public string? CNPJ { get; set; }

    [MinLength(5)]
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

    public BusinessRegister(string id, string name, string cpf, string cnpj, string _userName, string phonenumber, string email, string _description) {
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