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

	/// <summary>
	/// The DateTimes of the week where the business is open
	/// </summary>
	public BusinessDay[] BusinessDays { get; set; } = new BusinessDay[7];

	public string[] services = Array.Empty<string>();
	public bool allowListedServicesOnly = false;
	public bool openOnHolidays = false;

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

		for (int i = 0; i < BusinessDays.Length; i++) {
			DateTime start = new DateTime(DateTime.Now.Year, 1, 1, 8, 0, 0);
			DateTime end = new DateTime(DateTime.Now.Year, 1, 1, 18, 0, 0);

			BusinessDays[i] = new BusinessDay(i, start, end);
		}
	}

	public static explicit operator Business(BusinessRegisterRequest request) {
		return new Business(request.Name, request.Email, request.CNPJ, request.PhoneNumber);
	}
}
