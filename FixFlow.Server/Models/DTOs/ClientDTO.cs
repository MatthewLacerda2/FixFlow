using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

/// <summary>
/// Brief information about the Client
/// </summary>
public class ClientDTO {

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
	public string? AdditionalNote { get; set; }

	/// <summary>
	/// Phone Number. Must contain only numbers
	/// </summary>
	[Required]
	[Phone]
	public string PhoneNumber { get; set; }

	[EmailAddress]
	public string? Email { get; set; }

	public ClientDTO(string _Id, string fullname, string _phoneNumber, string? _email, string? additionalNote) {
		Id = _Id;
		FullName = fullname;
		PhoneNumber = _phoneNumber;
		AdditionalNote = additionalNote;
		Email = _email;
	}

	public static explicit operator ClientDTO(Client client) {
		return new ClientDTO
		(
			client.Id,
			client.FullName,
			client.PhoneNumber!,
			client.Email,
			client.AdditionalNote
		);
	}
}
