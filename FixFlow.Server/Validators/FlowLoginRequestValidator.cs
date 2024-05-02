using FluentValidation;
using Server.Models;
using Server.Models.Utils;

public class LogValidator : AbstractValidator<FlowLoginRequest>
{
    public LogValidator()
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
    }
}