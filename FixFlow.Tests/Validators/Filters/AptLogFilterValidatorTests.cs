using FluentValidation.TestHelper;
using Server.Models.Filters;
using Server.Validators.Appointments;

namespace FixFlow.Tests.Validators.Filters {
	public class AptLogFilterValidatorTests {
		private readonly AptLogFilterValidator _validator;

		public AptLogFilterValidatorTests() {
			_validator = new AptLogFilterValidator();
		}

		[Fact]
		public void Should_Have_Error_When_MinPrice_Is_Negative() {
			var model = new AptLogFilter { minPrice = -1 };
			var result = _validator.TestValidate(model);
			result.ShouldHaveValidationErrorFor(x => x.minPrice);
		}

		[Fact]
		public void Should_Have_Error_When_MinPrice_Is_Greater_Than_MaxPrice() {
			var model = new AptLogFilter { minPrice = 100, maxPrice = 50 };
			var result = _validator.TestValidate(model);
			result.ShouldHaveValidationErrorFor(x => x.minPrice);
		}

		[Fact]
		public void Should_Have_Error_When_MinDateTime_Is_Before_2024() {
			var model = new AptLogFilter { minDateTime = new DateTime(2023, 12, 31) };
			var result = _validator.TestValidate(model);
			result.ShouldHaveValidationErrorFor(x => x.minDateTime);
		}

		[Fact]
		public void Should_Have_Error_When_MinDateTime_Is_Greater_Than_MaxDateTime() {
			var model = new AptLogFilter { minDateTime = new DateTime(2024, 1, 2), maxDateTime = new DateTime(2024, 1, 1) };
			var result = _validator.TestValidate(model);
			result.ShouldHaveValidationErrorFor(x => x.minDateTime);
		}

		[Fact]
		public void Should_Have_Error_When_Offset_Is_Negative() {
			var model = new AptLogFilter { offset = -1 };
			var result = _validator.TestValidate(model);
			result.ShouldHaveValidationErrorFor(x => x.offset);
		}

		[Fact]
		public void Should_Have_Error_When_Limit_Is_Less_Than_One() {
			var model = new AptLogFilter { limit = 0 };
			var result = _validator.TestValidate(model);
			result.ShouldHaveValidationErrorFor(x => x.limit);
		}

		[Fact]
		public void Should_Not_Have_Error_When_Model_Is_Valid() {
			var model = new AptLogFilter {
				minPrice = 10,
				maxPrice = 100,
				minDateTime = new DateTime(2024, 1, 1),
				maxDateTime = new DateTime(2024, 12, 31),
				offset = 0,
				limit = 1
			};
			var result = _validator.TestValidate(model);
			result.ShouldNotHaveAnyValidationErrors();
		}
	}
}
