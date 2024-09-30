using Microsoft.AspNetCore.Identity;
using Server.Models.DTO;

namespace Server.Models;

public class Business : IdentityUser {

	public DateTime CreatedDate { get; set; }
	public DateTime LastLogin { get; set; }

	public bool IsActive { get; set; }

	/// <summary>
	/// The Name of the Business or Business owner
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
	/// </summary>
	public string CNPJ { get; set; }

	/// <summary>
	/// The DateTimes of the week where the business is open
	/// </summary>
	public List<BusinessDay> BusinessDays { get; set; } = new List<BusinessDay>();

	public string[] services = Array.Empty<string>();
	public bool allowListedServicesOnly { get; set; }
	public bool openOnHolidays { get; set; }

	public Business() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }

	public Business(string name, string email, string cnpj, string phoneNumber) {
		Id = Guid.NewGuid().ToString();
		CreatedDate = DateTime.Now;
		LastLogin = DateTime.Now;

		Name = name;
		Email = email;
		CNPJ = cnpj;
		PhoneNumber = phoneNumber;

		BusinessDays = new List<BusinessDay>(7);

		IsActive = true;
	}

	public static explicit operator Business(BusinessRegisterRequest request) {
		return new Business(request.Name, request.Email, request.CNPJ, request.PhoneNumber);
	}
}
