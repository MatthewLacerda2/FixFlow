using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Validators.DTOs;

public class ClientUpdateValidator : AbstractValidator<ClientUpdate> {

	public ClientUpdateValidator(UserManager<Business> businessManager, UserManager<Client> clientManager, ServerContext serverContext) {

		RuleFor(x => x.Id).Custom((id, context) => {
			if (clientManager.FindByIdAsync(id).Result == null) {
				context.AddFailure(ValidatorErrors.ClientIdRequired);
			}
		});

		RuleFor(x => x.businessId).Custom((businessId, context) => {
			if (businessManager.FindByIdAsync(businessId).Result == null) {
				context.AddFailure(ValidatorErrors.BusinessIdRequired);
			}
		});

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
