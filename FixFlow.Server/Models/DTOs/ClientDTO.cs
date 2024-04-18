using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class ClientDTO
{
    [Required]
    public string Id;

    [Required]
    public string FullName;

    /// <summary>
    /// CPF. Must be only precisely 11 numbers
    /// </summary>
    [Length(11, 11)]
    public string CPF;

    /// <summary>
    /// Special information about the Client, if applicable
    /// </summary>
    public string additionalNote = string.Empty;

    /// <summary>
    /// NickName. Must not contain spaces
    /// </summary>
    public string UserName;

    /// <summary>
    /// Phone Number. Must contain only numbers, and may be preceded by a '+'
    /// </summary>
    [Required]
    [Phone]
    public string PhoneNumber;

    [EmailAddress]
    public string Email;

    public ClientDTO(string _Id, string fullname, string cpf, string _userName, string _phoneNumber, string _email)
    {
        Id = _Id;
        FullName = fullname;
        CPF = cpf;
        UserName = _userName;
        PhoneNumber = _phoneNumber;
        Email = _email;
    }

    public static explicit operator Client(ClientDTO clientDTO)
    {
        return new Client(clientDTO.FullName, clientDTO.PhoneNumber!, clientDTO.additionalNote, clientDTO.CPF, clientDTO.Email!);
    }

    public static explicit operator ClientDTO(Client client)
    {
        return new ClientDTO(client.Id, client.FullName, client.CPF, client.UserName!, client.PhoneNumber!, client.Email!);
    }
}