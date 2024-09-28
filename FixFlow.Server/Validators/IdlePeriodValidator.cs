using FluentValidation;
using Server.Models;
using Server.Models.Erros;

namespace FixFlow.Server.Validators;

public class IdlePeriodValidator : AbstractValidator<IdlePeriod> {
	public IdlePeriodValidator() {

		RuleFor(idlePeriod => idlePeriod.start)
			.LessThan(idlePeriod => idlePeriod.finish)
			.WithMessage(ValidatorErrors.StartMustBeGreaterThanFinish);
	}
}
