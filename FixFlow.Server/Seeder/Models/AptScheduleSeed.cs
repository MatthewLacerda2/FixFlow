using Bogus;
using Server.Models.Appointments;
using Server.Seeder.WaveFunctions;

namespace Server.Seeder;

public class AptScheduleSeed
{
    private readonly Faker<AptSchedule> _faker;

    public AptScheduleSeed()
    {
        _faker = new Faker<AptSchedule>()
        .UseSeed(WaveFunction.BogusSeed)
        .StrictMode(true)
        .RuleFor(a => a.Id, f => Guid.NewGuid().ToString())
        .RuleFor(a => a.Observation, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty);
    }

    public AptSchedule Generate(string clientId, string reminderId, DateTime minDateTime, DateTime maxDateTime, string observation)
    {

        return _faker
            .RuleFor(a => a.ClientId, f => clientId)
            .RuleFor(a => a.reminderId, f => reminderId)
            .RuleFor(a => a.DateTime, f => f.Date.Between(minDateTime, maxDateTime))
            .Generate();
    }
}