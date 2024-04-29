using Bogus;
using Bogus.Extensions.Brazil;
using Server.Models;
using Server.Models.Appointments;
namespace Server.Seeder;

public class FlowSeeder
{

    /// <summary>
    /// 
    /// Let me make something very clear:
    /// This is NOT meant to simulate real-world behavior. It's more to test API, DB and Performance
    /// 
    /// It's meant to generate a BUNCH of data. We'd like to simulate good behavior but don't expect it
    /// Doing so would be complex and require more effort than it's worth
    /// 
    /// This is being done to begin with to test everything before it launches, which is indeed required
    /// 
    /// </summary>

    readonly static DateTime Jan2nd2023 = new DateTime(2023, 1, 2, 8, 0, 0);

    public Employee[] employees { get; } = [];
    public Client[] clients { get; } = [];

    public AptReminder[] aptReminders { get; set; } = [];
    public AptSchedule[] aptSchedules { get; set; } = [];
    public AptLog[] aptLogs { get; set; } = [];

    const int employeesCount = 50;
    const int clientsCount = employeesCount * 50;

    const int bogusSeed = 777;

    public FlowSeeder()
    {
        employees = GenerateEmployees(employeesCount);
        clients = GenerateClients(clientsCount);

        GenerateApts();
    }

    void GenerateApts()
    {

        Faker<AptSchedule> faker_schedules = ScheduleFaker();
        Faker<AptLog> faker_logs = LogFaker();
        Faker<AptReminder> faker_reminders = ReminderFaker();

        List<AptSchedule> schedules = new List<AptSchedule>();
        List<AptLog> logs = new List<AptLog>();
        List<AptReminder> reminders = new List<AptReminder>();

        foreach (Client cl in clients)
        {
            faker_schedules
            .RuleFor(s => s.ClientId, cl.Id)
            .RuleFor(s => s.reminderId, "")
            .RuleFor(s => s.Price, f => f.Random.Int(30, 100))
            .RuleFor(x => x.DateTime, f => f.Date.Between(Jan2nd2023, Jan2nd2023.AddDays(5)));

            faker_logs
            .RuleFor(s => s.ClientId, cl.Id)
            .RuleFor(s => s.Price, f => f.Random.Int(30, 300));

            faker_reminders
            .RuleFor(s => s.ClientId, cl.Id);

            const int num = 3;
            const int period = 3;

            AptSchedule[] schs2add = faker_schedules.Generate(num).ToArray();
            AptLog[] logs2add = faker_logs.Generate(num).ToArray();
            AptReminder[] rems2add = faker_reminders.Generate(num).ToArray();

            for (int i = 0; i < num; i++)
            {
                schs2add[i].DateTime = schs2add[i].DateTime.AddMonths(period);

                logs2add[i].ScheduleId = schs2add[i].Id;
                logs2add[i].DateTime = schs2add[i].DateTime.AddMonths(period).AddHours(1);

                rems2add[i].previousAppointmentId = logs2add[i].Id;
                rems2add[i].dateTime = schs2add[i].DateTime.AddMonths(period + 1).AddDays(-1);
            }

            for (int i = 1; i < num; i++)
            {
                schs2add[i].reminderId = rems2add[i].Id;
            }

            schedules.AddRange(schs2add);
            logs.AddRange(logs2add);
            reminders.AddRange(rems2add);

        }

        aptSchedules = schedules.ToArray();
        aptLogs = logs.ToArray();
        aptReminders = reminders.ToArray();
    }

    Employee[] GenerateEmployees(int amount)
    {
        var employees_faker = new Faker<Employee>()
        .UseSeed(bogusSeed)
        .StrictMode(false)
        .UseDateTimeReference(Jan2nd2023)

        .RuleFor(e => e.FullName, f => f.Name.FullName())
        .RuleFor(e => e.CPF, f => f.Person.Cpf())
        .RuleFor(e => e.CreatedDate, f => f.Date.Between(Jan2nd2023, Jan2nd2023.AddDays(1)))
        .RuleFor(e => e.LastLogin, f => f.Date.Between(DateTime.Now.AddDays(-60), DateTime.Now))
        .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FullName.ToLower()))
        .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("###########"))

        .RuleFor(e => e.salary, f => f.Random.Float(1500, 5000))

        .RuleFor(e => e.UserName, f => f.Internet.UserName())
        .RuleFor(e => e.NormalizedUserName, (f, e) => e.UserName!.ToUpper())

        .RuleFor(e => e.NormalizedEmail, (f, e) => e.Email!.ToUpper())
        .RuleFor(e => e.EmailConfirmed, false)

        .RuleFor(e => e.PasswordHash, Guid.NewGuid().ToString())
        .RuleFor(e => e.AccessFailedCount, 0)
        .RuleFor(e => e.SecurityStamp, "")
        .RuleFor(e => e.ConcurrencyStamp, "")
        .RuleFor(e => e.PhoneNumberConfirmed, false)
        .RuleFor(e => e.TwoFactorEnabled, false)
        .RuleFor(e => e.LockoutEnabled, false)
        .RuleFor(e => e.LockoutEnd, DateTimeOffset.MinValue);

        var employees = employees_faker.Generate(amount).ToArray();

        foreach (Employee emp in employees)
        {
            emp.Id = Guid.NewGuid().ToString();
        }

        return employees;
    }

    Client[] GenerateClients(int amount)
    {
        var clients_faker = new Faker<Client>()
        .UseSeed(bogusSeed)
        .StrictMode(false)
        .UseDateTimeReference(Jan2nd2023)

        .RuleFor(c => c.FullName, f => f.Name.FullName())
        .RuleFor(c => c.CPF, f => f.Person.Cpf())
        .RuleFor(e => e.CreatedDate, f => f.Date.Between(Jan2nd2023, Jan2nd2023.AddDays(1)))
        .RuleFor(e => e.LastLogin, DateTime.Now.AddDays(-60))
        .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FullName.ToLower()))
        .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("###########"))

        .RuleFor(c => c.additionalNote, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty)

        .RuleFor(e => e.UserName, f => f.Internet.UserName())
        .RuleFor(e => e.NormalizedUserName, (f, e) => e.UserName!.ToUpper())

        .RuleFor(e => e.NormalizedEmail, (f, e) => e.Email!.ToUpper())
        .RuleFor(e => e.EmailConfirmed, false)

        .RuleFor(e => e.PasswordHash, Guid.NewGuid().ToString())
        .RuleFor(e => e.AccessFailedCount, 0)
        .RuleFor(e => e.SecurityStamp, "")
        .RuleFor(e => e.ConcurrencyStamp, "")
        .RuleFor(e => e.PhoneNumberConfirmed, false)
        .RuleFor(e => e.TwoFactorEnabled, false)
        .RuleFor(e => e.LockoutEnabled, false)
        .RuleFor(e => e.LockoutEnd, DateTimeOffset.MinValue);

        var clients = clients_faker.Generate(amount).ToArray();

        foreach (Client cl in clients)
        {
            cl.Id = Guid.NewGuid().ToString();
        }

        return clients;
    }

    Faker<AptSchedule> ScheduleFaker()
    {

        var schedules_faker = new Faker<AptSchedule>()
        .UseSeed(bogusSeed)
        .StrictMode(false)
        .UseDateTimeReference(Jan2nd2023)

        .RuleFor(a => a.Observation, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty);

        return schedules_faker;

    }

    Faker<AptLog> LogFaker()
    {
        var logs_faker = new Faker<AptLog>()
        .UseSeed(bogusSeed)
        .StrictMode(false)
        .UseDateTimeReference(Jan2nd2023)

        .RuleFor(a => a.Observation, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty);

        return logs_faker;
    }

    Faker<AptReminder> ReminderFaker()
    {
        var reminders_faker = new Faker<AptReminder>()
        .UseSeed(bogusSeed)
        .StrictMode(false)
        .UseDateTimeReference(Jan2nd2023);

        return reminders_faker;
    }
}