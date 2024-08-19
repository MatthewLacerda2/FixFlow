using FluentValidation;
using Server.Models;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Validators;

public class FlowLoginRequestValidator : AbstractValidator<FlowLoginRequest>
{
    public FlowLoginRequestValidator()
    {
        RuleFor(x => x.password).Custom((password, context) =>
        {
            if(password.Length < 8){
                context.AddFailure(ValidatorErrors.ShortPassword);
            }

            if (StringChecker.IsPasswordStrong(password))
            {
                context.AddFailure(ValidatorErrors.BadPassword);
            }
        });
    }
}