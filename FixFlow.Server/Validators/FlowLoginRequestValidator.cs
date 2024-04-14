using FluentValidation;
using Server.Models;

public class LogValidator : AbstractValidator<FlowLoginRequest>
{
    public LogValidator()
    {
        RuleFor(x => x.password).NotNull().WithErrorCode("Password is null");
        RuleFor(x => x.password).NotEmpty().WithErrorCode("Password is empty");
    }
}