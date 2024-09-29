using FixFlow.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace FixFlow.Tests.Controllers;

public class OTPControllerTests {

	private readonly ServerContext _context;
	private readonly OTPController _controller;

	public OTPControllerTests() {

		var connectionStringBuilder = new SqliteConnectionStringBuilder();
		connectionStringBuilder.DataSource = ":memory:";

		var connection = new SqliteConnection(connectionStringBuilder.ToString());

		DbContextOptions<ServerContext> _dbContextOptions = new DbContextOptionsBuilder<ServerContext>()
			.UseSqlite(connection)
			.Options;

		_context = new ServerContext(_dbContextOptions);
		_context.Database.OpenConnection();
		_context.Database.EnsureCreated();

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
