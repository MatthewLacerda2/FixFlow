using Bogus;
using Bogus.Extensions.Brazil;
using Server.Models;
using Server.Models.Appointments;
namespace Server.Seeder;

public class FlowSeeder
{

    const int bogusSeed = 777;

    const int employeesCount = 100;
    const int clientsCount = employeesCount * 100;

    public Employee[] employees { get; } = [];
    public Client[] clients { get; } = [];

    public AptReminder[] aptReminders { get; } = [];
    public AptSchedule[] aptSchedules { get; } = [];
    public AptLog[] aptLogs { get; } = [];

    public FlowSeeder()
    {
        employees = GenerateEmployees(employeesCount);
        clients = GenerateClients(clientsCount);

        aptSchedules = GenerateSchedules(clients, clientsCount);
        aptLogs = GenerateLogs(clients, clientsCount);
        aptReminders = GenerateReminders(clients, clientsCount);
    }

    private static Employee[] GenerateEmployees(int amount)
    {
        var employees_faker = new Faker<Employee>()
        .UseSeed(bogusSeed)
        .StrictMode(true)
        .RuleFor(e => e.FullName, f => f.Name.FullName())
        .RuleFor(e => e.CPF, f => f.Person.Cpf())
        .RuleFor(e => e.CreatedDate, f => f.Date.Between(new DateTime(DateTime.Now.Year - 1, 1, 1), new DateTime(DateTime.Now.Year - 1, 1, 31)))
        .RuleFor(e => e.LastLogin, f => f.Date.Between(DateTime.Now.Date.AddDays(-30), DateTime.Now.Date))
        .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FullName.ToLower()))
        .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("###########"))
        .RuleFor(e => e.salary, f => f.Random.Float(1500, 5000));

        var employees = employees_faker.Generate(amount).ToArray();

        return employees;
    }

    private static Client[] GenerateClients(int amount)
    {
        var clients_faker = new Faker<Client>()
        .UseSeed(bogusSeed)
        .StrictMode(true)
        .RuleFor(c => c.FullName, f => f.Name.FullName())
        .RuleFor(c => c.CPF, f => f.Person.Cpf())
        .RuleFor(c => c.CreatedDate, f => f.Date.Between(new DateTime(DateTime.Now.Year - 1, 1, 1), new DateTime(DateTime.Now.Year - 1, 1, 31)))
        .RuleFor(c => c.LastLogin, f => f.Date.Between(DateTime.Now.Date.AddDays(-30), DateTime.Now.Date))
        .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FullName.ToLower()))
        .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("###########"))
        .RuleFor(c => c.additionalNote, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty);

        var clients = clients_faker.Generate(amount).ToArray();

        return clients;
    }

    private static AptSchedule[] GenerateSchedules(Client[] clients, int amount)
    {
        var schedules__faker = new Faker<AptSchedule>()
        .UseSeed(bogusSeed)
        .StrictMode(true)
        .RuleFor(a => a.Id, f => Guid.NewGuid().ToString())
        .RuleFor(a => a.Observation, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty);

        //.RuleFor(a => a.ClientId, f => clientId)
        //.RuleFor(a => a.reminderId, f => reminderId)
        //.RuleFor(a => a.DateTime, f => f.Date.Between(minDateTime, maxDateTime));

        var schedules = schedules__faker.Generate(amount).ToArray();

        return schedules;
    }

    private static AptLog[] GenerateLogs(Client[] clients, int amount)
    {
        var logs_faker = new Faker<AptLog>()
        .UseSeed(bogusSeed)
        .StrictMode(true)
        .RuleFor(a => a.Id, f => Guid.NewGuid().ToString())
        .RuleFor(a => a.Observation, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty);

        float finalPrice = 40;
        if (new Random().Next(0, 4) == 0)
        {
            finalPrice *= 2;
        }

        logs_faker
            //.RuleFor(a => a.ClientId, f => clientId)
            //.RuleFor(a => a.ScheduleId, f => scheduleId)
            //.RuleFor(a => a.DateTime, f => f.Date.Between(minDateTime, maxDateTime))
            .RuleFor(a => a.Price, f => finalPrice);

        var logs = logs_faker.Generate(amount).ToArray();

        return logs;
    }

    private static AptReminder[] GenerateReminders(Client[] clients, int amount)
    {
        var reminders__faker = new Faker<AptReminder>()
        .UseSeed(bogusSeed)
        .StrictMode(true)
        .RuleFor(a => a.Id, f => Guid.NewGuid().ToString());

        var reminders = reminders__faker.Generate(amount).ToArray();

        return reminders;
    }
}
