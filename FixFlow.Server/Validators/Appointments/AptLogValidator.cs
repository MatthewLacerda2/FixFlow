using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.Erros;

namespace Server.Validators.Appointments;

public class AptLogValidator : AbstractValidator<AptLog> {

	private readonly ServerContext _context;
	private readonly UserManager<Client> _clientUserManager;
	private readonly UserManager<Business> _businessUserManager;

	public AptLogValidator(ServerContext context, UserManager<Client> clientUserManager, UserManager<Business> businessUserManager) {

		_context = context;
		_clientUserManager = clientUserManager;
		_businessUserManager = businessUserManager;

		RuleFor(x => x.price)
				.GreaterThanOrEqualTo(0)
				.WithErrorCode(ValidatorErrors.PriceMustBeNaturalNumber);

		RuleFor(x => x.dateTime)
				.GreaterThanOrEqualTo(new DateTime(2023, 1, 1))
				.WithErrorCode(ValidatorErrors.DateMustBe2023orForward);
		RuleFor(x => x.dateTime)
				.LessThanOrEqualTo(DateTime.Now)
				.WithErrorCode(ValidatorErrors.DateHasntPassedYet);

		RuleFor(x => x.scheduleId)
				.Must(AptScheduleExists).When(s => !string.IsNullOrEmpty(s.scheduleId)).WithMessage(NotExistErrors.AptSchedule);

		RuleFor(x => x.clientId)
				.NotEmpty().WithMessage(ValidatorErrors.ClientIdRequired)
				.MustAsync(ClientExists).WithMessage(NotExistErrors.Client);

		RuleFor(x => x.businessId)
				.NotEmpty().WithMessage(ValidatorErrors.BusinessIdRequired)
				.MustAsync(BusinessExists).WithMessage(ValidatorErrors.BusinessIdRequired);
	}

	private async Task<bool> ClientExists(string clientId, CancellationToken cancellationToken) {
		return await _clientUserManager.FindByIdAsync(clientId) != null;
	}

	private async Task<bool> BusinessExists(string businessId, CancellationToken cancellationToken) {
		return await _businessUserManager.FindByIdAsync(businessId) != null;
	}

	private bool AptScheduleExists(string? scheduleId) {
		return scheduleId == null || _context.Schedules.Find(scheduleId) != null;
	}
}
