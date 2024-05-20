using FluentValidation;
using Server.Models.DTO;
using Server.Models.Utils;

namespace Server.Validators.DTOs;

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

        RuleFor(x => x.password).Custom((currentPassword, context) =>
        {
            if (StringChecker.IsPasswordStrong(currentPassword) == false)
            {
                context.AddFailure("Password must contain an upper case, lower case, number and special character");
            }
        });

        RuleFor(x => x.confirmPassword).Equal(x => x.password).WithErrorCode("ConfirmPassword must be identical to Password");
    }
}