using FluentValidation.TestHelper;
using Server.Models.DTO;
using Server.Validators.DTOs;

namespace FixFlow.Tests.Validators.DTOs;

public class CustomerCreateValidatorTests {
	private readonly CustomerRegisterValidator _validator;

	public CustomerCreateValidatorTests() {
		_validator = new CustomerRegisterValidator();
	}

	[Fact]
	public void Should_Have_Error_When_FullName_Is_Invalid() {
		var model = new CustomerCreate("1", "John", null, null, "1234567890", null);
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(x => x.FullName);
	}

	[Fact]
	public void Should_Not_Have_Error_When_FullName_Is_Valid() {
		var model = new CustomerCreate("1", "John Doe", null, null, "1234567890", null);
		var result = _validator.TestValidate(model);
		result.ShouldNotHaveValidationErrorFor(x => x.FullName);
	}

	[Fact]
	public void Should_Have_Error_When_CPF_Is_Invalid() {
		var model = new CustomerCreate("1", "John Doe", "12345678900", null, "1234567890", null);
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(x => x.CPF);
	}

	[Fact]
	public void Should_Not_Have_Error_When_CPF_Is_Valid() {
		var model = new CustomerCreate("1", "John Doe", "123.456.789-09", null, "1234567890", null);
		var result = _validator.TestValidate(model);
		result.ShouldNotHaveValidationErrorFor(x => x.CPF);
	}
}
