using Microsoft.AspNetCore.Identity;
using Server.Models.DTO;

namespace Server.Models;

public class Business : IdentityUser {

	public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

	public bool IsActive { get; set; } = true;

	/// <summary>
	/// The Name of the Business or Business owner
	/// </summary>
	/// <remarks>
	/// Idle Periods are allowed to overlap
	/// </remarks>
	public string Name { get; set; }

	/// <summary>
	/// CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
	/// </summary>
	public string CNPJ { get; set; }

	public string[] Services { get; set; } = Array.Empty<string>();
	public bool AllowListedServicesOnly { get; set; } = false;
	//TODO: public bool allowManyServices { get; set; } = false;
	public bool OpenOnHolidays { get; set; } = false;

	public Business() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }

	public Business(string name, string email, string cnpj, string phoneNumber) {
		Id = Guid.NewGuid().ToString();
		CreatedDate = DateTime.UtcNow;

		UserName = email;
		Name = name;
		Email = email;
		CNPJ = cnpj;
		PhoneNumber = phoneNumber;
	}

	public static explicit operator Business(BusinessRegisterRequest request) {
		return new Business(request.Name, request.Email, request.CNPJ, request.PhoneNumber);
	}
}
