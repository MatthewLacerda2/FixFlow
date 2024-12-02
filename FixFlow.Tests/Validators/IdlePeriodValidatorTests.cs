using FixFlow.Server.Validators;
using FluentValidation.TestHelper;
using Server.Models;

namespace FixFlow.Tests.Validators;

public class IdlePeriodValidatorTests {

	private readonly IdlePeriodValidator _validator;

	public IdlePeriodValidatorTests() {
		_validator = new IdlePeriodValidator();
	}

	[Fact]
	public void Should_Have_Error_When_Start_Is_Greater_Than_Finish() {
		var idlePeriod = new IdlePeriod {
			start = DateTime.UtcNow.AddDays(1),
			finish = DateTime.UtcNow
		};

		var result = _validator.TestValidate(idlePeriod);
		result.ShouldHaveValidationErrorFor(ip => ip.start);
	}

	[Fact]
	public void Should_Not_Have_Error_When_Start_Is_Less_Than_Finish() {
		var idlePeriod = new IdlePeriod {
			start = DateTime.UtcNow,
			finish = DateTime.UtcNow.AddDays(1)
		};

		var result = _validator.TestValidate(idlePeriod);
		result.ShouldNotHaveValidationErrorFor(ip => ip.start);
	}

	[Fact]
	public void Should_Have_Error_When_Name_Is_Empty() {
		var idlePeriod = new IdlePeriod {
			start = DateTime.UtcNow,
			finish = DateTime.UtcNow.AddDays(1),
			Name = string.Empty
		};

		var result = _validator.TestValidate(idlePeriod);
		result.ShouldHaveValidationErrorFor(ip => ip.Name);
	}
}
