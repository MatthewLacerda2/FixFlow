using FluentValidation;
using Server.Models.Appointments;
using Server.Models.Erros;

namespace Server.Validators.Appointments;

public class AptScheduleValidator : AbstractValidator<AptSchedule> {

	public AptScheduleValidator() {

		RuleFor(x => x.dateTime).GreaterThanOrEqualTo(new DateTime(2024, 1, 1)).WithMessage(ValidatorErrors.DateMustBe2024orForward);
		RuleFor(x => x.dateTime).LessThanOrEqualTo(DateTime.Now).WithMessage(ValidatorErrors.DateHasntPassedYet);

	}
}
