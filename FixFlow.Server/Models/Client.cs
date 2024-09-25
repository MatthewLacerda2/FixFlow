using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Server.Models.DTO;
//TODO: considerar quando o cliente é outra empresa
namespace Server.Models;

public class Client : IdentityUser {

	/// <summary>
	/// The business from which the Client is a customer
	/// </summary>
	[Required]
	public string businessId { get; set; }

	[Required]
	[MinLength(5)]
	public string FullName { get; set; }

	/// <summary>
	/// CPF. Must be on format XXX.XXX.XXX-XX
	/// </summary>
	[Length(14, 14)]
	public string? CPF { get; set; }

	public string? additionalNote { get; set; }

	public Client() {
		FullName = string.Empty;
		businessId = string.Empty;
	}

	public Client(string businessId, string phoneNumber, string fullName, string? email, string? cpf, string? additionalNote) {
		this.businessId = businessId;
		this.PhoneNumber = phoneNumber;
		this.FullName = fullName;
		this.Email = email;
		this.CPF = cpf;
		this.additionalNote = additionalNote;
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
