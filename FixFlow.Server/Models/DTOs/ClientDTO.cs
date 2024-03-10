namespace Server.Models.DTO;

public class ClientDTO {

    public string Id = string.Empty;
    public string Fullname = string.Empty;
    public string PhoneNumber = string.Empty;
    public string? CPF = string.Empty;
    public string? Email = string.Empty;

    public static explicit operator ClientDTO(Client client){
        return new ClientDTO {
            Id = client.Id,
            Fullname = client.Fullname,
            PhoneNumber = client.PhoneNumber!,
            CPF = client.CPF,
            Email = client.Email
        };
    }
}