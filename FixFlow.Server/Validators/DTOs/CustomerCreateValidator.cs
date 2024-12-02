using FluentValidation;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Validators.DTOs;

public class CustomerRegisterValidator : AbstractValidator<CustomerCreate> {

	public CustomerRegisterValidator() {

		RuleFor(x => x.FullName).Custom((fullname, context) => {
			if (StringChecker.IsFullNameValid(fullname) == false) {
				context.AddFailure(ValidatorErrors.FullName);
			}
		});

		RuleFor(x => x.CPF).Custom((cpf, context) => {
			if (cpf != null) {
				if (StringChecker.isCPFvalid(cpf) == false) {
					context.AddFailure(ValidatorErrors.CPFisInvalid);
				}
			}
		});
	}
}
