using FluentValidation;
using Server.Models.Appointments;
using Server.Models.Erros;

namespace Server.Validators.Appointments;

public class AptLogValidator : AbstractValidator<AptLog> {

	public AptLogValidator() {
		RuleFor(x => x.price)
				.GreaterThanOrEqualTo(0)
				.WithErrorCode(ValidatorErrors.PriceMustBeNaturalNumber);

		RuleFor(x => x.dateTime)
				.GreaterThanOrEqualTo(new DateTime(2024, 1, 1))
				.WithErrorCode(ValidatorErrors.DateMustBe2023orForward);
		RuleFor(x => x.dateTime)
				.LessThanOrEqualTo(DateTime.Now)
				.WithErrorCode(ValidatorErrors.DateHasntPassedYet);
	}
}
