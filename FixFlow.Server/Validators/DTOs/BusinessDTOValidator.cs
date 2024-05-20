using FluentValidation;
using Server.Models.DTO;
using Server.Models.Utils;

namespace Server.Validators.DTOs;

public class BusinessDTOValidator : AbstractValidator<BusinessDTO>
{
    public BusinessDTOValidator()
    {
        RuleFor(x => x.CPF).Custom((cpf, context) =>
        {
            if (cpf != null && StringChecker.isCPFvalid(cpf))
            {
                context.AddFailure("CPF invalid");
            }
        });

        RuleFor(x => x.Name).Custom((userName, context) =>
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                context.AddFailure("Username is empty");
            }
            if (userName.Contains(" "))
            {
                context.AddFailure("Username can not contain whitespaces");
            }
        });
    }
}