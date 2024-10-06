using FixFlow.Server.Controllers.Users;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;

namespace FixFlow.Tests.Controllers;

public class IdlePeriodControllerTests {

	private readonly ServerContext _context;
	private readonly IdlePeriodController _controller;

	public IdlePeriodControllerTests() {

		_context = new Util().SetupDbContextForTests();

		_controller = new IdlePeriodController(_context);
	}

	[Fact]
	public async Task GetIdlePeriodsAtDate_ReturnsIdlePeriods() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "987.6543.321-8901", "98988263255");
		var badBusiness = new Business("lenda", "lenda@gmail.com", "123.4567.789-8901", "98988263255");
		_context.Business.AddRange(business, badBusiness);

		var idlePeriod = new IdlePeriod(business.Id, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), "Test");
		var badDatePeriod = new IdlePeriod(business.Id, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2), "Test");
		var badBiPeriod = new IdlePeriod(badBusiness.Id, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), "Test");

		_context.IdlePeriods.AddRange(idlePeriod, badDatePeriod, badBiPeriod);
		await _context.SaveChangesAsync();

		var request = new BusinessIdlePeriodsRequest(business.Id, DateTime.UtcNow);

		// Act
		var result = await _controller.GetIdlePeriodsAtDate(request) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		var idlePeriods = Assert.IsType<IdlePeriod[]>(result!.Value);
		Assert.Single(idlePeriods);
	}

	[Fact]
	public async Task GetIdlePeriodsAtDate_BusinessNotFound_ReturnsBadRequest() {
		// Arrange
		var request = new BusinessIdlePeriodsRequest("666", DateTime.UtcNow);
		// Act
		var result = await _controller.GetIdlePeriodsAtDate(request);
		// Assert
		Assert.IsType<BadRequestObjectResult>(result);
	}

	[Fact]
	public async Task CreateIdlePeriod_ValidIdlePeriod_ReturnsOk() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "987.6543.321-8901", "98988263255");
		_context.Business.Add(business);
		await _context.SaveChangesAsync();

		var idlePeriod = new IdlePeriod(business.Id, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2), "Test");

		// Act
		var result = await _controller.CreateIdlePeriod(idlePeriod) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		var createdIdlePeriod = Assert.IsType<IdlePeriod>(result!.Value);
		Assert.Equal(idlePeriod.BusinessId, createdIdlePeriod.BusinessId);
		Assert.Equal(idlePeriod.start, createdIdlePeriod.start);
		Assert.Equal(idlePeriod.finish, createdIdlePeriod.finish);
		Assert.Equal(idlePeriod.Name, createdIdlePeriod.Name);
	}

	[Fact]
	public async Task CreateIdlePeriod_BusinessNotFound_ReturnsBadRequest() {
		// Arrange
		var idlePeriod = new IdlePeriod("666", DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2), "Test");
		// Act
		var result = await _controller.CreateIdlePeriod(idlePeriod);
		// Assert
		Assert.IsType<BadRequestObjectResult>(result);
	}

	[Fact]
	public async Task CreateIdlePeriod_IdlePeriodHasPassed_ReturnsBadRequest() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "987.6543.321-8901", "98988263255");
		var idlePeriod = new IdlePeriod(business.Id, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1), "Test");
		_context.Business.Add(business);
		await _context.SaveChangesAsync();
		// Act
		var result = await _controller.CreateIdlePeriod(idlePeriod);
		// Assert
		Assert.IsType<BadRequestObjectResult>(result);
	}

	[Fact]
	public async Task RemoveIdlePeriod_ValidId_ReturnsNoContent() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "987.6543.321-8901", "98988263255");
		_context.Business.Add(business);
		await _context.SaveChangesAsync();

		var idlePeriod = new IdlePeriod(business.Id, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2), "Test");
		_context.IdlePeriods.Add(idlePeriod);
		await _context.SaveChangesAsync();

		// Act
		var result = await _controller.RemoveIdlePeriod(idlePeriod.Id);

		// Assert
		Assert.IsType<NoContentResult>(result);
		Assert.Null(_context.IdlePeriods.Find(idlePeriod.Id));
	}

	[Fact]
	public async Task RemoveIdlePeriod_InvalidId_ReturnsBadRequest() {
		// Arrange
		var invalidId = "invalid-id";
		// Act
		var result = await _controller.RemoveIdlePeriod(invalidId);
		// Assert
		Assert.IsType<BadRequestObjectResult>(result);
	}
}
