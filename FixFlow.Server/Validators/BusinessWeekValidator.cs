using FluentValidation;
using Server.Models.DTO;
using Server.Models.Erros;

namespace FixFlow.Server.Validators;

public class BusinessTimeSpanValidator : AbstractValidator<BusinessTimeSpan> {

	public BusinessTimeSpanValidator() {

		RuleFor(x => x.Start)
			.LessThan(x => x.Finish)
			.WithMessage(ValidatorErrors.BusinessDayStartMustBeLessThanFinish);

	}
}
