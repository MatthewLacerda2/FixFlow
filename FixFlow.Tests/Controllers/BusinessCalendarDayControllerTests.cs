using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Server.Controllers;
using Server.Data;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models;

namespace FixFlow.Tests.Controllers;

public class BusinessCalendarControllerTests {

	private readonly ServerContext _context;
	private readonly BusinessCalendarDayController _controller;

	public BusinessCalendarControllerTests() {

		var connectionStringBuilder = new SqliteConnectionStringBuilder();
		connectionStringBuilder.DataSource = ":memory:";

		var connection = new SqliteConnection(connectionStringBuilder.ToString());

		DbContextOptions<ServerContext> _dbContextOptions = new DbContextOptionsBuilder<ServerContext>()
			.UseSqlite(connection)
			.Options;

		_context = new ServerContext(_dbContextOptions);
		_context.Database.OpenConnection();
		_context.Database.EnsureCreated();

		_controller = new BusinessCalendarDayController(_context);
	}

	[Fact]
	public async Task GetBusinessCalendar_ReturnsNotFound_WhenBusinessDoesNotExist() {
		// Arrange
		string nonExistentBusinessId = "non-existent-business-id";
		// Act
		var result = await _controller.GetBusinessCalendar(nonExistentBusinessId);
		// Assert
		Assert.IsType<NotFoundObjectResult>(result);
		var notFoundResult = result as NotFoundObjectResult;
		Assert.Equal(NotExistErrors.Business, notFoundResult!.Value);
	}

	[Fact]
	public async Task GetBusinessCalendar_ReturnsOk_WithBusinessCalendarDays() {
		// Arrange
		Business business = new Business("lenda", "lenda@gmail.com", "1234", "9899344788");
		business.Id = Guid.NewGuid().ToString();
		_context.Business.Add(business);

		_context.SaveChanges();

		// Act
		var result = await _controller.GetBusinessCalendar(business.Id);

		// Assert
		Assert.IsType<OkObjectResult>(result);
		var okResult = result as OkObjectResult;
		Assert.IsType<BusinessCalendarDay[]>(okResult!.Value);
		var businessCalendarDays = okResult.Value as BusinessCalendarDay[];
		Assert.Equal(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), businessCalendarDays!.Length);
	}
}
