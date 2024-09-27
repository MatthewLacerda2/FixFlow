using System.ComponentModel.DataAnnotations;
using Server.Models.Appointments;

namespace Server.Models.DTO;

/// <summary>
/// Dossiê about the Client
/// </summary>
public class ClientFullInfo {

	[Required]
	[MinLength(5)]
	public string FullName { get; set; } = string.Empty;

	/// <summary>
	/// Phone Number. Must contain only numbers
	/// </summary>
	[Required]
	[Phone]
	public string PhoneNumber { get; set; } = string.Empty;

	[EmailAddress]
	public string? Email { get; set; }

	public string? CPF { get; set; }

	/// <summary>
	/// Special information about the Client, if applicable
	/// </summary>
	public string? AdditionalNote { get; set; }

	public DateTime? firstLog { get; set; }
	public DateTime? lastLog { get; set; }

	public AptLog[] logs { get; set; } = Array.Empty<AptLog>();

	public int numSchedules { get; set; }

	public static explicit operator ClientFullInfo(Client client) {
		return new ClientFullInfo {
			FullName = client.FullName,
			PhoneNumber = client.PhoneNumber!,
			Email = client.Email,
			CPF = client.CPF,
			AdditionalNote = client.AdditionalNote,
		};
	}

}
