using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Controllers;
using Server.Data;
using Server.Models;
using Server.Models.Erros;

namespace FixFlow.Tests.Controllers;

public class BusinessControllerTests {

	private readonly ServerContext _context;
	private readonly BusinessController _controller;
	private readonly Mock<UserManager<Business>> _mockUserManager;

	public BusinessControllerTests() {

		_context = new Util().SetupDbContextForTests();

		var store = new Mock<IUserStore<Business>>();
		_mockUserManager = new Mock<UserManager<Business>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

		_controller = new BusinessController(_context, _mockUserManager.Object);
	}

	[Fact]
	public async Task GetBusiness_ReturnsOkResult_WhenBusinessExists() {
		// Arrange
		var businessId = "test-business-id";
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98999344788");
		_mockUserManager.Setup(x => x.FindByIdAsync(businessId)).ReturnsAsync(business);

		// Act
		var result = await _controller.GetBusiness(businessId);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		Assert.Equal(business, okResult.Value);
	}

	[Fact]
	public async Task GetBusiness_ReturnsBadRequest_WhenBusinessDoesNotExist() {
		// Arrange
		var businessId = "non-existent-business-id";
		_mockUserManager.Setup(x => x.FindByIdAsync(businessId)).ReturnsAsync((Business)null!);

		// Act
		var result = await _controller.GetBusiness(businessId);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(NotExistErrors.Business, badRequestResult.Value);
	}
}
