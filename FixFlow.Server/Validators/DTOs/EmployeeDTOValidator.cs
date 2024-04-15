using FluentValidation;
using Server.Models.DTO;
using Server.Models.Utils;

public class EmployeeDTOValidator : AbstractValidator<EmployeeDTO>
{
    public EmployeeDTOValidator()
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

        RuleFor(x => x.salary).GreaterThanOrEqualTo(0).WithErrorCode("Salary must be equal or greater than 0");
    }
}