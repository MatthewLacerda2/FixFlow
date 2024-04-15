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
                context.AddFailure("Password is invalid");
            }
            if (password.Length < 7)
            {
                context.AddFailure("Password must be at least 7 characters long");
            }
        });

        RuleFor(x => x.newPassword).Custom((newPassword, context) =>
        {
            if (StringChecker.IsPasswordStrong(newPassword))
            {
                context.AddFailure("Password must contain an upper case, lower case, number and special character");
            }
            if (newPassword.Length < 7)
            {
                context.AddFailure("Password must be at least 7 characters long");
            }
        });
    }
}