using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Server.Models;
using Server.Models.Filters;

namespace Server.Validators.Appointments;

public class AptContactFilterValidator : AbstractValidator<AptContactFilter> {

    private readonly UserManager<Client> _clientUserManager;
    private readonly UserManager<Business> _businessUserManager;

    public AptContactFilterValidator(UserManager<Client> clientUserManager, UserManager<Business> businessUserManager) {

        _clientUserManager = clientUserManager;
        _businessUserManager = businessUserManager;

        RuleFor(x => x.clientId)
            .MustAsync(ClientExists!).When(x => x.clientId != null).WithMessage("Client does not exist.");

        RuleFor(x => x.businessId)
            .MustAsync(BusinessExists!).When(x => x.businessId != null).WithMessage("Business does not exist.");

        RuleFor(x => x.minDateTime).GreaterThanOrEqualTo(new DateOnly(2023, 1, 1)).WithErrorCode("Date must be from 2023 and forward.");
        RuleFor(x => x.minDateTime).GreaterThanOrEqualTo(x=>x.maxDateTime).WithErrorCode("MinDate must be older than MaxDate.");
        RuleFor(x => x.maxDateTime).GreaterThan(DateOnly.FromDateTime(DateTime.Now)).WithErrorCode("Date has to be in the future.");
    }

    private async Task<bool> ClientExists(string clientId, CancellationToken cancellationToken) {
        return await _clientUserManager.FindByIdAsync(clientId) != null;
    }

    private async Task<bool> BusinessExists(string businessId, CancellationToken cancellationToken) {
        return await _businessUserManager.FindByIdAsync(businessId) != null;
    }
}