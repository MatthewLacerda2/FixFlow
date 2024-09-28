using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Server.Models.DTO;

namespace Server.Models;

public class Client : IdentityUser {

	/// <summary>
	/// The business from which the Client is a customer
	/// </summary>
	[Required]
	public string BusinessId { get; set; }

	[Required]
	[MinLength(5)]
	public string FullName { get; set; }

	/// <summary>
	/// CPF. Must be on format XXX.XXX.XXX-XX
	/// </summary>
	[Length(14, 14)]
	public string? CPF { get; set; }

	public string? AdditionalNote { get; set; }

	public Client() {
		FullName = string.Empty;
		BusinessId = string.Empty;
	}

	public Client(string businessId, string phoneNumber, string fullName, string? email, string? cpf, string? additionalNote) {
		BusinessId = businessId;
		PhoneNumber = phoneNumber;
		FullName = fullName;
		Email = email;
		CPF = cpf;
		AdditionalNote = additionalNote;
	}

	public static explicit operator Client(ClientCreate clientCreate) {
		return new Client
		(
			clientCreate.businessId,
			clientCreate.PhoneNumber,
			clientCreate.FullName,
			clientCreate.Email,
			clientCreate.CPF,
			clientCreate.additionalNote
		);
	}
}
