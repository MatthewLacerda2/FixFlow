using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

/// <summary>
/// Brief information about the Customer
/// </summary>
public class CustomerDTO {

	[Required]
	public string Id { get; set; }

	/// <summary>
	/// Phone Number. Must contain only numbers
	/// </summary>
	[Required]
	[Phone]
	public string PhoneNumber { get; set; }

	[EmailAddress]
	public string? Email { get; set; }

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

	public CustomerDTO() : this(string.Empty, string.Empty, string.Empty, null, null, null) { }

	public CustomerDTO(string _Id, string fullname, string _phoneNumber, string? _email, string? additionalNote, string? cpf) {
		Id = _Id;
		FullName = fullname;
		CPF = cpf;
		AdditionalNote = additionalNote;
		PhoneNumber = _phoneNumber;
		Email = _email;
	}

	public static explicit operator CustomerDTO(Customer customer) {
		return new CustomerDTO
		(
			customer.Id,
			customer.FullName,
			customer.PhoneNumber!,
			customer.Email,
			customer.AdditionalNote,
			customer.CPF
		);
	}
}
