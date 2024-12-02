using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO;

public class BusinessDTO {

	public string Id { get; set; } = string.Empty;

	public BusinessWeek BusinessWeek { get; set; }
	public string[] Services { get; set; } = Array.Empty<string>();
	public bool AllowListedServicesOnly { get; set; }
	public bool OpenOnHolidays { get; set; }

	public string Name { get; set; } = string.Empty;

	[EmailAddress]
	public string? Email { get; set; }

	public string CNPJ { get; set; } = string.Empty;

	[Phone]
	public string? PhoneNumber { get; set; }

	public BusinessDTO() : this(new Business()) { }

	public BusinessDTO(Business business) {
		Id = business.Id;
		BusinessWeek = business.BusinessWeek;
		Services = business.Services;
		AllowListedServicesOnly = business.AllowListedServicesOnly;
		OpenOnHolidays = business.OpenOnHolidays;

		Name = business.Name;
		Email = business.Email!;
		CNPJ = business.CNPJ;
		PhoneNumber = business.PhoneNumber!;
	}
}
