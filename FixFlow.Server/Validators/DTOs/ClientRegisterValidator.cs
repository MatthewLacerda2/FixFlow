using FluentValidation;
using Server.Models.DTO;
using Server.Models.Utils;

namespace Server.Validators.DTOs;

public class ClientRegisterValidator : AbstractValidator<ClientRegister>
{
    public ClientRegisterValidator()
    {
        RuleFor(x => x.FullName).Custom((fullname, context) =>
        {
            if (StringChecker.IsFullNameValid(fullname))
            {
                context.AddFailure("Fullname invalid");
            }
        });

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