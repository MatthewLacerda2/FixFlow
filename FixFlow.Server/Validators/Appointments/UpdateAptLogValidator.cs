using FluentValidation;
using Server.Models.Appointments;
using Server.Models.Erros;

namespace Server.Validators.Appointments;

public class UpdateAptLogValidator : AbstractValidator<UpdateAptLog> {

	public UpdateAptLogValidator() {
		RuleFor(x => x.Price)
			.GreaterThanOrEqualTo(0)
			.WithMessage(ValidatorErrors.PriceMustBeNaturalNumber);

		RuleFor(x => x.dateTime)
			.GreaterThanOrEqualTo(new DateTime(2024, 1, 1))
			.WithMessage(ValidatorErrors.DateMustBe2024orForward);
		RuleFor(x => x.dateTime)
			.LessThanOrEqualTo(DateTime.UtcNow)
			.WithMessage(ValidatorErrors.DateMustNotBeInTheFuture);

		RuleFor(x => x.Price)
			.GreaterThanOrEqualTo(0)
			.WithMessage(ValidatorErrors.PriceMustBeNaturalNumber);
	}
}
