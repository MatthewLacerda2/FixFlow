using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Server.Models;
using Server.Models.Erros;
using Server.Models.Filters;

namespace Server.Validators.Filters;

public class AptLogFilterValidator : AbstractValidator<AptLogFilter> {

	private readonly UserManager<Client> _clientUserManager;
	private readonly UserManager<Business> _businessUserManager;

	public AptLogFilterValidator(UserManager<Client> clientUserManager, UserManager<Business> businessUserManager) {

		_clientUserManager = clientUserManager;
		_businessUserManager = businessUserManager;

		RuleFor(x => x.clientId)
			.MustAsync(ClientExists!).When(x => x.clientId != null).WithMessage(NotExistErrors.Client);

		RuleFor(x => x.businessId)
			.MustAsync(BusinessExists!).When(x => x.businessId != null).WithMessage(NotExistErrors.Business);

		RuleFor(x => x.minDateTime).GreaterThanOrEqualTo(new DateOnly(2023, 1, 1)).WithErrorCode(ValidatorErrors.DateMustBe2023orForward);
		RuleFor(x => x.minDateTime).GreaterThanOrEqualTo(x => x.maxDateTime).WithErrorCode(ValidatorErrors.MinDateMustBeOlder);
		RuleFor(x => x.maxDateTime).GreaterThan(DateOnly.FromDateTime(DateTime.Now)).WithErrorCode(ValidatorErrors.DateHasntPassedYet);
	}

	private async Task<bool> ClientExists(string clientId, CancellationToken cancellationToken) {
		return await _clientUserManager.FindByIdAsync(clientId) != null;
	}

	private async Task<bool> BusinessExists(string businessId, CancellationToken cancellationToken) {
		return await _businessUserManager.FindByIdAsync(businessId) != null;
	}
}
