using FluentValidation;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Validators.DTOs;

public class BusinessRegisterValidator : AbstractValidator<BusinessRegister> {
	public BusinessRegisterValidator() {
		RuleFor(x => x.Name).Custom((userName, context) => {
			if (string.IsNullOrWhiteSpace(userName)) {
				context.AddFailure(ValidatorErrors.UsernameIsEmpty);
			}
			if (userName.Contains(" ")) {
				context.AddFailure(ValidatorErrors.UsernameHasWhitespaces);
			}
		});

		RuleFor(x => x.password).Custom((currentPassword, context) => {
			if (currentPassword.Length < 8) {
				context.AddFailure(ValidatorErrors.ShortPassword);
			}

			if (StringChecker.IsPasswordStrong(currentPassword) == false) {
				context.AddFailure(ValidatorErrors.BadPassword);
			}
		});

		RuleFor(x => x.confirmPassword).Equal(x => x.password).WithErrorCode(ValidatorErrors.ConfirmPassword);

		RuleFor(x => x.CNPJ).Matches(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$").WithMessage(ValidatorErrors.CNPJisInvalid);
	}
}
