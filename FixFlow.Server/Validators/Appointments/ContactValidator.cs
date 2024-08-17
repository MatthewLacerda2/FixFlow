using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;

namespace Server.Validators.Appointments;

public class ContactValidator : AbstractValidator<AptContact> {

    private readonly ServerContext _context;
    private readonly UserManager<Client> _clientUserManager;
    private readonly UserManager<Business> _businessUserManager;

    public ContactValidator(ServerContext context, UserManager<Client> clientUserManager, UserManager<Business> businessUserManager) {

        _context = context;
        _clientUserManager = clientUserManager;
        _businessUserManager = businessUserManager;

        RuleFor(x => x.dateTime).GreaterThanOrEqualTo(new DateTime(2023, 1, 1)).WithErrorCode("Date must be from 2023 and forward");
        RuleFor(x => x.dateTime).GreaterThan(DateTime.Now).WithErrorCode("Date has to be in the future");

        RuleFor(x => x.clientId)
            .NotEmpty().WithMessage("ClientId is required")
            .MustAsync(ClientExists).WithMessage("Client does not exist");

        RuleFor(x => x.businessId)
            .NotEmpty().WithMessage("BusinessId is required")
            .MustAsync(BusinessExists).WithMessage("Business does not exist");

        RuleFor(x => x.aptLogId)
            .NotEmpty().WithMessage("AptLogId is required")
            .Must(AptLogExists).WithMessage("AptLog does not exist");
    }

    private async Task<bool> ClientExists(string clientId, CancellationToken cancellationToken) {
        return await _clientUserManager.FindByIdAsync(clientId) != null;
    }

    private async Task<bool> BusinessExists(string businessId, CancellationToken cancellationToken) {
        return await _businessUserManager.FindByIdAsync(businessId) != null;
    }

    private bool AptLogExists(string aptLogId) {
        return _context.Logs.Find(aptLogId) != null;
    }
}