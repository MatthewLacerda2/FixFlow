using FixFlow.Server.Validators;
using FluentValidation.TestHelper;
using Server.Models.DTO;

namespace FixFlow.Tests.Validators;

public class BusinessTimeSpanValidatorTests {
	private readonly BusinessTimeSpanValidator _validator;

	public BusinessTimeSpanValidatorTests() {
		_validator = new BusinessTimeSpanValidator();
	}

	[Fact]
	public void Should_Have_Error_When_Start_Is_Greater_Than_Finish() {

		var bTimeSpan = new BusinessTimeSpan(new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0));

		var result = _validator.TestValidate(bTimeSpan);

		result.ShouldHaveValidationErrorFor(ip => ip.Start);

	}

	[Fact]
	public void Should_Not_Have_Error_When_Start_Is_Less_Than_Finish() {
		var bTimeSpan = new BusinessTimeSpan(new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0));

		var result = _validator.TestValidate(bTimeSpan);

		result.ShouldNotHaveValidationErrorFor(ip => ip.Start);
	}
}
