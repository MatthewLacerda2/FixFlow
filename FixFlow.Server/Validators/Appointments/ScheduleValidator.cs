using FluentValidation;
using Server.Models.Appointments;

namespace Server.Validators;

public class ScheduleValidator : AbstractValidator<AppointmentSchedule>
{

    public ScheduleValidator()
    {
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithErrorCode("Price must be greater than 0");
        RuleFor(x => x.DateTime).GreaterThanOrEqualTo(new DateTime(2024, 1, 1)).WithErrorCode("Date must be from 2023 and forward");
        RuleFor(x => x.DateTime).LessThanOrEqualTo(DateTime.Now).WithErrorCode("Date hasn't even passed yet");
    }
}