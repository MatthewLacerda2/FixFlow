using FluentValidation.TestHelper;
using Server.Models.Appointments;
using Server.Validators.Appointments;

namespace FixFlow.Tests.Validators;

public class CreateAptLogValidatorTests {

	private readonly CreateAptLogValidator _validator;

	public CreateAptLogValidatorTests() {
		_validator = new CreateAptLogValidator();
	}

	[Fact]
	public void Should_Have_Error_When_Price_Is_Negative() {
		var model = new CreateAptLog("clientId", "businessId", null, DateTime.UtcNow, -1, null, null, DateTime.UtcNow.AddDays(1));
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(x => x.price);
	}

	[Fact]
	public void Should_Not_Have_Error_When_Price_Is_Zero_Or_Positive() {
		var model = new CreateAptLog("clientId", "businessId", null, DateTime.UtcNow, 0, null, null, DateTime.UtcNow.AddDays(1));
		var result = _validator.TestValidate(model);
		result.ShouldNotHaveValidationErrorFor(x => x.price);

		model = new CreateAptLog("clientId", "businessId", null, DateTime.UtcNow, 10, null, null, DateTime.UtcNow.AddDays(1));
		result = _validator.TestValidate(model);
		result.ShouldNotHaveValidationErrorFor(x => x.price);
	}

	[Fact]
	public void Should_Have_Error_When_DateTime_Is_Before_2024() {
		var model = new CreateAptLog("clientId", "businessId", null, new DateTime(2023, 12, 31), 10, null, null, DateTime.UtcNow.AddDays(1));
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(x => x.dateTime);
	}

	[Fact]
	public void Should_Not_Have_Error_When_DateTime_Is_2024_Or_Later() {
		var model = new CreateAptLog("clientId", "businessId", null, new DateTime(2024, 1, 1), 10, null, null, DateTime.UtcNow.AddDays(1));
		var result = _validator.TestValidate(model);
		result.ShouldNotHaveValidationErrorFor(x => x.dateTime);
	}

	[Fact]
	public void Should_Have_Error_When_WhenShouldCustomerComeBack_Is_Not_In_The_Future() {
		var model = new CreateAptLog("clientId", "businessId", null, DateTime.UtcNow, 10, null, null, DateTime.UtcNow.Date);
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(x => x.whenShouldCustomerComeBack);
	}

	[Fact]
	public void Should_Not_Have_Error_When_WhenShouldCustomerComeBack_Is_In_The_Future() {
		var model = new CreateAptLog("clientId", "businessId", null, DateTime.UtcNow, 10, null, null, DateTime.UtcNow.AddDays(1));
		var result = _validator.TestValidate(model);
		result.ShouldNotHaveValidationErrorFor(x => x.whenShouldCustomerComeBack);
	}
}
