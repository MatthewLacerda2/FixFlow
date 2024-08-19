using FluentValidation;
using Server.Models.Erros;
using Server.Models.PasswordReset;
using Server.Models.Utils;

namespace Server.Validators;

public class PasswordResetRequestValidator : AbstractValidator<PasswordReset>
{
    public PasswordResetRequestValidator()
    {
        RuleFor(x => x.password).Custom((password, context) =>
        {
            if(password.Length < 8) {
                context.AddFailure(ValidatorErrors.ShortPassword);
            }

            if (StringChecker.IsPasswordStrong(password))
            {
                context.AddFailure(ValidatorErrors.BadPassword);
            }
        });

        RuleFor(x => x.confirmPassword).Custom((newPassword, context) =>
        {
            if(newPassword.Length < 8) {
                context.AddFailure(ValidatorErrors.ShortPassword);
            }

            if (!string.IsNullOrEmpty(newPassword) && StringChecker.IsPasswordStrong(newPassword))
            {
                context.AddFailure(ValidatorErrors.BadPassword);
            }
        });

        RuleFor(x => x.confirmPassword).NotEqual(x => x.password).WithErrorCode(ValidatorErrors.NewPasswordSameAsOldOne);
    }
}