using FluentValidation;
using Server.Models.DTO;
using Server.Models.Utils;

public class BusinessRegisterValidator : AbstractValidator<BusinessRegister>
{
    public BusinessRegisterValidator()
    {
        RuleFor(x => x.CPF).Custom((cpf, context) =>
        {
            if (cpf != null && StringChecker.isCPFvalid(cpf))
            {
                context.AddFailure("CPF invalid");
            }
        });

        RuleFor(x => x.UserName).Custom((userName, context) =>
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

        RuleFor(x => x.currentPassword).Custom((currentPassword, context) =>
        {
            if (StringChecker.IsPasswordStrong(currentPassword) == false)
            {
                context.AddFailure("Password must contain an upper case, lower case, number and special character");
            }
        });

        RuleFor(x => x.newPassword).Custom((newPassword, context) =>
        {
            if (newPassword != null && StringChecker.IsPasswordStrong(newPassword) == false)
            {
                context.AddFailure("Password must contain an upper case, lower case, number and special character");
            }
        });
    }
}