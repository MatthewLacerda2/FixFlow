using FluentValidation;
using Server.Models.Appointments;

namespace Server.Validators;

public class ScheduleValidator : AbstractValidator<AppointmentSchedule>
{

    public ScheduleValidator()
    {
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.DateTime).GreaterThanOrEqualTo(new DateTime(2024, 1, 1));
        /*
        var validator = new ScheduleValidator();
        var person = new AppointmentSchedule("",new DateTime());
        var result = validator.Validate(person, options => options.IncludeRuleSets("Names"));

        var resulta = validator.Validate(person, options =>
        {
            options.IncludeAllRuleSets();
        });*/
    }

}