using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Server.Models.DTO;

namespace Server.Models;

public class Business : IdentityUser {

	public DateTime CreatedDate { get; set; }
	public DateTime LastLogin { get; set; }

	public bool isActive = true;

	/// <summary>
	/// The Name of the Business or Business owner
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
	/// </summary>
	public string CNPJ { get; set; }

	public string Description { get; set; }

	[NotMapped]
	public string token { get; set; }

	/// <summary>
	/// The DateTimes of the week where the business is open
	/// </summary>
	public DateTime[,] BusinessDays { get; set; } = new DateTime[2, 7];

	[MaxLength(16)]
	public string[] Services { get; set; }

	public bool allowListedServicesOnly { get; set; } = false;
	public bool holidayOpen { get; set; } = false;
	public bool domicileService { get; set; } = false;

	public Business() {
		CreatedDate = DateTime.Now;
		LastLogin = DateTime.Now;

		Name = string.Empty;
		CNPJ = string.Empty;
		Description = string.Empty;
	}

	public Business(string name, string email, string cnpj, string phoneNumber, string description) {
		CreatedDate = DateTime.Now;
		LastLogin = DateTime.Now;

		Name = name;
		UserName = name.Replace(" ", string.Empty);
		Email = email;
		CNPJ = cnpj;
		PhoneNumber = phoneNumber;
		Description = description;
	}

	public static explicit operator Business(BusinessRegisterRequest request) {
		return new Business(request.Name, request.Email, request.CNPJ, request.PhoneNumber, request.description);
	}
}
