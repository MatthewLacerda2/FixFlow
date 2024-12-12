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
			Start = DateTime.UtcNow.AddDays(1),
			Finish = DateTime.UtcNow
		};

		var result = _validator.TestValidate(idlePeriod);
		result.ShouldHaveValidationErrorFor(ip => ip.Start);
	}

	[Fact]
	public void Should_Not_Have_Error_When_Start_Is_Less_Than_Finish() {
		var idlePeriod = new IdlePeriod {
			Start = DateTime.UtcNow,
			Finish = DateTime.UtcNow.AddDays(1)
		};

		var result = _validator.TestValidate(idlePeriod);
		result.ShouldNotHaveValidationErrorFor(ip => ip.Start);
	}

	[Fact]
	public void Should_Have_Error_When_Name_Is_Empty() {
		var idlePeriod = new IdlePeriod {
			Start = DateTime.UtcNow,
			Finish = DateTime.UtcNow.AddDays(1),
			Name = string.Empty
		};

		var result = _validator.TestValidate(idlePeriod);
		result.ShouldHaveValidationErrorFor(ip => ip.Name);
	}
}
