using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Server.Models.DTO;

namespace Server.Models;

/// <summary>
/// Clients of the Businesses
/// </summary>
/// <remarks>
/// Customers are NOT meant to Login. They're IdentityUsers out of convenience for development.
/// They are not called 'Client' to avoid class naming conflicts.
/// </remarks>
public class Customer : IdentityUser {

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

	/// <summary>
	/// Special information about the Customer, if applicable
	/// </summary>
	public string? AdditionalNote { get; set; }

	public Customer() : this(string.Empty, string.Empty, string.Empty, null, null, null) { }

	public Customer(string businessId, string phoneNumber, string fullName, string? email, string? cpf, string? additionalNote) {
		Id = Guid.NewGuid().ToString();
		BusinessId = businessId;
		PhoneNumber = phoneNumber;
		FullName = fullName;
		Email = email;
		CPF = cpf;
		AdditionalNote = additionalNote;

		UserName = GenerateUserName(fullName, Id);
	}

	public static explicit operator Customer(CustomerCreate customerCreate) {
		return new Customer
		(
			customerCreate.BusinessId,
			customerCreate.PhoneNumber,
			customerCreate.FullName,
			customerCreate.Email,
			customerCreate.CPF,
			customerCreate.AdditionalNote
		);
	}

	private string GenerateUserName(string fullName, string id) {
		var firstName = fullName.Split(' ')[0];
		var guidPrefix = id.Substring(0, 8);
		return $"{firstName}-{guidPrefix}";
	}
}
