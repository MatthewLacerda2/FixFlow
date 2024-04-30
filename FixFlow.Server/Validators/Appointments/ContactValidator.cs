using FluentValidation;
using Server.Models.Appointments;

namespace Server.Validators;

public class ContactValidator : AbstractValidator<AptContact>
{

    public ContactValidator()
    {
        RuleFor(x => x.dateTime).GreaterThan(DateTime.Now).WithErrorCode("Date has to be in the future");
    }

}