using FluentValidation;
using Server.Models.Appointments;

namespace Server.Validators.Appointments;

public class ContactValidator : AbstractValidator<AptContact>
{

    public ContactValidator()
    {
        RuleFor(x => x.dateTime).GreaterThanOrEqualTo(new DateTime(2023, 1, 1)).WithErrorCode("Date must be from 2023 and forward");
        RuleFor(x => x.dateTime).GreaterThan(DateTime.Now).WithErrorCode("Date has to be in the future");
    }

}