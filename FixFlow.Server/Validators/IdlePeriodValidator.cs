using FluentValidation;
using Server.Models;
using Server.Models.Erros;

namespace FixFlow.Server.Validators;

public class IdlePeriodValidator : AbstractValidator<IdlePeriod> {
	public IdlePeriodValidator() {

		RuleFor(idlePeriod => idlePeriod.Start)
			.LessThan(idlePeriod => idlePeriod.Finish)
			.WithMessage(ValidatorErrors.StartMustBeOlderThanFinish);

		RuleFor(idlePeriod => idlePeriod.Finish)
			.LessThan(idlePeriod => DateTime.UtcNow)
			.WithMessage(ValidatorErrors.IdlePeriodHasPassed);

		RuleFor(x => x.Name).Custom((Name, context) => {
			if (string.IsNullOrEmpty(Name)) {
				context.AddFailure(nameof(Name) + ValidatorErrors.MustNotBeEmpty);
			}
		});
	}
}
