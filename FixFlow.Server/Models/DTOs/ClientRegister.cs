namespace Server.Models.DTO;

public class ClientRegister
{
    public string Id;
    public string FullName;
    public string CPF;
    public string additionalNote = string.Empty;

    public string UserName;
    public string PhoneNumber;
    public string Email;

    public string currentPassword = string.Empty;
    public string newPassword = string.Empty;

    public ClientRegister(string _Id, string _FullName, string _CPF, string _userName, string _PhoneNumber, string _Email)
    {
        Id = _Id;
        FullName = _FullName;
        CPF = _CPF;
        UserName = _userName;
        PhoneNumber = _PhoneNumber;
        Email = _Email;
    }

    public void SetPasswords(string _currentPassword, string _newPassword)
    {
        currentPassword = _currentPassword;
        newPassword = _newPassword;
    }

    public static explicit operator Client(ClientRegister clientDTO)
    {
        return new Client(clientDTO.FullName, clientDTO.CPF, clientDTO.additionalNote, clientDTO.PhoneNumber!, clientDTO.Email!);
    }

    public static explicit operator ClientRegister(Client client)
    {
        return new ClientRegister(client.Id, client.FullName, client.CPF, client.UserName!, client.PhoneNumber!, client.Email!);
    }
}