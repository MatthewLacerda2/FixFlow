using FluentValidation;
using Server.Models.Appointments;
using Server.Models.Erros;

namespace Server.Validators.Appointments;

public class CreateAptScheduleValidator : AbstractValidator<CreateAptSchedule> {

	public CreateAptScheduleValidator() {

		RuleFor(x => x.dateTime).GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage(ValidatorErrors.DateMustBeInTheFuture);
		RuleFor(x => x.dateTime).LessThanOrEqualTo(DateTime.UtcNow.AddMonths(7)).WithMessage(ValidatorErrors.DateIsTooFarInFuture);

	}
}
