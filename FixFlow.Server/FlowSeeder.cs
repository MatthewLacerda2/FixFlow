using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.Appointments;
namespace Server.Seeder;

/// <summary>
/// 
/// Let me make something very clear:
/// This is meant to generate valid data, for unit tests, performance tests and seeding the DB
/// This is NOT meant to precisely simulate real-world behavior.
/// 
/// </summary>

public class FlowSeeder {

	readonly static DateTime Jan1st2024 = new DateTime(2024, 1, 1, 8, 0, 0);

	public Business[] businesses { get; } = [];
	public Client[] clients { get; } = [];

	public AptContact[] aptContacts { get; set; } = [];
	public AptSchedule[] aptSchedules { get; set; } = [];
	public AptLog[] aptLogs { get; set; } = [];

	const int businesCount = 50;
	const int clientsCount = businesCount * 50;

	int bogusSeed;

	public FlowSeeder(ModelBuilder builder, int seed) {
		bogusSeed = seed;

		businesses = GenerateBusinesses(businesCount);
		clients = GenerateClients(clientsCount);
		GenerateApts();

		builder.Entity<Business>().HasData(businesses);
		builder.Entity<Client>().HasData(clients);

		builder.Entity<AptContact>().HasData(aptContacts);
		builder.Entity<AptSchedule>().HasData(aptSchedules);
		builder.Entity<AptLog>().HasData(aptLogs);
	}

	void GenerateApts() {
		const int numAptsPerClient = 3;

		int totalClients = clients.Length;
		int indexBusiness = 1;
		int index = 0;

		aptContacts = new AptContact[totalClients * numAptsPerClient];
		aptSchedules = new AptSchedule[totalClients * numAptsPerClient];
		aptLogs = new AptLog[totalClients * numAptsPerClient];

		foreach (Client cl in clients) {
			Faker<AptContact> fakerContacts = GetContactFaker(cl.Id, businesses[indexBusiness % indexBusiness].Id, "fakeLogId", bogusSeed);
			Faker<AptSchedule> fakerSchedules = GetScheduleFaker(cl.Id, businesses[indexBusiness % indexBusiness].Id, null, bogusSeed);
			Faker<AptLog> fakerLogs = GetLogFaker(cl.Id, businesses[indexBusiness % indexBusiness].Id, null, bogusSeed);

			AptContact[] bogusContacts = fakerContacts.Generate(numAptsPerClient).ToArray();
			AptSchedule[] bogusSchedules = fakerSchedules.Generate(numAptsPerClient).ToArray();
			AptLog[] bogusLogs = fakerLogs.Generate(numAptsPerClient).ToArray();

			int monthSpan = (6 % indexBusiness) + 1;

			for (int i = 0; i < numAptsPerClient; i++) {

				bogusContacts[i].aptLogId = bogusLogs[i].Id;
				bogusContacts[i].dateTime = bogusSchedules[i].dateTime.AddMonths((monthSpan * i) + 1);

				bogusSchedules[i].dateTime = bogusSchedules[i].dateTime.AddMonths(monthSpan * i);

				bogusLogs[i].scheduleId = bogusSchedules[i].Id;

			}

			bogusContacts[0].aptLogId = "";
			bogusLogs[0].scheduleId = "";

			Array.Copy(bogusContacts, 0, aptContacts, index, numAptsPerClient);
			Array.Copy(bogusSchedules, 0, aptSchedules, index, numAptsPerClient);
			Array.Copy(bogusLogs, 0, aptLogs, index, numAptsPerClient);

			index += numAptsPerClient;
			indexBusiness++;
		}

		aptContacts = aptContacts.Where(c => c.aptLogId != "" || c.aptLogId != null).ToArray();
		aptLogs = aptLogs.Where(c => c.scheduleId != "" || c.scheduleId != null).ToArray();
	}

	Business[] GenerateBusinesses(int amount) {
		var faker_business = new Faker<Business>()
		.UseSeed(bogusSeed)
		.StrictMode(true)
		.UseDateTimeReference(Jan1st2024)

		.RuleFor(e => e.Id, f => f.Random.Guid().ToString())
		.RuleFor(e => e.Name, f => f.Name.FullName())
		.RuleFor(e => e.CreatedDate, f => f.Date.Between(Jan1st2024, Jan1st2024.AddDays(30)))
		.RuleFor(e => e.LastLogin, f => f.Date.Between(DateTime.Now.AddDays(-60), DateTime.Now))
		.RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.Name.ToLower()))
		.RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("###########"))

		.RuleFor(e => e.CNPJ, f => f.Company.Cnpj())

		.RuleFor(e => e.UserName, f => f.Internet.UserName())
		.RuleFor(e => e.NormalizedUserName, (f, e) => e.UserName!.ToUpper())

		.RuleFor(e => e.NormalizedEmail, (f, e) => e.Email!.ToUpper())
		.RuleFor(e => e.EmailConfirmed, false)

		.RuleFor(e => e.PasswordHash, f => f.Random.Guid().ToString().Replace("-", "/"))
		.RuleFor(e => e.AccessFailedCount, 0)
		.RuleFor(e => e.SecurityStamp, "")
		.RuleFor(e => e.ConcurrencyStamp, "")
		.RuleFor(e => e.PhoneNumberConfirmed, false)
		.RuleFor(e => e.TwoFactorEnabled, false)
		.RuleFor(e => e.LockoutEnabled, false)
		.RuleFor(e => e.LockoutEnd, DateTimeOffset.MinValue);

		var generatedBusinesses = faker_business.Generate(amount).ToArray();

		return generatedBusinesses;
	}

	Client[] GenerateClients(int amount) {
		var faker_clients = new Faker<Client>()
		.UseSeed(bogusSeed)
		.StrictMode(true)
		.UseDateTimeReference(Jan1st2024)

		.RuleFor(c => c.Id, f => f.Random.Guid().ToString())
		.RuleFor(c => c.FullName, f => f.Name.FullName())
		.RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("###########"))

		.RuleFor(c => c.UserName, f => f.Internet.UserName())
		.RuleFor(c => c.NormalizedUserName, (f, e) => e.UserName!.ToUpper())

		.RuleFor(c => c.NormalizedEmail, (f, c) => c.Email!.ToUpper())
		.RuleFor(c => c.EmailConfirmed, false)

		.RuleFor(c => c.AccessFailedCount, 0)
		.RuleFor(c => c.SecurityStamp, "")
		.RuleFor(c => c.ConcurrencyStamp, "")
		.RuleFor(c => c.PhoneNumberConfirmed, false)
		.RuleFor(c => c.TwoFactorEnabled, false)
		.RuleFor(c => c.LockoutEnabled, false)
		.RuleFor(c => c.LockoutEnd, DateTimeOffset.MinValue);

		var clients = faker_clients.Generate(amount).ToArray();

		return clients;
	}

	public static Faker<AptSchedule> GetScheduleFaker(string clientId, string businessId, string? contactId, int seed) {

		var schedules_faker = new Faker<AptSchedule>()
		.UseSeed(seed)
		.StrictMode(false)
		.UseDateTimeReference(Jan1st2024)

		.RuleFor(s => s.ClientId, clientId)
		.RuleFor(s => s.BusinessId, businessId)

		.RuleFor(s => s.Id, f => f.Random.Guid().ToString())
		.RuleFor(s => s.dateTime, f => f.Date.Between(Jan1st2024, Jan1st2024.AddDays(5)))
		.RuleFor(s => s.observation, f => f.Random.Bool(0.07f) ? f.Random.Words() : null);

		return schedules_faker;
	}

	public static Faker<AptLog> GetLogFaker(string clientId, string businessId, string? scheduleId, int seed) {
		var faker_logs = new Faker<AptLog>()
		.UseSeed(seed)
		.StrictMode(false)
		.UseDateTimeReference(Jan1st2024)

		.RuleFor(x => x.ClientId, clientId)
		.RuleFor(x => x.BusinessId, businessId)
		.RuleFor(x => x.scheduleId, scheduleId)

		.RuleFor(x => x.Id, f => f.Random.Guid().ToString())
		.RuleFor(x => x.dateTime, f => f.Date.Between(Jan1st2024, Jan1st2024.AddDays(5)))
		.RuleFor(x => x.Price, f => f.Random.Int(30, 300))
		.RuleFor(x => x.description, f => f.Random.Bool(0.1f) ? f.Random.Words() : null);

		return faker_logs;
	}

	public static Faker<AptContact> GetContactFaker(string clientId, string businessId, string aptLogId, int seed) {
		var faker_contact = new Faker<AptContact>()
		.UseSeed(seed)
		.StrictMode(false)
		.UseDateTimeReference(Jan1st2024)

		.RuleFor(c => c.ClientId, clientId)
		.RuleFor(c => c.businessId, businessId)
		.RuleFor(c => c.aptLogId, aptLogId)
		.RuleFor(c => c.dateTime, f => f.Date.Between(DateTime.Now.AddDays(30), DateTime.Now.AddDays(60)))

		.RuleFor(c => c.Id, f => f.Random.Guid().ToString());

		return faker_contact;
	}
}
