using FluentValidation;
using Server.Models.PasswordReset;
using Server.Models.Utils;

namespace Server.Validators;

public class PasswordResetRequestValidator : AbstractValidator<PasswordResetRequest>
{
    public PasswordResetRequestValidator()
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

        RuleFor(x => x.newPassword).NotEqual(x => x.password).WithErrorCode("Password and Confirmation password must be identical");
    }
}