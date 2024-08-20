using FluentValidation;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Validators.DTOs;

public class ClientRegisterValidator : AbstractValidator<ClientRegister> {

	public ClientRegisterValidator() {

		RuleFor(x => x.FullName).Custom((fullname, context) => {

			if (StringChecker.IsFullNameValid(fullname)) {
				context.AddFailure(ValidatorErrors.FullName);
			}
		});

		RuleFor(x => x.CPF).Custom((cpf, context) => {

			if (cpf != null && StringChecker.isCPFvalid(cpf)) {
				context.AddFailure(ValidatorErrors.CPFisInvalid);
			}
		});

		RuleFor(x => x.UserName).Custom((userName, context) => {

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
	}
}
