using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class CustomerCreate {

	/// <summary>
	/// The business from which the Customer is a customer
	/// </summary>
	[Required]
	public string BusinessId { get; set; } = string.Empty;

	[Required]
	[MinLength(5)]
	public string FullName { get; set; } = string.Empty;

	/// <summary>
	/// CPF. Must be on format XXX.XXX.XXX-XX
	/// </summary>
	[Length(14, 14)]
	public string? CPF { get; set; }

	/// <summary>
	/// Special information about the Customer, if applicable
	/// </summary>
	public string? additionalNote { get; set; }

	/// <summary>
	/// Phone Number. Must contain only numbers
	/// </summary>
	[Required]
	[Phone]
	public string PhoneNumber { get; set; } = string.Empty;

	[EmailAddress]
	public string? Email { get; set; }

	public CustomerCreate(string businessId, string fullName, string? cpf, string? additionalNote, string phoneNumber, string? email) {
		BusinessId = businessId;
		FullName = fullName;
		CPF = cpf;
		PhoneNumber = phoneNumber;
		Email = email;
		this.additionalNote = additionalNote;
	}
}