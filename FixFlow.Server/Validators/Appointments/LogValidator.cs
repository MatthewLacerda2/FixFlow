using FluentValidation;
using Server.Models.Appointments;

namespace Server.Validators;

public class LogValidator : AbstractValidator<AppointmentLog>
{

    public LogValidator()
    {
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithErrorCode("Price must be greater than 0");
        RuleFor(x => x.DateTime).GreaterThanOrEqualTo(new DateTime(2023, 1, 1)).WithErrorCode("Date must be from 2023 and forward");
    }

}