using FluentValidation;
using Server.Models;
using Server.Models.Utils;

namespace Server.Validators;

public class FlowLoginRequestValidator : AbstractValidator<FlowLoginRequest>
{
    public FlowLoginRequestValidator()
    {
        RuleFor(x => x.password).Custom((password, context) =>
        {
            if (StringChecker.IsPasswordStrong(password))
            {
                context.AddFailure("Password must contain an upper case, lower case, number and special character");
            }
        });

        RuleFor(x => x.newPassword).Custom((newPassword, context) =>
        {
            if (!string.IsNullOrEmpty(newPassword) && StringChecker.IsPasswordStrong(newPassword))
            {
                context.AddFailure("Password must contain an upper case, lower case, number and special character");
            }
        });

        RuleFor(x => x.newPassword).NotEqual(x => x.password).WithErrorCode("New password can not be the same as old one");
    }
}