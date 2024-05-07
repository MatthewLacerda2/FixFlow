using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.Appointments;
namespace Server.Seeder;

/// <summary>
/// 
/// Let me make something very clear:
/// This is NOT meant to precisely simulate real-world behavior. It's more to test API, DB and Performance
/// 
/// The data generated DOES make sense, just the behavior that isn't ground-truth
/// What we want is to generate a BUNCH of data
/// 
/// This generator is here to help test everything before launch, which is indeed required
/// 
/// </summary>

public class FlowSeeder
{

    readonly static DateTime Jan2nd2023 = new DateTime(2023, 1, 2, 8, 0, 0);

    public Business[] businesses { get; } = [];
    public Client[] clients { get; } = [];

    public AptContact[] aptContacts { get; set; } = [];
    public AptSchedule[] aptSchedules { get; set; } = [];
    public AptLog[] aptLogs { get; set; } = [];

    const int businesCount = 55;
    const int clientsCount = businesCount * 55;

    int bogusSeed;

    public FlowSeeder(ModelBuilder builder, int seed)
    {
        bogusSeed = seed;

        businesses = GenerateBusinesses(businesCount);
        clients = GenerateClients(clientsCount);

        GenerateApts();

        builder.Entity<Business>().HasData(businesses);
        builder.Entity<Client>().HasData(clients);

        builder.Entity<AptSchedule>().HasData(aptSchedules);
        builder.Entity<AptLog>().HasData(aptLogs);
        builder.Entity<AptContact>().HasData(aptContacts);

    }

    void GenerateApts()
    {
        Faker<AptSchedule> faker_schedules = ScheduleFaker();
        Faker<AptLog> faker_logs = LogFaker();
        Faker<AptContact> faker_contacts = ContactFaker();

        const int num = 3;

        int totalClients = clients.Length;
        int index = 0;
        int indexBusiness = 1;

        aptSchedules = new AptSchedule[totalClients * num];
        aptLogs = new AptLog[totalClients * num];
        aptContacts = new AptContact[totalClients * num];

        foreach (Client cl in clients)
        {
            faker_schedules
            .RuleFor(s => s.ClientId, cl.Id)
            .RuleFor(s => s.businessId, businesses[indexBusiness % indexBusiness].Id);

            faker_logs
            .RuleFor(s => s.ClientId, cl.Id)
            .RuleFor(s => s.businessId, businesses[indexBusiness % indexBusiness].Id);

            faker_contacts
            .RuleFor(s => s.ClientId, cl.Id)
            .RuleFor(s => s.businessId, businesses[indexBusiness % indexBusiness].Id);

            AptSchedule[] schs2add = faker_schedules.Generate(num).ToArray();
            AptLog[] logs2add = faker_logs.Generate(num).ToArray();
            AptContact[] conts2add = faker_contacts.Generate(num).ToArray();

            int myMonthSpan = (6 % indexBusiness) + 1;

            for (int i = 0; i < num; i++)
            {

                schs2add[i].dateTime = schs2add[i].dateTime.AddMonths(myMonthSpan * i);
                schs2add[i].contactId = conts2add[i - 1].Id;

                logs2add[i].scheduleId = schs2add[i].Id;
                logs2add[i].dateTime = schs2add[i].dateTime.AddHours(1);

                conts2add[i].aptLogId = logs2add[i].Id;
                conts2add[i].dateTime = schs2add[i].dateTime.AddMonths((myMonthSpan * i) + 1).AddDays(-1);

            }

            conts2add[0].aptLogId = "";
            schs2add[0].contactId = null;
            logs2add[0].scheduleId = null;

            Array.Copy(schs2add, 0, aptSchedules, index, num);
            Array.Copy(logs2add, 0, aptLogs, index, num);
            Array.Copy(conts2add, 0, aptContacts, index, num);

            index += num;
            indexBusiness++;
        }

        //I did it this way to be more readable

        aptContacts = aptContacts.Where(c => c.aptLogId != "").ToArray();
    }

    Business[] GenerateBusinesses(int amount)
    {
        var business_faker = new Faker<Business>()
        .UseSeed(bogusSeed)
        .StrictMode(true)
        .UseDateTimeReference(Jan2nd2023)

        .RuleFor(e => e.Id, f => f.Random.Guid().ToString())
        .RuleFor(e => e.Name, f => f.Name.FullName())
        .RuleFor(e => e.CPF, f => f.Person.Cpf())
        .RuleFor(e => e.CreatedDate, f => f.Date.Between(Jan2nd2023, Jan2nd2023.AddDays(1)))
        .RuleFor(e => e.LastLogin, f => f.Date.Between(DateTime.Now.AddDays(-60), DateTime.Now))
        .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.Name.ToLower()))
        .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("###########"))

        .RuleFor(e => e.CNPJ, f => f.Company.Cnpj())
        .RuleFor(e => e.description, f => f.Commerce.Department())

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

        var generatedBusinesses = business_faker.Generate(amount).ToArray();

        return generatedBusinesses;
    }

    Client[] GenerateClients(int amount)
    {
        var clients_faker = new Faker<Client>()
        .UseSeed(bogusSeed)
        .StrictMode(true)
        .UseDateTimeReference(Jan2nd2023)

        .RuleFor(e => e.Id, f => Guid.NewGuid().ToString())
        .RuleFor(c => c.FullName, f => f.Name.FullName())
        .RuleFor(e => e.CreatedDate, f => f.Date.Between(Jan2nd2023, Jan2nd2023.AddDays(1)))
        .RuleFor(e => e.LastLogin, f => f.Date.Between(DateTime.Now.AddDays(-60), DateTime.Now))
        .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("###########"))

        .RuleFor(c => c.additionalNote, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty)
        .RuleFor(c => c.signedUp, f => f.Random.Bool(0.4f))

        .RuleFor(e => e.UserName, f => f.Internet.UserName())
        .RuleFor(e => e.NormalizedUserName, (f, e) => e.UserName!.ToUpper())

        .RuleFor(e => e.NormalizedEmail, (f, e) => e.Email!.ToUpper())
        .RuleFor(e => e.EmailConfirmed, false)

        .RuleFor(c => c.CPF, (f, c) => c.signedUp || f.Random.Bool(0.45f) ? f.Person.Cpf() : string.Empty)
        .RuleFor(c => c.Email, (f, c) => c.signedUp || f.Random.Bool(0.85f) ? f.Internet.Email(c.FullName.ToLower()) : string.Empty)

        .RuleFor(e => e.PasswordHash, (f, c) => c.signedUp ? f.Random.Guid().ToString().Replace("-", "/") : string.Empty)
        .RuleFor(e => e.AccessFailedCount, 0)
        .RuleFor(e => e.SecurityStamp, "")
        .RuleFor(e => e.ConcurrencyStamp, "")
        .RuleFor(e => e.PhoneNumberConfirmed, false)
        .RuleFor(e => e.TwoFactorEnabled, false)
        .RuleFor(e => e.LockoutEnabled, false)
        .RuleFor(e => e.LockoutEnd, DateTimeOffset.MinValue);

        var clients = clients_faker.Generate(amount).ToArray();

        return clients;
    }

    Faker<AptSchedule> ScheduleFaker()
    {

        var schedules_faker = new Faker<AptSchedule>()
        .UseSeed(bogusSeed)
        .StrictMode(false)
        .UseDateTimeReference(Jan2nd2023)

        .RuleFor(e => e.Id, f => f.Random.Guid().ToString())
        .RuleFor(s => s.price, f => f.Random.Int(30, 100))
        .RuleFor(x => x.dateTime, f => f.Date.Between(Jan2nd2023, Jan2nd2023.AddDays(5)))
        .RuleFor(a => a.observation, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty);

        return schedules_faker;

    }

    Faker<AptLog> LogFaker()
    {
        var logs_faker = new Faker<AptLog>()
        .UseSeed(bogusSeed)
        .StrictMode(false)
        .UseDateTimeReference(Jan2nd2023)

        .RuleFor(e => e.Id, f => f.Random.Guid().ToString())
        .RuleFor(s => s.price, f => f.Random.Int(30, 300))
        .RuleFor(a => a.observation, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty);

        return logs_faker;
    }

    Faker<AptContact> ContactFaker()
    {
        var contact_faker = new Faker<AptContact>()
        .UseSeed(bogusSeed)
        .StrictMode(false)
        .UseDateTimeReference(Jan2nd2023)

        .RuleFor(e => e.Id, f => f.Random.Guid().ToString());

        return contact_faker;
    }
}