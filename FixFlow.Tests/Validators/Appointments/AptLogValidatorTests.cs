using FluentValidation.TestHelper;
using Server.Models.Appointments;
using Server.Models.Erros;
using Server.Validators.Appointments;

namespace FixFlow.Tests.Validators;

public class AptLogValidatorTests {

	private readonly AptLogValidator _validator;

	public AptLogValidatorTests() {
		_validator = new AptLogValidator();
	}

	[Fact]
	public void Should_HaveError_When_PriceIsNegative() {
		// Arrange
		var dateTime = new DateTime(2024, 6, 1);
		var createLog = new CreateAptLog("clientId", null, dateTime, -1, null, null, dateTime.AddDays(90));
		var aptLog = new AptLog(createLog, "businessId");

		// Act & Assert
		var result = _validator.TestValidate(aptLog);
		result.ShouldHaveValidationErrorFor(log => log.price)
			  .WithErrorMessage(ValidatorErrors.PriceMustBeNaturalNumber);
	}

	[Fact]
	public void Should_HaveError_When_DateTimeIsBefore2024() {
		// Arrange
		var dateTime = new DateTime(2023, 12, 31);
		var createLog = new CreateAptLog("clientId", null, dateTime, 100, null, null, dateTime.AddDays(90));
		var aptLog = new AptLog(createLog, "businessId");

		// Act & Assert
		var result = _validator.TestValidate(aptLog);
		result.ShouldHaveValidationErrorFor(log => log.dateTime)
			  .WithErrorMessage(ValidatorErrors.DateMustBe2024orForward);
	}

	[Fact]
	public void Should_HaveError_When_DateTimeIsInTheFuture() {
		// Arrange
		var futureDate = DateTime.UtcNow.AddDays(1);
		var createLog = new CreateAptLog("clientId", null, futureDate, 100, null, null, futureDate.AddDays(90));
		var aptLog = new AptLog(createLog, "businessId");

		// Act & Assert
		var result = _validator.TestValidate(aptLog);
		result.ShouldHaveValidationErrorFor(log => log.dateTime)
			  .WithErrorMessage(ValidatorErrors.DateMustNotBeInTheFuture);
	}

	[Fact]
	public void Should_NotHaveError_When_ValidAptLog() {
		// Arrange
		var dateTime = DateTime.UtcNow.AddDays(-1);
		var createLog = new CreateAptLog("clientId", null, dateTime, 100, null, null, dateTime.AddDays(90));
		var validAptLog = new AptLog(createLog, "businessId");

		// Act & Assert
		var result = _validator.TestValidate(validAptLog);
		result.ShouldNotHaveValidationErrorFor(log => log.price);
		result.ShouldNotHaveValidationErrorFor(log => log.dateTime);
	}
}
