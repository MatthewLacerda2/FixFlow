using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Server.Controllers;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.Filters;
using Server.Seeder;

namespace FixFlow.Tests.Controllers;

public class AptLogControllerTests {

	private readonly DbContextOptions<ServerContext> _dbContextOptions;
	private readonly Mock<UserManager<Client>> _userManagerMock;
	private readonly ServerContext _context;
	private readonly AptLogController _controller;

	public AptLogControllerTests() {

		var connectionStringBuilder = new SqliteConnectionStringBuilder {
			DataSource = ":memory:"
		};
		var connection = new SqliteConnection(connectionStringBuilder.ToString());

		// Configure DbContext to use SQLite in-memory database
		_dbContextOptions = new DbContextOptionsBuilder<ServerContext>()
			.UseSqlite(connection)
			.Options;

		var userStoreMock = new Mock<IUserStore<Client>>();
		_userManagerMock = new Mock<UserManager<Client>>(userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);

		_context = new ServerContext(_dbContextOptions);
		_context.Database.OpenConnection();
		_context.Database.EnsureCreated();

		_controller = new AptLogController(_context, _userManagerMock.Object);
	}

	[Fact]
	public async Task ReadLog_ReturnsLog_WhenLogExists() {
		// Arrange
		var client = new Client("fulano", "123456789", "", "88263255", "fulano@hotmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var log = new AptLog(client.Id, business.Id, 30);

		_context.AddRange(client, business, log);
		_context.SaveChanges();

		// Act
		var result = await _controller.ReadLog(log.Id) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status200OK, result!.StatusCode);
		var returnedLog = Assert.IsType<AptLog>(result.Value);
		Assert.Equal(log.Id, returnedLog.Id);
	}

	[Fact]
	public async Task ReadLog_ReturnsNotFound_WhenLogDoesNotExist() {
		// Act
		var result = await _controller.ReadLog("nonExistingId") as NotFoundObjectResult;
		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status404NotFound, result!.StatusCode);
		Assert.Equal("Log does not exist", result.Value);
	}

	[Fact]
	public void ReadLogs_ReturnsEmptyArray_WhenNoLogsMatchFilter() {
		// Arrange
		var filter = new AptLogFilter(null!, null!, DateOnly.MinValue, DateOnly.MaxValue);
		// Act
		var result = _controller.ReadLogs(filter) as OkObjectResult;
		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status200OK, result!.StatusCode);
		var logs = Assert.IsType<AptLog[]>(result!.Value);
		Assert.Empty(logs);
	}

	[Fact]
	public void ReadLogs_FiltersLogs() {
		// Arrange
		var client = new Client("fulano", "123456789", null!, "88263255", "fulano@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");

		var otherClient = new Client("cicrano", "987654321", null!, "9898263255", "cicrano@gmail.com", true);
		var otherBusiness = new Business("otherbusiness", "60742928000", "4560123", "98993265849", "otherbusiness@gmail.com", "");

		_context.AddRange(client, business, otherClient, otherBusiness);
		_context.SaveChanges();

		var filter = new AptLogFilter(client.Id, business.Id, new DateOnly(2023, 1, 1), new DateOnly(2025, 3, 1)) {
			sort = LogSort.Date,
			descending = false,
			offset = 1,
			limit = 3
		};

		//We need at least 9 logs, with 4 filtered out and then apply offset/limit
		var mockLogs = FlowSeeder.GetLogFaker(client.Id, business.Id, null!, 49)
			.Generate(9)
			.Select((c, i) => {

				if (i == 0) c.clientId = otherClient.Id;
				if (i == 1) c.businessId = otherBusiness.Id;
				if (i == 2) c.dateTime = DateTime.MinValue;
				if (i == 3) c.dateTime = DateTime.MaxValue;
				return c;
			}).ToArray();

		_context.Logs.AddRange(mockLogs);
		_context.SaveChanges();

		// Act
		var result = _controller.ReadLogs(filter) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status200OK, result!.StatusCode);
		var logs = Assert.IsType<AptLog[]>(result!.Value);
		Assert.Equal(3, logs.Length);
		Assert.True(logs[0].dateTime < logs[2].dateTime);
	}

	[Fact]
	public async Task CreateLog_ReturnsCreated_WhenScheduleIsNull() {
		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var newLog = new AptLog(client.Id, business.Id, 30);

		_context.AddRange(client, business);
		_context.SaveChanges();

		// Act
		var result = await _controller.CreateLog(newLog) as CreatedAtActionResult;

		// Assert
		Assert.NotNull(result);
		var createdLog = Assert.IsType<AptLog>(result!.Value);
		Assert.Equal(StatusCodes.Status201Created, result!.StatusCode);
		Assert.Equal(newLog.Id, createdLog.Id);
	}

	[Fact]
	public async Task CreateLog_ReturnsCreated_WhenScheduleDoesNotExist() {
		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");

		_context.AddRange(client, business);
		_context.SaveChanges();

		var newLog = new AptLog(client.Id, business.Id, 30);
		newLog.scheduleId = "nonExistingId";

		// Act
		var result = await _controller.CreateLog(newLog) as BadRequestObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status400BadRequest, result!.StatusCode);
		Assert.Equal("Schedule does not exist", result!.Value);
	}

	[Fact]
	public async Task CreateLog_ReturnsCreated_WhenScheduleExists() {
		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var schedule = new AptSchedule(client.Id, business.Id, DateTime.Now);

		_context.AddRange(client, business, schedule);
		_context.SaveChanges();

		var newLog = new AptLog(client.Id, business.Id, 30);
		newLog.scheduleId = schedule.Id;

		// Act
		var result = await _controller.CreateLog(newLog) as CreatedAtActionResult;

		// Assert
		Assert.NotNull(result);
		var createdLog = Assert.IsType<AptLog>(result!.Value);
		Assert.Equal(StatusCodes.Status201Created, result!.StatusCode);
		Assert.Equal(newLog.Id, createdLog.Id);
	}

	[Fact]
	public async Task UpdateLog_ReturnsBadRequest_WhenLogNotFound() {
		// Arrange
		var nonExistingLog = new AptLog();
		// Act
		var result = await _controller.UpdateLog(nonExistingLog) as BadRequestObjectResult;
		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status400BadRequest, result!.StatusCode);
		Assert.Equal("Log does not exist", result!.Value);
	}

	[Fact]
	public async Task UpdateLog_ReturnsOk_WhenLogIsUpdated() {
		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var existingSchedule = new AptSchedule(client.Id, business.Id, DateTime.Now);
		var aptLog = new AptLog(client.Id, business.Id, 30);
		aptLog.dateTime = new DateTime(2024, 1, 1);
		aptLog.scheduleId = existingSchedule.Id;

		_context.AddRange(client, business, existingSchedule, aptLog);
		_context.SaveChanges();

		aptLog.dateTime = new DateTime(2024, 12, 12);

		// Act
		var result = await _controller.UpdateLog(aptLog) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.IsType<AptLog>(result!.Value);
		Assert.Equal(StatusCodes.Status200OK, result!.StatusCode);
		Assert.Equal(aptLog.Id, ((AptLog)result.Value!).Id);
		Assert.Equal(aptLog.dateTime, ((AptLog)result.Value!).dateTime);
	}

	[Fact]
	public async Task DeleteLog_ReturnsBadRequest_WhenLogNotFound() {
		// Act
		var result = await _controller.DeleteLog("nonExistingId") as BadRequestObjectResult;
		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status400BadRequest, result!.StatusCode);
		Assert.Equal("Log does not exist", result!.Value);
	}

	[Fact]
	public async Task DeleteLog_ReturnsNoContent_WhenLogExists() {
		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var aptLog = new AptLog(client.Id, business.Id, 30);

		_context.AddRange(client, business, aptLog);
		_context.SaveChanges();

		// Act
		var result = await _controller.DeleteLog(aptLog.Id) as NoContentResult;

		// Assert
		var logInDb = _context.Logs.Find(aptLog.Id);
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status204NoContent, result!.StatusCode);
		Assert.IsType<NoContentResult>(result);
		Assert.Null(logInDb);
	}
}
