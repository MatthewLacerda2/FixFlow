using FluentValidation;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Validators.DTOs;

public class BusinessDTOValidator : AbstractValidator<BusinessDTO> {
	public BusinessDTOValidator() {
		RuleFor(x => x.CPF).Custom((cpf, context) => {
			if (cpf != null && StringChecker.isCPFvalid(cpf)) {
				context.AddFailure(ValidatorErrors.CPFisInvalid);
			}
		});

		RuleFor(x => x.Name).Custom((userName, context) => {
			if (string.IsNullOrWhiteSpace(userName)) {
				context.AddFailure(ValidatorErrors.UsernameIsEmpty);
			}
			if (userName.Contains(" ")) {
				context.AddFailure(ValidatorErrors.UsernameHasWhitespaces);
			}
		});
	}
}
