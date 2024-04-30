using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class BusinessDTO
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    /// <summary>
    /// CPF. Must be precisely XXX.XXX.XXX-XX
    /// </summary>
    [Length(14, 14)]
    public string CPF { get; set; }

    public string CNPJ { get; set; }

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

    public BusinessDTO(string id, string name, string cpf, string cnpj, string _userName, string phonenumber, string email)
    {
        Id = id;
        Name = name;
        CPF = cpf;
        CNPJ = cnpj;

        UserName = _userName;
        PhoneNumber = phonenumber;
        Email = email;
    }

    public static explicit operator Business(BusinessDTO businessDTO)
    {
        return new Business(businessDTO.Name, businessDTO.CPF, businessDTO.CNPJ, businessDTO.PhoneNumber!, businessDTO.Email!);
    }

    public static explicit operator BusinessDTO(Business business)
    {
        return new BusinessDTO(business.Id, business.Name, business.CPF, business.CNPJ, business.UserName!, business.PhoneNumber!, business.Email!);
    }
}