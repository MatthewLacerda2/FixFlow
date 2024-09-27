using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Server.Controllers;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.Erros;
using Server.Models.Filters;
using Server.Seeder;

namespace FixFlow.Tests.Controllers;

public class AptScheduleControllerTests {

	private readonly ServerContext _context;
	private readonly AptScheduleController _controller;

	public AptScheduleControllerTests() {

		var connectionStringBuilder = new SqliteConnectionStringBuilder {
			DataSource = ":memory:"
		};
		var connection = new SqliteConnection(connectionStringBuilder.ToString());

		DbContextOptions<ServerContext> _dbContextOptions = new DbContextOptionsBuilder<ServerContext>()
			.UseSqlite(connection)
			.Options;

		_context = new ServerContext(_dbContextOptions);
		_context.Database.OpenConnection();
		_context.Database.EnsureCreated();

		_controller = new AptScheduleController(_context);
	}

	[Fact]
	public async Task ReadSchedule_ReturnsOk_WhenScheduleExists() {
		// Arrange
		var client = new Client("fulano", "123456789", "", "88263255", "fulano@hotmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var schedule = new AptSchedule(client.Id, business.Id, DateTime.Now);

		_context.AddRange(client, business, schedule);
		_context.SaveChanges();

		// Act
		var result = await _controller.ReadSchedule(schedule.Id) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status200OK, result!.StatusCode);
		var returnedSchedule = Assert.IsType<AptSchedule>(result.Value);
		Assert.Equal(schedule.Id, returnedSchedule.Id);
	}

	[Fact]
	public async Task ReadSchedule_ReturnsNotFound_WhenScheduleDoesNotExist() {
		// Act
		var result = await _controller.ReadSchedule("nonExistingId") as NotFoundObjectResult;
		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status404NotFound, result!.StatusCode);
		Assert.Equal(NotExistErrors.AptSchedule, result.Value);
	}

	[Fact]
	public async Task ReadSchedules_ReturnsEmptyArray_WhenNoLogsMatchFilter() {
		// Arrange
		var client = new Client("fulano", "123456789", null!, "88263255", "fulano@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");

		var otherClient = new Client("cicrano", "987654321", null!, "9898263255", "cicrano@gmail.com", true);
		var otherBusiness = new Business("otherbusiness", "60742928000", "4560123", "98993265849", "otherbusiness@gmail.com", "");

		var filter = new AptScheduleFilter(client.Id, business.Id, false, new DateOnly(2023, 1, 1), new DateOnly(2025, 3, 1));
		filter.offset = 1;
		filter.limit = 3;

		//We need at least 5 logs, with 4 filtered out and then apply offset
		var mockSchedules = FlowSeeder.GetScheduleFaker(client.Id, business.Id, null!, 49)
			.Generate(5)
			.Select((c, i) => {

				if (i == 0) c.ClientId = otherClient.Id;
				if (i == 1) c.businessId = otherBusiness.Id;
				if (i == 2) c.dateTime = DateTime.MinValue;
				if (i == 3) c.dateTime = DateTime.MaxValue;
				return c;
			}).ToArray();

		_context.AddRange(client, business, otherClient, otherBusiness);
		_context.AddRange(mockSchedules);
		_context.SaveChanges();

		// Act
		var result = await _controller.ReadSchedules(filter) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status200OK, result!.StatusCode);
		var schedules = Assert.IsType<AptSchedule[]>(result!.Value);
		Assert.Empty(schedules);
	}

	[Fact]
	public async Task ReadSchedules_ReturnsArray_WhenSchedulesMatchFilter() {
		// Arrange
		var client = new Client("fulano", "123456789", null!, "88263255", "fulano@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");

		var otherClient = new Client("cicrano", "987654321", null!, "9898263255", "cicrano@gmail.com", true);
		var otherBusiness = new Business("otherbusiness", "60742928000", "4560123", "98993265849", "otherbusiness@gmail.com", "");

		var filter = new AptScheduleFilter(client.Id, business.Id, false, new DateOnly(2023, 1, 1), new DateOnly(2025, 3, 1));
		filter.offset = 1;
		filter.limit = 3;

		//We need at least 11 logs, with 6 filtered out and then apply offset/limit
		var mockSchedules = FlowSeeder.GetScheduleFaker(client.Id, business.Id, null!, 49)
			.Generate(9)
			.Select((c, i) => {

				if (i == 0) c.ClientId = otherClient.Id;
				if (i == 1) c.businessId = otherBusiness.Id;
				if (i == 2) c.dateTime = DateTime.MinValue;
				if (i == 3) c.dateTime = DateTime.MaxValue;
				return c;
			}).ToArray();

		_context.AddRange(client, business, otherClient, otherBusiness);
		_context.AddRange(mockSchedules);
		_context.SaveChanges();

		// Act
		var result = await _controller.ReadSchedules(filter) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status200OK, result!.StatusCode);
		var schedules = Assert.IsType<AptSchedule[]>(result!.Value);
		Assert.Equal(3, schedules.Length);
		Assert.True(schedules[0].dateTime > schedules[2].dateTime);
	}

	[Fact]
	public async Task CreateSchedule_ReturnsCreated_WhenContactExist() {
		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var log = new AptLog(client.Id, business.Id, 30);
		var contact = new AptContact(client.Id, business.Id, log.Id, DateTime.Now);
		var newSchedule = new AptSchedule(client.Id, business.Id, DateTime.Now);
		newSchedule.contactId = contact.Id;

		_context.AddRange(client, business, log, contact);
		_context.SaveChanges();

		// Act
		var result = await _controller.CreateSchedule(newSchedule) as CreatedAtActionResult;

		// Assert
		Assert.NotNull(result);
		var createdSchedule = Assert.IsType<AptSchedule>(result!.Value);
		Assert.Equal(StatusCodes.Status201Created, result!.StatusCode);
		Assert.Equal(newSchedule.Id, createdSchedule.Id);
	}

	[Fact]
	public async Task CreateSchedule_ReturnsBadRequest_WhenContactDoesNotExist() {
		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");

		_context.AddRange(client, business);
		_context.SaveChanges();

		var newSchedule = new AptSchedule(client.Id, business.Id, DateTime.Now);
		newSchedule.contactId = "nonExistingId";

		// Act
		var result = await _controller.CreateSchedule(newSchedule) as BadRequestObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status400BadRequest, result!.StatusCode);
		Assert.Equal(NotExistErrors.AptContact, result!.Value);
	}

	[Fact]
	public async Task UpdateSchedule_ReturnsBadRequest_WhenScheduleDoesNotExists() {
		// Arrange
		var nonExistingSchedule = new AptSchedule();
		// Act
		var result = await _controller.UpdateSchedule(nonExistingSchedule) as BadRequestObjectResult;
		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status400BadRequest, result!.StatusCode);
		Assert.Equal(NotExistErrors.AptSchedule, result!.Value);
	}

	[Fact]
	public async Task UpdateSchedule_ReturnsBadRequest_WhenContactDoesNotExist() {
		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var existingSchedule = new AptSchedule(client.Id, business.Id, DateTime.Now);

		_context.AddRange(client, business, existingSchedule);
		_context.SaveChanges();

		existingSchedule.contactId = "nonExistingId";

		// Act
		var result = await _controller.UpdateSchedule(existingSchedule) as BadRequestObjectResult;
		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status400BadRequest, result!.StatusCode);
		Assert.Equal(NotExistErrors.AptContact, result!.Value);
	}

	[Fact]
	public async Task UpdateSchedule_ReturnsOk_WhenContactExists() {
		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var aptLog = new AptLog(client.Id, business.Id, 30);
		var contact = new AptContact(client.Id, business.Id, aptLog.Id, DateTime.Now);
		var existingSchedule = new AptSchedule(client.Id, business.Id, DateTime.Now);

		_context.AddRange(client, business, aptLog, contact, existingSchedule);
		_context.SaveChanges();

		existingSchedule.dateTime = new DateTime(2024, 12, 12);
		existingSchedule.contactId = contact.Id;

		// Act
		var result = await _controller.UpdateSchedule(existingSchedule) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		var value = Assert.IsType<AptSchedule>(result!.Value);
		Assert.Equal(StatusCodes.Status200OK, result!.StatusCode);
		Assert.Equal(existingSchedule.Id, value!.Id);
		Assert.Equal(existingSchedule.dateTime, value.dateTime);
	}

	[Fact]
	public async Task DeleteSchedule_ReturnsBadRequest_WhenScheduleNotFound() {
		// Act
		var result = await _controller.DeleteSchedule("nonExistingId") as BadRequestObjectResult;
		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status400BadRequest, result!.StatusCode);
		Assert.Equal(NotExistErrors.AptSchedule, result!.Value);
	}

	[Fact]
	public async Task DeleteSchedule_ReturnsNoContent_WhenScheduleExists() {
		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var aptSchedule = new AptSchedule(client.Id, business.Id, DateTime.Now);

		_context.AddRange(client, business, aptSchedule);
		_context.SaveChanges();

		// Act
		var result = await _controller.DeleteSchedule(aptSchedule.Id) as NoContentResult;

		// Assert
		var scheduleInDb = _context.Schedules.Find(aptSchedule.Id);
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status204NoContent, result!.StatusCode);
		Assert.IsType<NoContentResult>(result);
		Assert.Null(scheduleInDb);
	}
}
