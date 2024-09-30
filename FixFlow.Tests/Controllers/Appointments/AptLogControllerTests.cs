using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Controllers;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.Erros;
using Server.Models.Filters;

namespace FixFlow.Tests.Controllers;

public class AptLogControllerTests {

	private readonly ServerContext _context;
	private readonly AptLogController _controller;
	private readonly Mock<UserManager<Business>> _mockUserManager;
	private readonly Mock<UserManager<Client>> _mockClientManager;

	public AptLogControllerTests() {

		_context = new Util().SetupDbContextForTests();

		var store = new Mock<IUserStore<Business>>();
		_mockUserManager = new Mock<UserManager<Business>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

		var clientStore = new Mock<IUserStore<Client>>();
		_mockClientManager = new Mock<UserManager<Client>>(clientStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);

		_controller = new AptLogController(_context, _mockUserManager.Object, _mockClientManager.Object);
	}

	[Fact]
	public async Task ReadLogs_ReturnsFilteredLogs() {
		var client1Name = "fulano da silva";

		var business1 = new Business("b-1", "b-1@gmail.com", "789.4561.123-0001", "98999344788");
		var business2 = new Business("b-2", "b-2@gmail.com", "123.4567.789-0001", "98988263255");
		var client1 = new Client(business1.Id, "789456123", client1Name, null, null, null);
		var client2 = new Client(business2.Id, "123456789", "ciclano da silva", null, null, null);
		var client3 = new Client(business1.Id, "123456789", "beltrano da silva", null, null, null);

		_context.Business.AddRange(business1, business2);
		_context.Clients.AddRange(client1, client2, client3);

		// Arrange
		var logs = new List<AptLog>();
		for (int i = 0; i < 10; i++) {
			logs.Add(new AptLog {
				Id = Guid.NewGuid().ToString(),
				BusinessId = business1.Id,
				ClientId = client1.Id,
				dateTime = DateTime.Now.AddDays(-i),
				price = i * 10,
				service = $"Option {i}"
			});
		}

		logs[0].BusinessId = business2.Id;
		logs[0].ClientId = client2.Id;

		logs[1] = logs[5];
		logs[1].price = 40;

		_context.Logs.AddRange(logs);
		_context.SaveChanges();

		var filter = new AptLogFilter {
			businessId = business1.Id,
			client = "fulano ",
			minPrice = 30,
			maxPrice = 80,
			minDateTime = DateTime.Now.AddDays(-7),
			maxDateTime = DateTime.Now.AddDays(-3),
			sort = LogSort.Price,
			descending = true,
			offset = 1,
			limit = 1
		};

		// Act
		var result = await _controller.ReadLogs(filter) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(200, result!.StatusCode);
		var filteredLogs = result.Value as IEnumerable<AptLog>;
		Assert.Single(filteredLogs);

		Assert.Equal(client1Name, filteredLogs!.First().Client.FullName);
		Assert.Equal(40, filteredLogs!.First().price);
		Assert.Equal(DateTime.Now.AddDays(-4).Date, filteredLogs!.First().dateTime.Date);
		Assert.Equal("Option 4", filteredLogs!.First().service);
	}

	[Fact]
	public async Task CreateLog_ClientDoesNotExist_ReturnsBadRequest() {
		// Arrange
		var createLog = new CreateAptLog("client-id", "business-id", null, DateTime.Now, 100, null, null, DateTime.Now.AddDays(30));

		_context.Business.Add(new Business("business", "business@example.com", "123456789", "1234567890"));
		_context.SaveChanges();

		// Act
		var result = await _controller.CreateLog(createLog) as BadRequestObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(400, result!.StatusCode);
		Assert.Equal(NotExistErrors.Client, result.Value);
	}

	[Fact]
	public async Task CreateLog_UnlistedService_ReturnsBadRequest() {
		// Arrange
		var business = new Business("power busy", "business@example.com", "123456789", "1234567890") {
			allowListedServicesOnly = true,
			services = ["Service 1", "Service 2"]
		};
		var client = new Client(business.Id, "98988263255", "Client Name", null, null, null);
		_context.Business.Add(business);
		_context.Clients.Add(client);
		_context.SaveChanges();

		var createLog = new CreateAptLog(client.Id, business.Id, null, DateTime.Now, 100, "UnlistedService", null, DateTime.Now.AddDays(30));

		// Act
		var result = await _controller.CreateLog(createLog) as BadRequestObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(400, result!.StatusCode);
		Assert.Equal(ValidatorErrors.UnlistedService, result.Value);
	}

	[Fact]
	public async Task CreateLog_Successful_ReturnsCreated() {
		// Arrange
		var business = new Business("power busy", "business@example.com", "123456789", "1234567890") {
			allowListedServicesOnly = true,
			services = ["Service 1", "Service 2"]
		};
		var client = new Client(business.Id, "98988263255", "Client Name", null, null, null);
		var schedule = new AptSchedule(client.Id, business.Id, DateTime.Now, 100);
		_context.Business.Add(business);
		_context.Clients.Add(client);
		_context.Schedules.Add(schedule);
		_context.SaveChanges();

		var createLog = new CreateAptLog(client.Id, business.Id, schedule.Id, DateTime.Now, 100, "Service 2", null, DateTime.Now.AddDays(30));

		// Act
		var result = await _controller.CreateLog(createLog) as CreatedAtActionResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(201, result!.StatusCode);
		var createdLog = result.Value as AptLog;
		Assert.NotNull(createdLog);
		Assert.Equal(createLog.ClientId, createdLog!.ClientId);
		Assert.Equal(createLog.BusinessId, createdLog.BusinessId);
		Assert.Equal(createLog.service, createdLog.service);
		Assert.Equal(createLog.price, createdLog.price);
		Assert.Equal(createLog.dateTime, createdLog.dateTime);

		var contact = _context.Contacts.Where(x => x.aptLogId == createdLog.Id).FirstOrDefault();

		Assert.NotNull(contact);
		Assert.Equal(contact!.dateTime, createLog.whenShouldClientComeBack);
	}

	[Fact]
	public async Task UpdateLog_AptLogDoesNotExist_ReturnsBadRequest() {
		// Arrange
		var upLog = new UpdateAptLog("non-exist-log", null, DateTime.Now, null, 200, null);

		// Act
		var result = await _controller.UpdateLog(upLog) as BadRequestObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(400, result!.StatusCode);
		Assert.Equal(NotExistErrors.AptLog, result.Value);
	}

	[Fact]
	public async Task UpdateLog_UnlistedService_ReturnsBadRequest() {
		// Arrange
		var business = new Business("business-id", "business@example.com", "123456789", "1234567890") {
			allowListedServicesOnly = true,
			services = ["Service 1", "Service 2"]
		};
		var client = new Client(business.Id, "123456789", "Client Name", null, null, null);
		var log = new AptLog {
			ClientId = client.Id,
			BusinessId = business.Id,
			dateTime = DateTime.Now,
			price = 100,
			service = "Service 2"
		};

		_context.Business.Add(business);
		_context.Clients.Add(client);
		_context.Logs.Add(log);
		_context.SaveChanges();

		var upLog = new UpdateAptLog(log.Id, null, DateTime.Now, "UnlistedService", 100, null);

		// Act
		var result = await _controller.UpdateLog(upLog) as BadRequestObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(400, result!.StatusCode);
		Assert.Equal(ValidatorErrors.UnlistedService, result.Value);
	}

	[Fact]
	public async Task UpdateLog_ScheduleDoesNotExist_ReturnsBadRequest() {
		// Arrange
		var business = new Business("business-id", "business@example.com", "123456789", "1234567890") {
			allowListedServicesOnly = true,
			services = ["Service 1", "Service 2"]
		};
		var client = new Client(business.Id, "123456789", "Client Name", null, null, null);
		var log = new AptLog {
			ClientId = client.Id,
			BusinessId = business.Id,
			dateTime = DateTime.Now,
			price = 100,
			service = "Service 2"
		};

		_context.Business.Add(business);
		_context.Clients.Add(client);
		_context.Logs.Add(log);
		_context.SaveChanges();

		var upLog = new UpdateAptLog(log.Id, "non-exist-schedule-id", DateTime.Now, "Service 2", 100, null);

		// Act
		var result = await _controller.UpdateLog(upLog) as BadRequestObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(400, result!.StatusCode);
		Assert.Equal(NotExistErrors.AptSchedule, result.Value);
	}

	[Fact]
	public async Task UpdateLog_Successful_ReturnsOk() {
		// Arrange
		var business = new Business("business-id", "business@example.com", "123456789", "1234567890") {
			allowListedServicesOnly = true,
			services = ["Service 1", "Service 2"]
		};
		var client = new Client(business.Id, "123456789", "Client Name", null, null, null);
		var schedule = new AptSchedule(client.Id, business.Id, DateTime.Now.AddHours(-1), 100);
		var log = new AptLog {
			ClientId = client.Id,
			BusinessId = business.Id,
			scheduleId = null,
			dateTime = DateTime.Now.AddHours(-1),
			price = 100,
			service = "Service 1"
		};

		_context.Business.Add(business);
		_context.Clients.Add(client);
		_context.Schedules.Add(schedule);
		_context.Logs.Add(log);
		_context.SaveChanges();

		var upLog = new UpdateAptLog(log.Id, schedule.Id, DateTime.Now, "Service 2", 200, "description");

		// Act
		var result = await _controller.UpdateLog(upLog) as OkObjectResult;

		// Assert
		Assert.Equal(200, result!.StatusCode);
		Assert.NotNull(result);
		var updatedLog = Assert.IsType<AptLog>(result.Value);
		Assert.Equal(upLog.ScheduleId, updatedLog.scheduleId);
		Assert.Equal(upLog.dateTime, updatedLog.dateTime);
		Assert.Equal(upLog.Service, updatedLog.service);
		Assert.Equal(upLog.Price, updatedLog.price);
		Assert.Equal(upLog.Description, updatedLog.description);
	}

	[Fact]
	public async Task DeleteLog_LogDoesNotExist_ReturnsBadRequest() {
		// Arrange
		var logId = "non-existent-log-id";

		// Act
		var result = await _controller.DeleteLog(logId) as BadRequestObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(400, result!.StatusCode);
		Assert.Equal(NotExistErrors.AptLog, result.Value);
	}

	[Fact]
	public async Task DeleteLog_LogExists_ReturnsNoContent() {
		// Arrange
		var business = new Business("business-id", "business@example.com", "123456789", "1234567890");
		var client = new Client(business.Id, "123456789", "Client Name", null, null, null);
		var log = new AptLog {
			Id = "log-id",
			ClientId = client.Id,
			BusinessId = business.Id,
			dateTime = DateTime.Now,
			price = 100,
			service = "Service 1"
		};
		var contact = new AptContact(log, DateTime.Now.AddDays(-30));

		_context.Business.Add(business);
		_context.Clients.Add(client);
		_context.Logs.Add(log);
		_context.Contacts.Add(contact);
		_context.SaveChanges();

		// Act
		var result = await _controller.DeleteLog(log.Id) as NoContentResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(204, result!.StatusCode);

		var deletedLog = _context.Logs.Find(log.Id);
		Assert.Null(deletedLog);

		var deletedContact = _context.Contacts.Where(x => x.aptLogId == log.Id).FirstOrDefault();
		Assert.Null(deletedContact);
	}
}