namespace Server.Models.DTO;

public class ClientDTO {

    public string Id = string.Empty;
    public string Name = string.Empty;
    public string Phone = string.Empty;

    public static explicit operator ClientDTO(Client client){
        return new ClientDTO {
            Id = client.Id,
            Name = client.UserName!,
            Phone = client.PhoneNumber!
        };
    }
}