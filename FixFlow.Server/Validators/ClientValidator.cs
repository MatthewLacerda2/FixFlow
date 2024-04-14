using FluentValidation;
using Server.Models;
using Server.Models.Utils;

public class ClientValidator : AbstractValidator<Client>
{
    public ClientValidator()
    {
        RuleFor(x => x.CreatedDate).GreaterThanOrEqualTo(new DateTime(2023, 1, 1)).WithErrorCode("Date must be from 2023 and forward");
        RuleFor(x => x.LastLogin).GreaterThanOrEqualTo(x => x.CreatedDate).WithErrorCode("Last Login cannot be earlier than Creation Date");

        RuleFor(x => x.FullName).Custom((fullname, context) =>
        {

            if (StringChecker.IsFullNameValid(fullname))
            {
                context.AddFailure("Fullname invalid");
            }

        });

        RuleFor(x => x.CPF).Custom((cpf, context) =>
        {
            if (StringChecker.isCPFvalid(cpf))
            {
                context.AddFailure("CPF invalid");
            }
        });
    }
}