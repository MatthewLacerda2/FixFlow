using FixFlow.Server.Validators;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Erros;

namespace FixFlow.Tests.Validators {
	public class BusinessValidatorTests {
		private readonly BusinessValidator _validator;

		public BusinessValidatorTests() {
			_validator = new BusinessValidator();
		}

		[Fact]
		public void Should_Have_Error_When_BusinessDays_Count_Is_Not_7() {
			var business = new Business {
				BusinessDays = new List<BusinessDay> { new BusinessDay() }
			};

			var result = _validator.Validate(business);

			Assert.False(result.IsValid);
			Assert.Contains(result.Errors, e => e.ErrorMessage == ValidatorErrors.BusinessDayCountMustBe7);
		}

		[Fact]
		public void Should_Have_Error_When_BusinessDay_Start_Is_After_Finish() {
			var business = new Business {
				BusinessDays = new List<BusinessDay>
				{
					new BusinessDay { Start = new DateTime(2023, 1, 1, 18, 0, 0), Finish = new DateTime(2023, 1, 1, 8, 0, 0) },
					new BusinessDay(),
					new BusinessDay(),
					new BusinessDay(),
					new BusinessDay(),
					new BusinessDay(),
					new BusinessDay()
				}
			};

			var result = _validator.Validate(business);

			Assert.False(result.IsValid);
			Assert.Contains(result.Errors, e => e.ErrorMessage == ValidatorErrors.BusinessDayStartMustBeLessThanFinish);
		}

		[Fact]
		public void Should_Not_Have_Error_When_BusinessDays_Are_Valid() {
			var business = new Business {
				BusinessDays = new List<BusinessDay>
				{
					new BusinessDay(),
					new BusinessDay(),
					new BusinessDay(),
					new BusinessDay(),
					new BusinessDay(),
					new BusinessDay(),
					new BusinessDay()
				}
			};

			var result = _validator.Validate(business);

			Assert.True(result.IsValid);
		}
	}
}
