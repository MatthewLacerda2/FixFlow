using Bogus;
using Server.Models.Appointments;
using Server.Models.Utils;
using Server.Seeder.WaveFunctions;

namespace Server.Seeder;

public class AptLogSeed
{
    private readonly Faker<AptLog> _faker;

    public AptLogSeed()
    {
        _faker = new Faker<AptLog>()
        .UseSeed(WaveFunction.BogusSeed)
        .StrictMode(true)
        .RuleFor(a => a.Id, f => Guid.NewGuid().ToString())
        .RuleFor(a => a.Status, CompletedStatus.Successfull)
        .RuleFor(a => a.Observation, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty);
    }

    public AptLog Generate(string clientId, string scheduleId, float price, DateTime minDateTime, DateTime maxDateTime)
    {
        float finalPrice = price;
        if (new Random().Next(0, 4) == 0)
        {
            finalPrice *= 2;
        }

        return _faker
            .RuleFor(a => a.ClientId, f => clientId)
            .RuleFor(a => a.ScheduleId, f => scheduleId)
            .RuleFor(a => a.Price, f => finalPrice)
            .RuleFor(a => a.DateTime, f => f.Date.Between(minDateTime, maxDateTime))
            .Generate();
    }
}