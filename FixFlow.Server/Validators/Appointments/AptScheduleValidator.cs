using FluentValidation;
using Server.Models.Appointments;
using Server.Models.Erros;

namespace Server.Validators.Appointments;

public class AptScheduleValidator : AbstractValidator<AptSchedule> {

	public AptScheduleValidator() {

		RuleFor(x => x.dateTime).GreaterThanOrEqualTo(DateTime.Now).WithMessage(ValidatorErrors.DateHasntPassedYet);
		RuleFor(x => x.dateTime).LessThanOrEqualTo(DateTime.Now.AddMonths(7)).WithMessage(ValidatorErrors.DateIsTooFarInFuture);

	}
}
