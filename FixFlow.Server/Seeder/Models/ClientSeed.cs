using Bogus;
using Bogus.Extensions.Brazil;
using Server.Models;
using Server.Seeder.WaveFunctions;

namespace Server.Seeder;

public class ClientSeed
{
    private readonly Faker<Client> _faker;

    public ClientSeed()
    {
        _faker = new Faker<Client>()
        .UseSeed(WaveFunction.BogusSeed)
        .StrictMode(true)
        .RuleFor(c => c.FullName, f => f.Name.FullName())
        .RuleFor(c => c.CPF, f => f.Person.Cpf())
        .RuleFor(c => c.CreatedDate, f => f.Date.Between(new DateTime(DateTime.Now.Year - 1, 1, 1), new DateTime(DateTime.Now.Year - 1, 1, 31)))
        .RuleFor(c => c.LastLogin, f => f.Date.Between(DateTime.Now.Date.AddDays(-30), DateTime.Now.Date))
        .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FullName.ToLower()))
        .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("###########"))
        .RuleFor(c => c.additionalNote, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty);
    }

    public Client Generate()
    {
        return _faker.Generate();
    }
}
