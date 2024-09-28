using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class BusinessRegisterRequest {

	/// <summary>
	/// The Name of the Business or Business owner
	/// </summary>
	[Required]
	[MinLength(2)]
	public string Name { get; set; } = string.Empty;

	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	/// <summary>
	/// CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
	/// </summary>
	[Length(18, 18)]
	public string CNPJ { get; set; } = string.Empty;

	/// <summary>
	/// Phone Number. Must contain only numbers and/or a '+'
	/// </summary>
	[Required]
	[Phone]
	public string PhoneNumber { get; set; } = string.Empty;

	[MinLength(7)]
	public string password = string.Empty;

	/// <summary>
	/// Must be identical to 'password'
	/// </summary>
	[MinLength(7)]
	public string confirmPassword { get; set; } = string.Empty;

	[Length(6, 6)]
	public string OTPCode { get; set; } = string.Empty;
}