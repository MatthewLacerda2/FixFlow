using FluentValidation;
using Server.Models.Appointments;

namespace Server.Validators.Appointments;

public class ScheduleValidator : AbstractValidator<AptSchedule>
{

    public ScheduleValidator()
    {
        RuleFor(x => x.price).GreaterThanOrEqualTo(0).WithErrorCode("Price must be greater than 0");
        RuleFor(x => x.dateTime).GreaterThanOrEqualTo(new DateTime(2024, 1, 1)).WithErrorCode("Date must be from 2023 and forward");
        RuleFor(x => x.dateTime).LessThanOrEqualTo(DateTime.Now).WithErrorCode("Date hasn't even passed yet");
    }
}