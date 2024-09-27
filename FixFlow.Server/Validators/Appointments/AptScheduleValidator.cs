using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.Erros;

namespace Server.Validators.Appointments;

public class AptScheduleValidator : AbstractValidator<AptSchedule> {

	public AptScheduleValidator() {

		RuleFor(x => x.dateTime).GreaterThanOrEqualTo(new DateTime(2023, 1, 1)).WithErrorCode(ValidatorErrors.DateMustBe2023orForward);
		RuleFor(x => x.dateTime).LessThanOrEqualTo(DateTime.Now).WithErrorCode(ValidatorErrors.DateHasntPassedYet);

	}
}
