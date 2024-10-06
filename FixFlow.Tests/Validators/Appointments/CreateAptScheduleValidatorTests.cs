using FluentValidation.TestHelper;
using Server.Models.Appointments;
using Server.Validators.Appointments;
using Xunit;

namespace FixFlow.Tests.Validators.Appointments {
	public class CreateAptScheduleValidatorTests {
		private readonly CreateAptScheduleValidator _validator;

		public CreateAptScheduleValidatorTests() {
			_validator = new CreateAptScheduleValidator();
		}

		[Fact]
		public void Should_Have_Error_When_DateTime_Is_In_The_Past() {
			var model = new CreateAptSchedule {
				dateTime = DateTime.UtcNow.AddMinutes(-1)
			};
			var result = _validator.TestValidate(model);
			result.ShouldHaveValidationErrorFor(x => x.dateTime);
		}

		[Fact]
		public void Should_Have_Error_When_DateTime_Is_Too_Far_In_The_Future() {
			var model = new CreateAptSchedule {
				dateTime = DateTime.UtcNow.AddMonths(8)
			};
			var result = _validator.TestValidate(model);
			result.ShouldHaveValidationErrorFor(x => x.dateTime);
		}

		[Fact]
		public void Should_Not_Have_Error_When_DateTime_Is_Valid() {
			var model = new CreateAptSchedule {
				dateTime = DateTime.UtcNow.AddDays(1)
			};
			var result = _validator.TestValidate(model);
			result.ShouldNotHaveValidationErrorFor(x => x.dateTime);
		}
	}
}
