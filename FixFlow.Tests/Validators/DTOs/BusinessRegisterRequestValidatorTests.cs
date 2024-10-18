using FluentValidation.TestHelper;
using Server.Models.DTO;
using Server.Validators.DTOs;

namespace FixFlow.Tests.Validators;

public class BusinessRegisterRequestValidatorTests {

	private readonly BusinessRegisterRequestValidator _validator;

	public BusinessRegisterRequestValidatorTests() {
		_validator = new BusinessRegisterRequestValidator();
	}

	[Fact]
	public void Should_Have_Error_When_Password_Is_Short() {
		var model = new BusinessRegisterRequest("", "lenda@gmail.com", "789.4561.123/0001-80", "98999344788", "@Sh0rt", "@Short");
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(x => x.Password);
	}

	[Fact]
	public void Should_Not_Have_Error_When_Password_Is_Strong() {
		var model = new BusinessRegisterRequest("Valid Name", "lenda@gmail.com", "789.4561.123/0001-80", "98999344788", "@Str0ng", "@Str0ng");
		var result = _validator.TestValidate(model);
		result.ShouldNotHaveValidationErrorFor(x => x.Password);
	}

	[Fact]
	public void Should_Have_Error_When_ConfirmPassword_Does_Not_Match() {
		var model = new BusinessRegisterRequest("", "lenda@gmail.com", "789.4561.123/0001-80", "98999344788", "@Str0ng1", "@Str0ng2");
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
	}

	[Fact]
	public void Should_Not_Have_Error_When_ConfirmPassword_Matches() {
		var model = new BusinessRegisterRequest("", "lenda@gmail.com", "789.4561.123/0001-80", "98999344788", "@Str0ng!", "@Str0ng!");
		var result = _validator.TestValidate(model);
		result.ShouldNotHaveValidationErrorFor(x => x.ConfirmPassword);
	}
}
