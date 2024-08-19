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

        RuleFor(x => x.newPassword).Custom((newPassword, context) =>
        {
            if(newPassword.Length < 8){
                context.AddFailure(ValidatorErrors.ShortPassword);
            }

            if (!string.IsNullOrEmpty(newPassword) && StringChecker.IsPasswordStrong(newPassword))
            {
                context.AddFailure(ValidatorErrors.BadPassword);
            }
        });

        RuleFor(x => x.newPassword).NotEqual(x => x.password).WithErrorCode(ValidatorErrors.NewPasswordSameAsOldOne);
    }
}