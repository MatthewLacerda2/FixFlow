using FluentValidation;
using Server.Models.Appointments;

namespace Server.Validators;

public class ReminderValidator : AbstractValidator<AptReminder>
{

    public ReminderValidator()
    {
        RuleFor(x => x.dateTime).GreaterThan(DateTime.Now).WithErrorCode("Date has to be in the future");
    }

}