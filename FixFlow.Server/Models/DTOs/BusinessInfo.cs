using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class BusinessInfo {

	/// <summary>
	/// The Name of the Business or Business owner
	/// </summary>
	[Required]
	public string Name { get; set; }

	/// <summary>
	/// CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
	/// </summary>
	public string CNPJ { get; set; }

	public string description { get; set; }

	/// <summary>
	/// Phone Number. Must contain only numbers
	/// </summary>
	[Required]
	[Phone]
	public string PhoneNumber { get; set; }

	[EmailAddress]
	public string Email { get; set; }

	public BusinessInfo(string name, string cnpj, string _userName, string phonenumber, string email, string _description) {
		Name = name;
		CNPJ = cnpj;

		Name = _userName;
		PhoneNumber = phonenumber;
		Email = email;
		description = _description;
	}
}
