using Bogus;
using Bogus.Extensions.Brazil;
using Server.Models;

namespace Server.Seeder;

public class EmployeeSeed
{
    private readonly Faker<Employee> _faker;

    public EmployeeSeed()
    {
        _faker = new Faker<Employee>()
            .RuleFor(e => e.FullName, f => f.Name.FullName())
            .RuleFor(e => e.CPF, f => f.Person.Cpf())
            .RuleFor(e => e.CreatedDate, f => f.Date.Between(new DateTime(DateTime.Now.Year - 1, 1, 1), new DateTime(DateTime.Now.Year - 1, 1, 31)))
            .RuleFor(e => e.LastLogin, f => f.Date.Between(DateTime.Now.Date.AddDays(-30), DateTime.Now.Date))
            .RuleFor(e => e.salary, f => f.Random.Float(1500, 5000))
            .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FullName.ToLower()))
            .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("###########"));
    }

    public Employee Generate()
    {
        return _faker.Generate();
    }
}
