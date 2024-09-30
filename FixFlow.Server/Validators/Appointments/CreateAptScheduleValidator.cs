using FluentValidation;
using Server.Models.Appointments;
using Server.Models.Erros;

namespace Server.Validators.Appointments;

public class CreateAptScheduleValidator : AbstractValidator<CreateAptSchedule> {

	public CreateAptScheduleValidator() {

		RuleFor(x => x.dateTime).GreaterThanOrEqualTo(DateTime.Now).WithMessage(ValidatorErrors.DateMustBeInTheFuture);
		RuleFor(x => x.dateTime).LessThanOrEqualTo(DateTime.Now.AddMonths(7)).WithMessage(ValidatorErrors.DateIsTooFarInFuture);

	}
}
