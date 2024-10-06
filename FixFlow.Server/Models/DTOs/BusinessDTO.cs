namespace Server.Models.DTO;

public class BusinessDTO {

	public string Id { get; set; } = string.Empty;

	public List<BusinessDay> BusinessDays { get; set; }
	public string[] Services { get; set; } = Array.Empty<string>();
	public bool AllowListedServicesOnly { get; set; }
	public bool OpenOnHolidays { get; set; }

	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string CNPJ { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;

	public BusinessDTO() {
		Id = Guid.NewGuid().ToString();
		BusinessDays = new List<BusinessDay>(7);
		for (int i = 0; i < 7; i++) {
			BusinessDays.Add(new BusinessDay());
		}
	}

	public BusinessDTO(Business business) {
		Id = business.Id;
		BusinessDays = business.BusinessDays;
		Services = business.services;
		AllowListedServicesOnly = business.allowListedServicesOnly;
		OpenOnHolidays = business.openOnHolidays;

		Name = business.Name;
		Email = business.Email!;
		CNPJ = business.CNPJ;
		PhoneNumber = business.PhoneNumber!;
	}
}
