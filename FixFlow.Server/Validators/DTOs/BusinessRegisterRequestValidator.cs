using FluentValidation;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Validators.DTOs;

public class BusinessRegisterRequestValidator : AbstractValidator<BusinessRegisterRequest> {
	public BusinessRegisterRequestValidator() {
		RuleFor(x => x.Name).Custom((name, context) => {
			if (StringChecker.IsFullNameValid(name) == false) {
				context.AddFailure(ValidatorErrors.FullName);
			}
		});

		RuleFor(x => x.Password).Custom((currentPassword, context) => {
			if (currentPassword.Length < 7) {
				context.AddFailure(ValidatorErrors.ShortPassword);
			}

			if (StringChecker.IsPasswordStrong(currentPassword) == false) {
				context.AddFailure(ValidatorErrors.BadPassword);
			}
		});

		RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithErrorCode(ValidatorErrors.ConfirmPassword);
	}
}
