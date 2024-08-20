using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class ClientRegister {

	[Required]
	public string Id { get; set; }

	[Required]
	[MinLength(5)]
	public string FullName { get; set; }

	/// <summary>
	/// CPF. Must be on format XXX.XXX.XXX-XX
	/// </summary>
	[Length(14, 14)]
	public string? CPF { get; set; }

	/// <summary>
	/// Special information about the Client, if applicable
	/// </summary>
	public string? additionalNote { get; set; }

	/// <summary>
	/// NickName. Must not contain spaces
	/// </summary>
	[MinLength(2)]
	public string UserName { get; set; }

	/// <summary>
	/// Phone Number. Must contain only numbers
	/// </summary>
	[Required]
	[Phone]
	public string PhoneNumber { get; set; }

	[EmailAddress]
	public string Email { get; set; }

	/// <summary>
	/// Whether or not the Account was registered by a Client
	/// 
	/// If false, the Client didn't insert a password and this account is not supposed to be logged in
	/// </summary>
	[Required]
	public bool signedUp { get; set; }

	[MinLength(7)]
	public string password { get; set; } = string.Empty;

	/// <summary>
	/// Must be identical to 'password'
	/// </summary>
	public string confirmPassword { get; set; } = string.Empty;

	public ClientRegister(string _Id, string _FullName, string _CPF, string _userName, string _PhoneNumber, string _Email, bool _signedUp) {
		Id = _Id;
		FullName = _FullName;
		CPF = _CPF;
		UserName = _userName;
		PhoneNumber = _PhoneNumber;
		Email = _Email;
		signedUp = signedUp;
	}
}
