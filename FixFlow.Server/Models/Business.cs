using Microsoft.AspNetCore.Identity;
using Server.Models.DTO;

namespace Server.Models;

public class Business : IdentityUser {
	public DateTime CreatedDate { get; set; }
	public DateTime LastLogin { get; set; }

	/// <summary>
	/// The Name of the Business or Business owner
	/// </summary>
	public string Name { get; set; }

	public string CPF { get; set; }

	public string? CNPJ { get; set; }

	public string description { get; set; }

	public Business() {
		CreatedDate = DateTime.Now;
		LastLogin = DateTime.Now;

		Name = string.Empty;
		CPF = string.Empty;
		description = string.Empty;
	}

	public Business(string name, string cpf, string cnpj, string phonenumber, string email, string _description) {
		CreatedDate = DateTime.Now;
		LastLogin = DateTime.Now;

		Name = name;
		CPF = cpf;
		CNPJ = cnpj;

		PhoneNumber = phonenumber;
		Email = email;
		description = _description;
	}

	public Business(BusinessRegister business) {
		CreatedDate = DateTime.Now;
		LastLogin = DateTime.Now;

		Name = business.Name;
		CPF = business.CPF;
		CNPJ = business.CNPJ;

		PhoneNumber = business.PhoneNumber;
		Email = business.Email;
		description = business.description;
	}
}
