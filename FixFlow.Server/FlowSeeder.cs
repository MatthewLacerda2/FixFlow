using Bogus;
using Bogus.Extensions.Brazil;
using Server.Models;
using Server.Models.Appointments;
namespace Server.Seeder;

public class FlowSeeder
{

    readonly static DateTime Jan1st2023 = new DateTime(2023, 1, 1);

    public Employee[] employees { get; } = [];
    public Client[] clients { get; } = [];

    public AptReminder[] aptReminders { get; } = [];
    public AptSchedule[] aptSchedules { get; } = [];
    public AptLog[] aptLogs { get; } = [];

    const int employeesCount = 100;
    const int clientsCount = employeesCount * 100;

    const int bogusSeed = 777;

    public FlowSeeder()
    {
        employees = GenerateEmployees(employeesCount);
        clients = GenerateClients(clientsCount);

        GenerateApts();
    }

    Employee[] GenerateEmployees(int amount)
    {
        var employees_faker = new Faker<Employee>()
        .UseSeed(bogusSeed)
        .UseDateTimeReference(Jan1st2023)
        .StrictMode(true)
        .RuleFor(e => e.FullName, f => f.Name.FullName())
        .RuleFor(e => e.CPF, f => f.Person.Cpf())
        .RuleFor(e => e.CreatedDate, Jan1st2023)
        .RuleFor(e => e.LastLogin, DateTime.Now.AddDays(-1))
        .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FullName.ToLower()))
        .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("###########"))
        .RuleFor(e => e.salary, f => f.Random.Float(1500, 5000))

        .RuleFor(e => e.Id, Guid.NewGuid().ToString())
        .RuleFor(e => e.PasswordHash, Guid.NewGuid().ToString())

        .RuleFor(e => e.UserName, (f, e) => e.FullName.Replace(" ", ""))
        .RuleFor(e => e.NormalizedUserName, (f, e) => e.UserName!.ToUpper())

        .RuleFor(e => e.NormalizedEmail, (f, e) => e.Email!.ToUpper())
        .RuleFor(e => e.EmailConfirmed, false)
        .RuleFor(e => e.AccessFailedCount, 0)

        .RuleFor(e => e.SecurityStamp, "")
        .RuleFor(e => e.ConcurrencyStamp, "")
        .RuleFor(e => e.PhoneNumberConfirmed, false)
        .RuleFor(e => e.TwoFactorEnabled, false)
        .RuleFor(e => e.LockoutEnabled, false)
        .RuleFor(e => e.LockoutEnd, DateTimeOffset.MinValue);

        var employees = employees_faker.Generate(amount).ToArray();

        return employees;
    }

    Client[] GenerateClients(int amount)
    {
        var clients_faker = new Faker<Client>()
        .UseSeed(bogusSeed)
        .StrictMode(true)
        .UseDateTimeReference(Jan1st2023)
        .RuleFor(c => c.FullName, f => f.Name.FullName())
        .RuleFor(c => c.CPF, f => f.Person.Cpf())
        .RuleFor(e => e.CreatedDate, Jan1st2023)
        .RuleFor(e => e.LastLogin, DateTime.Now.AddDays(-1))
        .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FullName.ToLower()))
        .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("###########"))
        .RuleFor(c => c.additionalNote, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty)

        .RuleFor(e => e.Id, Guid.NewGuid().ToString())
        .RuleFor(e => e.PasswordHash, Guid.NewGuid().ToString())

        .RuleFor(e => e.UserName, (f, e) => e.FullName.Replace(" ", ""))
        .RuleFor(e => e.NormalizedUserName, (f, e) => e.UserName!.ToUpper())

        .RuleFor(e => e.NormalizedEmail, (f, e) => e.Email!.ToUpper())
        .RuleFor(e => e.EmailConfirmed, false)
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

    void GenerateApts()
    {

        List<AptSchedule> schedules = new List<AptSchedule>();
        List<AptLog> logs = new List<AptLog>();
        List<AptReminder> reminders = new List<AptReminder>();

        AptSchedule lastSch = new AptSchedule();
        AptLog lastLog = new AptLog();
        AptReminder lastRem = new AptReminder();

    }

    Faker<AptSchedule> ScheduleFaker()
    {

        var schedules_faker = new Faker<AptSchedule>()
        .UseSeed(bogusSeed)
        .StrictMode(true)
        .UseDateTimeReference(Jan1st2023)

        .RuleFor(a => a.Id, Guid.NewGuid().ToString())
        .RuleFor(a => a.Observation, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty);

        return schedules_faker;

    }

    Faker<AptLog> LogFaker()
    {
        var logs_faker = new Faker<AptLog>()
        .UseSeed(bogusSeed)
        .StrictMode(true)
        .UseDateTimeReference(Jan1st2023)

        .RuleFor(a => a.Id, Guid.NewGuid().ToString())
        .RuleFor(a => a.Observation, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty);

        return logs_faker;
    }

    Faker<AptReminder> ReminderFaker()
    {
        var reminders_faker = new Faker<AptReminder>()
        .UseSeed(bogusSeed)
        .StrictMode(true)
        .UseDateTimeReference(Jan1st2023)
        .RuleFor(a => a.Id, Guid.NewGuid().ToString());

        return reminders_faker;
    }
}
