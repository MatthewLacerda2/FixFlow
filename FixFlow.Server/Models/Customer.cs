using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Server.Models.DTO;

namespace Server.Models;

public class Customer : IdentityUser {

	/// <summary>
	/// The business from which the Customer is a customer
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

	public Customer() : this(string.Empty, string.Empty, string.Empty, null, null, null) { }

	public Customer(string businessId, string phoneNumber, string fullName, string? email, string? cpf, string? additionalNote) {
		Id = Guid.NewGuid().ToString();
		BusinessId = businessId;
		PhoneNumber = phoneNumber;
		FullName = fullName;
		Email = email;
		CPF = cpf;
		AdditionalNote = additionalNote;
	}

	public static explicit operator Customer(CustomerCreate customerCreate) {
		return new Customer
		(
			customerCreate.BusinessId,
			customerCreate.PhoneNumber,
			customerCreate.FullName,
			customerCreate.Email,
			customerCreate.CPF,
			customerCreate.additionalNote
		);
	}
}
