namespace Server.Models.DTO;

public class ClientDTO
{
    public string Id;
    public string FullName;
    public string CPF;
    public string additionalNote;

    public string PhoneNumber;
    public string Email;

    public ClientDTO(string id, string fullname, string cpf, string _additionalNote, string _phoneNumber, string _email)
    {
        Id = id;
        FullName = fullname;
        CPF = cpf;
        PhoneNumber = _phoneNumber;
        Email = _email;
        additionalNote = _additionalNote;
    }

    public static explicit operator Client(ClientDTO clientDTO)
    {
        return new Client(clientDTO.FullName, clientDTO.PhoneNumber!, clientDTO.CPF, clientDTO.Email!, clientDTO.additionalNote);
    }

    public static explicit operator ClientDTO(Client client)
    {
        return new ClientDTO(client.Id, client.FullName, client.PhoneNumber!, client.CPF, client.Email!, client.additionalNote);
    }
}