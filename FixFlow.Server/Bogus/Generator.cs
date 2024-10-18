using Bogus;
using Bogus.Extensions.Brazil;
using Server.Models;
using Server.Models.Appointments;

namespace Server.Bogus;

public class Generator {

	DateTime jan1st2024 = new DateTime(2024, 1, 1, 8, 0, 0);
	int seed = 224466;

	public Business[] GetFakeBusiness(int count) {

		Faker<Business> faker = new Faker<Business>()
								.UseSeed(seed)
								.StrictMode(true)
								.UseDateTimeReference(jan1st2024)
								.RuleFor(x => x.CreatedDate, jan1st2024)
								.RuleFor(x => x.Name, f => f.Company.CompanyName())
								.RuleFor(x => x.CNPJ, f => f.Company.Cnpj())

								.RuleFor(x => x.Id, f => f.Random.Guid().ToString())
								.RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###########"))
								.RuleFor(x => x.Email, f => f.Internet.Email())
								.RuleFor(x => x.NormalizedEmail, (f, x) => x.Email!.ToUpper())
								.RuleFor(x => x.UserName, (f, x) => x.Email)
								.RuleFor(x => x.NormalizedUserName, (f, x) => x.UserName!.ToUpper());

		return faker.Generate(count).ToArray();

	}

	public Customer[] GetFakeCustomers(int count, string businessId) {

		Faker<Customer> faker = new Faker<Customer>()
								.UseSeed(seed)
								.StrictMode(true)
								.UseDateTimeReference(jan1st2024)
								.RuleFor(x => x.BusinessId, businessId)
								.RuleFor(x => x.FullName, f => f.Name.FullName())
								.RuleFor(x => x.CPF, (f, x) => f.Random.Bool(0.05f) ? f.Person.Cpf() : null)
								.RuleFor(x => x.AdditionalNote, (f, x) => f.Person.Random.Words())

								.RuleFor(x => x.Id, f => f.Random.Guid().ToString())
								.RuleFor(x => x.UserName, (f, x) => GenerateUserName(x.FullName, x.Id))
								.RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###########"))
								.RuleFor(x => x.Email, (f, x) => f.Random.Bool(0.15f) ? f.Internet.Email() : null);

		return faker.Generate(count).ToArray();

	}

	string GenerateUserName(string fullName, string id) {
		var firstName = fullName.Split(' ')[0];
		var guidPrefix = id.Substring(0, 8);
		return $"{firstName}-{guidPrefix}";
	}

	public AptSchedule[] GetFakeSchedules(int count, string businessId, string customerId) {

		Faker<AptSchedule> faker = new Faker<AptSchedule>()
									.UseSeed(seed)
									.StrictMode(true)
									.UseDateTimeReference(jan1st2024)
									.RuleFor(x => x.Id, f => f.Random.Guid().ToString())
									.RuleFor(x => x.CustomerId, customerId)
									.RuleFor(x => x.BusinessId, businessId)
									.RuleFor(x => x.dateTime, jan1st2024)
									.RuleFor(x => x.Observation, (f, c) => f.Random.Bool(0.05f) ? f.Random.Words() : null)
									.RuleFor(x => x.Price, f => f.Random.Float(20, 300));

		return faker.Generate(count).ToArray();

	}

	public AptLog[] GetFakeLogs(int count, string businessId, string customerId) {

		Faker<AptLog> faker = new Faker<AptLog>()
								.UseSeed(seed)
								.StrictMode(false)
								.RuleFor(x => x.Id, f => f.Random.Guid().ToString())
								.RuleFor(x => x.CustomerId, customerId)
								.RuleFor(x => x.BusinessId, businessId)
								.RuleFor(x => x.dateTime, jan1st2024)
								.RuleFor(x => x.Description, (f, c) => f.Random.Bool(0.05f) ? f.Random.Words() : null)
								.RuleFor(x => x.Price, f => f.Random.Float(20, 300));

		return faker.Generate(count).ToArray();

	}

}
