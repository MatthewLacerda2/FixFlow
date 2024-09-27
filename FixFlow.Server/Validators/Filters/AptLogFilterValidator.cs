using FluentValidation;
using Server.Models.Erros;
using Server.Models.Filters;

namespace Server.Validators.Appointments;

public class AptLogFilterValidator : AbstractValidator<AptLogFilter> {

	public AptLogFilterValidator() {
		RuleFor(x => x.minPrice)
			.GreaterThanOrEqualTo(0)
			.WithMessage(ValidatorErrors.PriceMustBeNaturalNumber);

		RuleFor(x => x.minPrice)
			.LessThanOrEqualTo(x => x.maxPrice)
			.WithMessage("Min Price must be less than or equal to Max Price");

		RuleFor(x => x.offset)
			.GreaterThanOrEqualTo(0)
			.WithMessage("Offset must be greater than or equal to 0");
	}
}
