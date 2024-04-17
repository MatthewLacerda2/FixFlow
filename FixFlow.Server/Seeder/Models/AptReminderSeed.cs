using Bogus;
using Server.Models.Appointments;
using Server.Seeder.WaveFunctions;

namespace Server.Seeder;

public class AptReminderSeed
{
    private readonly Faker<AptReminder> _faker;

    public AptReminderSeed()
    {
        _faker = new Faker<AptReminder>()
        .UseSeed(WaveFunction.BogusSeed)
        .StrictMode(true)
        .RuleFor(a => a.Id, f => Guid.NewGuid().ToString());
    }

    public AptReminder Generate(string clientId, string previousAppointmentId, DateTime minDateTime, DateTime maxDateTime)
    {

        return _faker
            .RuleFor(a => a.ClientId, f => clientId)
            .RuleFor(a => a.previousAppointmentId, f => previousAppointmentId)
            .RuleFor(a => a.dateTime, f => f.Date.Between(minDateTime, maxDateTime))
            .Generate();
    }
}