using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Controllers;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Erros;

namespace FixFlow.Tests.Controllers;

public class BusinessCalendarControllerTests {

	private readonly ServerContext _context;
	private readonly BusinessCalendarDayController _controller;
	private readonly Mock<UserManager<Customer>> _mockCustomerManager;

	public BusinessCalendarControllerTests() {

		_context = new Util().SetupDbContextForTests();

		var clientStore = new Mock<IUserStore<Customer>>();
		_mockCustomerManager = new Mock<UserManager<Customer>>(clientStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

		_controller = new BusinessCalendarDayController(_context, _mockCustomerManager.Object);
	}

	[Fact]
	public async Task GetBusinessCalendar_ReturnsNotFound_WhenBusinessDoesNotExist() {
		// Arrange
		string nonExistentBusinessId = "non-existent-business-id";
		// Act
		var result = await _controller.GetBusinessMonthCalendar(2024, 12);
		// Assert
		Assert.IsType<NotFoundObjectResult>(result);
		var notFoundResult = result as NotFoundObjectResult;
		Assert.Equal(NotExistErrors.Business, notFoundResult!.Value);
	}

	[Fact]
	public async Task GetBusinessCalendar_ReturnsOk_WithBusinessCalendarDays() {
		// Arrange
		Business business = new Business("lenda", "lenda@gmail.com", "1234", "9899344788");
		_context.Business.Add(business);

		_context.SaveChanges();

		// Act
		var result = await _controller.GetBusinessMonthCalendar(2024, 12);

		// Assert
		Assert.IsType<OkObjectResult>(result);
		var okResult = result as OkObjectResult;
		Assert.IsType<BusinessCalendarDay[]>(okResult!.Value);
		var businessCalendarDays = okResult.Value as BusinessCalendarDay[];
		Assert.Equal(DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month), businessCalendarDays!.Length);
	}
}
