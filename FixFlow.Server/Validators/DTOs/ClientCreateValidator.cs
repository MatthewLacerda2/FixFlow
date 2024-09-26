using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Validators.DTOs;

public class ClientRegisterValidator : AbstractValidator<ClientCreate> {

	public ClientRegisterValidator(ServerContext serverContext) {

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

				bool cpfExists = serverContext.Clients.Any(x => x.CPF == cpf);
				if (cpfExists) {
					context.AddFailure(AlreadyRegisteredErrors.CPF);
				}
			}
		});
	}
}
