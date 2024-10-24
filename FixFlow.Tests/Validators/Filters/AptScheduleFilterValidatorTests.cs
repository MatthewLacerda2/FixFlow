using System;
using FluentValidation.TestHelper;
using Server.Models.Filters;
using Server.Validators.Appointments;
using Xunit;

namespace FixFlow.Tests.Validators;

public class AptScheduleFilterValidatorTests {

	private readonly AptScheduleFilterValidator _validator;

	public AptScheduleFilterValidatorTests() {
		_validator = new AptScheduleFilterValidator();
	}

	[Fact]
	public void Should_Have_Error_When_MinPrice_Is_Negative() {
		var model = new AptScheduleFilter { minPrice = -1 };
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(x => x.minPrice);
	}

	[Fact]
	public void Should_Have_Error_When_MinPrice_Greater_Than_MaxPrice() {
		var model = new AptScheduleFilter { minPrice = 100, maxPrice = 50 };
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(x => x.minPrice);
	}

	[Fact]
	public void Should_Have_Error_When_MinDateTime_Is_Before_2024() {
		var model = new AptScheduleFilter { minDateTime = new DateTime(2023, 12, 31) };
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(x => x.minDateTime);
	}

	[Fact]
	public void Should_Have_Error_When_MinDateTime_Greater_Than_MaxDateTime() {
		var model = new AptScheduleFilter { minDateTime = new DateTime(2024, 1, 2), maxDateTime = new DateTime(2024, 1, 1) };
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(x => x.minDateTime);
	}

	[Fact]
	public void Should_Have_Error_When_Offset_Is_Negative() {
		var model = new AptScheduleFilter { offset = -1 };
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(x => x.offset);
	}

	[Fact]
	public void Should_Not_Have_Error_When_Valid_Model() {
		var model = new AptScheduleFilter {
			minPrice = 0,
			maxPrice = 100,
			minDateTime = new DateTime(2024, 1, 1),
			maxDateTime = new DateTime(2024, 12, 31),
			offset = 0
		};
		var result = _validator.TestValidate(model);
		result.ShouldNotHaveAnyValidationErrors();
	}
}
