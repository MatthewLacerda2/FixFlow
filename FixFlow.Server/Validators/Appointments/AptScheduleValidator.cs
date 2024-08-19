using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;

namespace Server.Validators.Appointments;

public class AptScheduleValidator : AbstractValidator<AptSchedule> {

    private readonly ServerContext _context;
    private readonly UserManager<Client> _clientUserManager;
    private readonly UserManager<Business> _businessUserManager;

    public AptScheduleValidator(ServerContext context, UserManager<Client> clientUserManager, UserManager<Business> businessUserManager) {

        _context = context;
        _clientUserManager = clientUserManager;
        _businessUserManager = businessUserManager;

        RuleFor(x => x.dateTime).GreaterThanOrEqualTo(new DateTime(2023, 1, 1)).WithErrorCode("Date must be from 2023 and forward");
        RuleFor(x => x.dateTime).LessThanOrEqualTo(DateTime.Now).WithErrorCode("Date hasn't even passed yet");

        RuleFor(x => x.clientId)
            .NotEmpty().WithMessage("ClientId is required")
            .MustAsync(ClientExists).WithMessage("Client does not exist");

        RuleFor(x => x.businessId)
            .NotEmpty().WithMessage("BusinessId is required")
            .MustAsync(BusinessExists).WithMessage("Business does not exist");

        RuleFor(x=>x.contactId)
            .Must(ContactExists).When(s=>s.contactId!=null).WithErrorCode("Contact does not exist");
    }

    private async Task<bool> ClientExists(string clientId, CancellationToken cancellationToken) {
        return await _clientUserManager.FindByIdAsync(clientId) != null;
    }

    private async Task<bool> BusinessExists(string businessId, CancellationToken cancellationToken) {
        return await _businessUserManager.FindByIdAsync(businessId) != null;
    }

    private bool ContactExists(string? contactId) {
        return contactId == null || _context.Contacts.Find(contactId) != null;
    }    
}