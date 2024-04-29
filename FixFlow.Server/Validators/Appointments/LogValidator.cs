using FluentValidation;
using Server.Models.Appointments;

namespace Server.Validators;

public class LogValidator : AbstractValidator<AptLog>
{

    public LogValidator()
    {
        RuleFor(x => x.price).GreaterThanOrEqualTo(0).WithErrorCode("Price must be greater than 0");
        RuleFor(x => x.dateTime).GreaterThanOrEqualTo(new DateTime(2023, 1, 1)).WithErrorCode("Date must be from 2023 and forward");
        RuleFor(x => x.dateTime).LessThanOrEqualTo(DateTime.Now).WithErrorCode("Date hasn't even passed yet");
    }

}