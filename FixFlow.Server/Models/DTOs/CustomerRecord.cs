using System.ComponentModel.DataAnnotations;
using Server.Models.Appointments;

namespace Server.Models.DTO;

/// <summary>
/// Customer history in the business
/// </summary>
public class CustomerRecord {

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

	public required string? CPF { get; set; }

	/// <summary>
	/// Special information about the Customer, if applicable
	/// </summary>
	public string? AdditionalNote { get; set; }

	public DateTime? firstLog { get; set; }
	public DateTime? lastLog { get; set; }

	public AptLog[] logs { get; set; } = Array.Empty<AptLog>();

	public int numSchedules { get; set; }

	public int avgTimeBetweenSchedules { get; set; }

	public static explicit operator CustomerRecord(Customer customer) {
		return new CustomerRecord {
			FullName = customer.FullName,
			PhoneNumber = customer.PhoneNumber!,
			Email = customer.Email,
			CPF = customer.CPF,
			AdditionalNote = customer.AdditionalNote,
		};
	}
}
