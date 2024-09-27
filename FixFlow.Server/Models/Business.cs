using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Server.Models.DTO;

namespace Server.Models;

public class Business : IdentityUser {

	public DateTime CreatedDate { get; set; }
	public DateTime LastLogin { get; set; }

	public bool IsActive = true;

	/// <summary>
	/// The Name of the Business or Business owner
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
	/// </summary>
	public string CNPJ { get; set; }

	[NotMapped]
	public string token { get; set; } = string.Empty;

	/// <summary>
	/// The DateTimes of the week where the business is open
	/// </summary>
	public DateTime[,] BusinessDays { get; set; } = new DateTime[2, 7];

	public Business() {
		CreatedDate = DateTime.Now;
		LastLogin = DateTime.Now;

		Name = string.Empty;
		CNPJ = string.Empty;
	}

	public Business(string name, string email, string cnpj, string phoneNumber) {
		CreatedDate = DateTime.Now;
		LastLogin = DateTime.Now;

		Name = name;
		Email = email;
		CNPJ = cnpj;
		PhoneNumber = phoneNumber;
	}

	public static explicit operator Business(BusinessRegisterRequest request) {
		return new Business(request.Name, request.Email, request.CNPJ, request.PhoneNumber);
	}
}
