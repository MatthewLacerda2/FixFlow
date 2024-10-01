using FluentValidation;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Validators.DTOs;

public class BusinessRegisterRequestValidator : AbstractValidator<BusinessRegisterRequest> {
	public BusinessRegisterRequestValidator() {
		RuleFor(x => x.Name).Custom((name, context) => {
			if (string.IsNullOrWhiteSpace(name)) {
				context.AddFailure(ValidatorErrors.UsernameIsEmpty);
			}
		});

		RuleFor(x => x.Password).Custom((currentPassword, context) => {
			if (currentPassword.Length < 8) {
				context.AddFailure(ValidatorErrors.ShortPassword);
			}

			if (StringChecker.IsPasswordStrong(currentPassword) == false) {
				context.AddFailure(ValidatorErrors.BadPassword);
			}
		});

		RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithErrorCode(ValidatorErrors.ConfirmPassword);

		RuleFor(x => x.CNPJ).Matches(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$").WithMessage(ValidatorErrors.CNPJisInvalid);
	}
}
