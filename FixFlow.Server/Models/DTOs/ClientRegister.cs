using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class ClientRegister
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
    /// Phone Number. Must contain only numbers and/or a '+'
    /// </summary>
    [Required]
    [Phone]
    public string PhoneNumber;

    [EmailAddress]
    public string Email;

    public string currentPassword = string.Empty;

    /// <summary>
    /// New Password. Only used when registering the user or changing the password
    /// For Logging in, use FlowLoginRequest instead
    /// </summary>
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