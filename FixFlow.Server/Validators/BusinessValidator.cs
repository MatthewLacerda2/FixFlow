using FluentValidation;
using Server.Models;
using Server.Models.Erros;

namespace FixFlow.Server.Validators;

public class BusinessValidator : AbstractValidator<Business> {
	public BusinessValidator() {
		RuleFor(x => x.BusinessDays.Count).Equal(7).WithMessage(ValidatorErrors.BusinessDayCountMustBe7);

		RuleForEach(x => x.BusinessDays)
			.Must(x => x.Start < x.Finish)
			.WithMessage("All Business Days start time must be less than End time.");
	}
}
