using Microsoft.AspNetCore.Identity;

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

	public string description { get; set; }

	public Business() {
		CreatedDate = DateTime.Now;
		LastLogin = DateTime.Now;

		Name = string.Empty;
		CNPJ = string.Empty;
		description = string.Empty;
	}
}
