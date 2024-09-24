using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class BusinessInfo {

	/// <summary>
	/// The Name of the Business or Business owner
	/// </summary>
	[Required]
	[MinLength(2)]
	public string Name { get; set; }

	/// <summary>
	/// CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
	/// </summary>
	[Length(18, 18)]
	public string CNPJ { get; set; }

	/// <summary>
	/// Phone Number. Must contain only numbers
	/// </summary>
	[Required]
	[Phone]
	public string PhoneNumber { get; set; }

	[EmailAddress]
	public string Email { get; set; }

	[MaxLength(255)]
	public string description { get; set; }

	/// <summary>
	/// The DateTimes of the week where the business is open
	/// </summary>
	public DateTime[,] businessDays { get; set; } = new DateTime[2, 7];

	[MaxLength(16)]
	public string[] Services { get; set; }

	public bool allowListedServicesOnly { get; set; }
	public bool holidayOpen { get; set; }
	public bool domicileService { get; set; }

	public BusinessInfo() {
		Name = string.Empty;
		CNPJ = string.Empty;
		PhoneNumber = string.Empty;
		Email = string.Empty;
		description = string.Empty;
		Services = Array.Empty<string>();
	}
}
