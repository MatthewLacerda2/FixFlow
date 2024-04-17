using Bogus;
using Bogus.Extensions.Brazil;
using Server.Models;

namespace Server.Seeder;

public class ClientSeed
{
    private readonly Faker<Client> _faker;

    public ClientSeed()
    {
        _faker = new Faker<Client>()
            .RuleFor(c => c.FullName, f => f.Name.FullName())
            .RuleFor(e => e.CPF, f => f.Person.Cpf())
            .RuleFor(c => c.CreatedDate, f => f.Date.Between(new DateTime(DateTime.Now.Year - 1, 1, 1), new DateTime(DateTime.Now.Year - 1, 1, 31)))
            .RuleFor(c => c.LastLogin, f => f.Date.Between(DateTime.Now.Date.AddDays(-30), DateTime.Now.Date))
            .RuleFor(e => e.additionalNote, f => f.Random.Bool(0.1f) ? f.Random.Words() : string.Empty)
            .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FullName.ToLower()))
            .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("###########"));
    }

    public Client Generate()
    {
        return _faker.Generate();
    }
}
