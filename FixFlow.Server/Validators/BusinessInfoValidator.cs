using FluentValidation;
using Server.Models;
using Server.Models.Erros;

namespace Server.Validators;

public class BusinessInfoValidator : AbstractValidator<BusinessInfo> {
	public BusinessInfoValidator() {
		RuleFor(x => x.businessDays).Custom((businessDays, context) => {
			if (businessDays.GetLength(0) != 2 || businessDays.GetLength(1) != 7) {
				context.AddFailure(ValidatorErrors.businessDayLength);
			}
		});

		RuleFor(x => x.Services).Custom((Services, context) => {
			foreach (var service in Services) {
				if (service.Length > 63) {
					context.AddFailure(ValidatorErrors.serviceLength);
				}
			}
		});
	}
}
