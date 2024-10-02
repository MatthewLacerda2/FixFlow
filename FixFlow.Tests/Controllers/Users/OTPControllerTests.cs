using FixFlow.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models.Erros;

namespace FixFlow.Tests.Controllers;

public class OTPControllerTests {

	private readonly ServerContext _context;
	private readonly OTPController _controller;

	public OTPControllerTests() {

		_context = new Util().SetupDbContextForTests();

		_controller = new OTPController(_context);
	}

	[Fact]
	public async Task CreateBusinessOTP_ReturnsOkResult() {
		// Arrange
		string phoneNumber = "11234567890";
		// Act
		var result = await _controller.CreateBusinessOTP(phoneNumber);
		// Assert
		Assert.IsType<OkResult>(result);
	}

	[Theory]
	[InlineData("123")]
	[InlineData("(11)23456789")]
	public async Task CreateBusinessOTP_ReturnsBadRequest_ForInvalidPhoneNumber(string invalidPhoneNumber) {
		// Act
		var result = await _controller.CreateBusinessOTP(invalidPhoneNumber);
		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(ValidatorErrors.InvalidOTP, badRequestResult.Value);
	}
}
