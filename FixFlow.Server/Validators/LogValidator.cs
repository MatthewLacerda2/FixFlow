using FluentValidation;
using Server.Models.Appointments;

namespace Server.Validators;

public class LogValidator : AbstractValidator<AppointmentLog>
{

    public LogValidator()
    {
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.DateTime).GreaterThanOrEqualTo(new DateTime(2024, 1, 1));
    }

}