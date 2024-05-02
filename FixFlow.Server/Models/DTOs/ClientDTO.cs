using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class ClientDTO
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string FullName { get; set; }

    /// <summary>
    /// CPF. Must be only precisely 11 numbers
    /// </summary>
    [Length(14, 14)]
    public string CPF { get; set; }

    /// <summary>
    /// Special information about the Client, if applicable
    /// </summary>
    public string additionalNote { get; set; } = string.Empty;

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

    [Required]
    public bool signedUp { get; set; }

    public ClientDTO(string _Id, string fullname, string cpf, string _userName, string _phoneNumber, string _email, bool _signedUp)
    {
        Id = _Id;
        FullName = fullname;
        CPF = cpf;
        UserName = _userName;
        PhoneNumber = _phoneNumber;
        Email = _email;
        signedUp = _signedUp;
    }

    public static explicit operator Client(ClientDTO clientDTO)
    {
        return new Client(clientDTO.FullName, clientDTO.PhoneNumber!, clientDTO.additionalNote, clientDTO.CPF, clientDTO.Email!, clientDTO.signedUp);
    }

    public static explicit operator ClientDTO(Client client)
    {
        return new ClientDTO(client.Id, client.FullName, client.CPF, client.UserName!, client.PhoneNumber!, client.Email!, client.signedUp);
    }
}