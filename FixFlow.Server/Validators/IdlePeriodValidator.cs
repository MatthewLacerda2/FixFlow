using FluentValidation;
using Server.Models;

namespace FixFlow.Server.Validators;

public class IdlePeriodValidator : AbstractValidator<IdlePeriod> {
	public IdlePeriodValidator() {

		RuleFor(idlePeriod => idlePeriod.start)
			.LessThan(idlePeriod => idlePeriod.finish)
			.WithMessage("Start time must be less than finish time.");
	}
}
