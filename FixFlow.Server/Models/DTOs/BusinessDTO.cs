namespace Server.Models.DTO;

public class BusinessDTO {

	public string Id { get; set; } = string.Empty;

	public BusinessWeek BusinessWeek { get; set; }
	public string[] Services { get; set; } = Array.Empty<string>();
	public bool AllowListedServicesOnly { get; set; }
	public bool OpenOnHolidays { get; set; }

	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string CNPJ { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;

	public BusinessDTO(Business business) {
		Id = business.Id;
		BusinessWeek = business.BusinessWeek;
		Services = business.services;
		AllowListedServicesOnly = business.allowListedServicesOnly;
		OpenOnHolidays = business.openOnHolidays;

		Name = business.Name;
		Email = business.Email!;
		CNPJ = business.CNPJ;
		PhoneNumber = business.PhoneNumber!;
	}
}
