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
			.WithMessage(ValidatorErrors.MinPriceIsGreaterThanMaxPrice);

		RuleFor(x => x.minDateTime)
			.GreaterThanOrEqualTo(new DateTime(2024, 1, 1))
			.WithMessage(ValidatorErrors.MinDateMustBeOlder);

		RuleFor(x => x.minDateTime)
			.LessThanOrEqualTo(x => x.maxDateTime)
			.WithMessage(ValidatorErrors.MinDateIsGreaterThanMaxDate);

		RuleFor(x => x.offset)
			.GreaterThanOrEqualTo(0)
			.WithMessage(ValidatorErrors.OffsetMustBeNaturalNumber);

		RuleFor(x => x.limit)
			.GreaterThan(0)
			.WithMessage(ValidatorErrors.LimitMustBeNaturalNumberGreaterThanZero);
	}
}
