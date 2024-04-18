using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class ClientDTO
{
    public string Id { get; set; }

    [Required]
    public string FullName { get; set; }

    public string CPF { get; set; }
    public string additionalNote { get; set; } = string.Empty;

    public string UserName { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [EmailAddress]
    public string Email { get; set; }

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