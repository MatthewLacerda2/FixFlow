using Bogus;
using Bogus.Extensions.Brazil;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.DTO;

namespace Server.Bogus;

public class Generator {

	public int seed = 224466;

	DateTime jan1st2024 = new DateTime(2024, 1, 1, 8, 0, 0);

	public Generator(int _seed) {
		seed = _seed;
	}

	public Business[] GetFakeBusiness(int count) {

		Faker<Business> faker = new Faker<Business>()
								.UseSeed(seed)
								.StrictMode(false)
								.UseDateTimeReference(jan1st2024)
								.RuleFor(x => x.CreatedDate, jan1st2024)
								.RuleFor(x => x.Name, f => f.Company.CompanyName())
								.RuleFor(x => x.CNPJ, f => f.Company.Cnpj())

								//I just added these two lines and the two Faker<> functions below but still got this when running the migration:

								//Unable to create a 'DbContext' of type ''
								//The exception 'The seed entity for entity type 'Business' cannot be added because it has the navigation 'businessWeek' set.
								//To seed relationships,  add the entity seed to 'Business' and specify the foreign key values {'businessWeekId'}.
								.RuleFor(x => x.businessWeek, GetBusinessWeekFaker())
								.RuleFor(x => x.businessWeekId, (f, x) => x.businessWeek.Id)

								.RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###########"))
								.RuleFor(x => x.Email, f => f.Internet.Email())
								.RuleFor(x => x.NormalizedEmail, (f, x) => x.Email!.ToUpper())
								.RuleFor(x => x.UserName, (f, x) => x.Email)
								.RuleFor(x => x.NormalizedUserName, (f, x) => x.UserName!.ToUpper());

		return faker.Generate(count).ToArray();

	}

	public Faker<BusinessWeek> GetBusinessWeekFaker() {
		return new Faker<BusinessWeek>()
			.RuleFor(bw => bw.Id, f => Guid.NewGuid().ToString())
			.RuleFor(bw => bw.Sunday, f => GetBusinessTimeSpanFaker(false).Generate())
			.RuleFor(bw => bw.Monday, f => GetBusinessTimeSpanFaker(true).Generate())
			.RuleFor(bw => bw.Tuesday, f => GetBusinessTimeSpanFaker(true).Generate())
			.RuleFor(bw => bw.Wednesday, f => GetBusinessTimeSpanFaker(true).Generate())
			.RuleFor(bw => bw.Thursday, f => GetBusinessTimeSpanFaker(true).Generate())
			.RuleFor(bw => bw.Friday, f => GetBusinessTimeSpanFaker(true).Generate())
			.RuleFor(bw => bw.Saturday, f => GetBusinessTimeSpanFaker(true).Generate());
	}

	public Faker<BusinessTimeSpan> GetBusinessTimeSpanFaker(bool isActive) {
		return new Faker<BusinessTimeSpan>()
			.RuleFor(bts => bts.Id, f => Guid.NewGuid().ToString())
			.RuleFor(bts => bts.IsActive, f => isActive)
			.RuleFor(bts => bts.Start, f => new TimeSpan(8, 0, 0))
			.RuleFor(bts => bts.Finish, f => new TimeSpan(18, 0, 0));
	}

	public Customer[] GetFakeCustomers(int count, string businessId) {

		Faker<Customer> faker = new Faker<Customer>()
								.UseSeed(seed)
								.StrictMode(false)
								.UseDateTimeReference(jan1st2024)
								.RuleFor(x => x.BusinessId, businessId)
								.RuleFor(x => x.FullName, f => f.Name.FullName())
								.RuleFor(x => x.CPF, (f, x) => f.Random.Bool(0.05f) ? f.Person.Cpf() : null)
								.RuleFor(x => x.AdditionalNote, (f, x) => f.Person.Random.Words())

								.RuleFor(x => x.UserName, (f, x) => GenerateUserName(x.FullName, x.Id))
								.RuleFor(x => x.NormalizedUserName, (f, x) => x.UserName!.ToUpper())
								.RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###########"))
								.RuleFor(x => x.Email, (f, x) => f.Random.Bool(0.15f) ? f.Internet.Email() : null)
								.RuleFor(x => x.NormalizedEmail, (f, x) => x.Email != null ? x.Email.ToUpper() : null);

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
									.StrictMode(false)
									.UseDateTimeReference(jan1st2024)
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
								.RuleFor(x => x.CustomerId, customerId)
								.RuleFor(x => x.BusinessId, businessId)
								.RuleFor(x => x.dateTime, jan1st2024)
								.RuleFor(x => x.Description, (f, c) => f.Random.Bool(0.05f) ? f.Random.Words() : null)
								.RuleFor(x => x.Price, f => f.Random.Float(20, 300));

		return faker.Generate(count).ToArray();

	}
}
