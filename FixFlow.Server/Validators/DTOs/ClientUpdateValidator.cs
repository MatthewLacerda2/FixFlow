using FluentValidation;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Validators.DTOs;

public class ClientUpdateValidator : AbstractValidator<ClientUpdate> {

	public ClientUpdateValidator() {

		RuleFor(x => x.FullName).Custom((fullname, context) => {
			if (StringChecker.IsFullNameValid(fullname)) {
				context.AddFailure(ValidatorErrors.FullName);
			}
		});

		RuleFor(x => x.CPF).Custom((cpf, context) => {
			if (cpf != null) {
				if (StringChecker.isCPFvalid(cpf)) {
					context.AddFailure(ValidatorErrors.CPFisInvalid);
				}
			}
		});
	}
}
