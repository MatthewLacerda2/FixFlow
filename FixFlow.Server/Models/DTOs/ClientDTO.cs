using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class ClientDTO
{
    public string Id;

    [Required]
    public string FullName;

    public string CPF;
    public string additionalNote = string.Empty;

    public string UserName;

    [Required]
    [Phone]
    public string PhoneNumber;

    [EmailAddress]
    public string Email;

    public ClientDTO(string id, string fullname, string cpf, string _userName, string _phoneNumber, string _email)
    {
        Id = id;
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