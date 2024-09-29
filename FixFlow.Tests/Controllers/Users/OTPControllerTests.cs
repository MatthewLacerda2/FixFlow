using FixFlow.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Server.Data;

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
		string phoneNumber = "1234567890";
		// Act
		var result = await _controller.CreateBusinessOTP(phoneNumber);
		// Assert
		Assert.IsType<OkResult>(result);
	}
}
