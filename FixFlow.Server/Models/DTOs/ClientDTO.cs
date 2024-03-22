namespace Server.Models.DTO;

public class ClientDTO {

    public string Id;
    public string FullName;
    public string CPF = string.Empty;
    public string Email = string.Empty;
    public string PhoneNumber;
    public string additionalNote;

    public ClientDTO(string id, string fullname, string cpf, string _phoneNumber, string _email, string _additionalNote){
        Id = id;
        FullName = fullname;
        CPF = cpf;
        PhoneNumber = _phoneNumber;
        Email = _email;
        additionalNote = _additionalNote;
    }

    public static explicit operator Client(ClientDTO clientDTO){
        return new Client( clientDTO.FullName, clientDTO.PhoneNumber!, clientDTO.CPF, clientDTO.Email!, clientDTO.additionalNote
        );
    }

    public static explicit operator ClientDTO(Client client){
        return new ClientDTO( client.Id, client.FullName, client.PhoneNumber!, client.CPF, client.Email!, client.additionalNote);
    }
}