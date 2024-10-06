using FluentValidation.TestHelper;
using Server.Models.Appointments;
using Server.Validators.Appointments;

namespace FixFlow.Tests.Validators.Appointments {
	public class UpdateAptLogValidatorTests {
		private readonly UpdateAptLogValidator _validator;

		public UpdateAptLogValidatorTests() {
			_validator = new UpdateAptLogValidator();
		}

		[Fact]
		public void Should_Have_Error_When_Price_Is_Negative() {
			var model = new UpdateAptLog("1", null, DateTime.UtcNow, null, -1, null);
			var result = _validator.TestValidate(model);
			result.ShouldHaveValidationErrorFor(x => x.Price);
		}

		[Fact]
		public void Should_Not_Have_Error_When_Price_Is_Zero_Or_Positive() {
			var model = new UpdateAptLog("1", null, DateTime.UtcNow, null, 0, null);
			var result = _validator.TestValidate(model);
			result.ShouldNotHaveValidationErrorFor(x => x.Price);
		}

		[Fact]
		public void Should_Have_Error_When_DateTime_Is_Before_2024() {
			var model = new UpdateAptLog("1", null, new DateTime(2023, 12, 31), null, 0, null);
			var result = _validator.TestValidate(model);
			result.ShouldHaveValidationErrorFor(x => x.dateTime);
		}

		[Fact]
		public void Should_Not_Have_Error_When_DateTime_Is_2024_Or_Later() {
			var model = new UpdateAptLog("1", null, new DateTime(2024, 1, 1), null, 0, null);
			var result = _validator.TestValidate(model);
			result.ShouldNotHaveValidationErrorFor(x => x.dateTime);
		}

		[Fact]
		public void Should_Have_Error_When_DateTime_Is_In_The_Future() {
			var model = new UpdateAptLog("1", null, DateTime.UtcNow.AddDays(1), null, 0, null);
			var result = _validator.TestValidate(model);
			result.ShouldHaveValidationErrorFor(x => x.dateTime);
		}

		[Fact]
		public void Should_Not_Have_Error_When_DateTime_Is_Not_In_The_Future() {
			var model = new UpdateAptLog("1", null, DateTime.UtcNow.AddDays(-1), null, 0, null);
			var result = _validator.TestValidate(model);
			result.ShouldNotHaveValidationErrorFor(x => x.dateTime);
		}
	}
}
