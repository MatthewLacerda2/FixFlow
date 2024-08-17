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

public class AptContactControllerTests {

	private readonly DbContextOptions<ServerContext> _dbContextOptions;
	private readonly Mock<UserManager<Client>> _userManagerMock;
	private readonly ServerContext _context;
	private readonly AptContactController _controller;

	public AptContactControllerTests() {

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

		_controller = new AptContactController(_context, _userManagerMock.Object);
	}

	[Fact]
	public async Task ReadContact_ReturnsContact_WhenContactExists() {

		// Arrange
		var client = new Client("fulano", "123456789", "", "88263255", "fulano@hotmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var prevApt = new AptLog(client.Id, business.Id, 30);
		var contact = new AptContact(client.Id, business.Id, prevApt.Id);

		_context.Clients.Add(client);
		_context.Business.Add(business);
		_context.Logs.Add(prevApt);
		_context.Contacts.Add(contact);
		_context.SaveChanges();

		// Act
		var result = await _controller.ReadContact(contact.Id) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status200OK, result!.StatusCode);
		var returnedContact = Assert.IsType<AptContact>(result.Value);
		Assert.Equal(contact.Id, returnedContact.Id);
	}

	[Fact]
	public async Task ReadContact_ReturnsNotFound_WhenContactDoesNotExist() {

		// Arrange
		var contactId = "2";
		var controller = new AptContactController(_context, _userManagerMock.Object);

		// Act
		var result = await controller.ReadContact(contactId) as NotFoundObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status404NotFound, result!.StatusCode);
		Assert.Equal("Contact does not exist", result.Value);
	}

	[Fact]
	public void ReadContact_ReturnsEmptyArray_WhenNoContactsMatchFilter() {

		// Arrange
		var filter = new AptContactFilter(null!, null!, null!, DateOnly.MinValue, DateOnly.MaxValue);

		// Act
		var result = _controller.ReadContacts(filter) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status200OK, result!.StatusCode);
		var contacts = Assert.IsType<AptContact[]>(result!.Value);
		Assert.Empty(contacts);
	}

	[Fact]
	public void ReadContact_FiltersContacts() {

		// Arrange
		var client = new Client("fulano", "123456789", null!, "88263255", "fulano@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var aptLog = new AptLog(client.Id, business.Id, 30);

		var otherClient = new Client("cicrano", "987654321", null!, "9898263255", "cicrano@gmail.com", true);
		var otherBusiness = new Business("otherbusiness", "60742928000", "4560123", "98993265849", "otherbusiness@gmail.com", "");
		var otherAptLog = new AptLog(client.Id, business.Id, 30);

		_context.AddRange(client, otherClient, business, otherBusiness, aptLog, otherAptLog);

		var filter = new AptContactFilter(client.Id, business.Id, aptLog.Id, new DateOnly(2023, 1, 1), new DateOnly(2025, 3, 1)) {
			descending = true,
			offset = 1,
			limit = 3
		};

		//We need at least 10 contacts, with 5 filtered out and then apply offset/limit
		var mockContacts = FlowSeeder.GetContactFaker(client.Id, business.Id, aptLog.Id, 49)
			.Generate(10)
			.Select((c, i) => {
				if (i == 0) c.ClientId = otherClient.Id;
				if (i == 1) c.businessId = otherBusiness.Id;
				if (i == 2) c.aptLogId = otherAptLog.Id;
				if (i == 3) c.dateTime = DateTime.MinValue;
				if (i == 4) c.dateTime = DateTime.MaxValue;
				return c;
			}).ToArray();

		_context.Contacts.AddRange(mockContacts);
		_context.SaveChanges();

		// Act
		var result = _controller.ReadContacts(filter) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal(StatusCodes.Status200OK, result!.StatusCode);
		var contacts = Assert.IsType<AptContact[]>(result!.Value);
		Assert.Equal(3, contacts.Length);
		Assert.True(contacts[0].dateTime < contacts[2].dateTime);
	}

	[Fact]
	public async Task CreateContact_ReturnsCreated_WhenValid() {

		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var aptLog = new AptLog(client.Id, business.Id, 30);

		_context.AddRange(client, business, aptLog);
		_context.SaveChanges();

		var newContact = FlowSeeder.GetContactFaker(client.Id, business.Id, aptLog.Id, 49).Generate(1).First();

		_userManagerMock.Setup(um => um.FindByIdAsync(newContact.ClientId)).ReturnsAsync(client);

		// Act
		var result = await _controller.CreateContact(newContact) as CreatedAtActionResult;

		// Assert
		Assert.NotNull(result);
		var createdContact = Assert.IsType<AptContact>(result!.Value);
		Assert.Equal(StatusCodes.Status201Created, result!.StatusCode);
		Assert.Equal(newContact.ClientId, createdContact.ClientId);
	}

	[Fact]
	public async Task CreateContact_ReturnsBadRequest_WhenLogNotFound() {

		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");

		_context.AddRange(client, business);
		_context.SaveChanges();

		var newContact = FlowSeeder.GetContactFaker(client.Id, business.Id, "nonExistentLogId", 49)
			.Generate(1)
			.First();

		_userManagerMock.Setup(um => um.FindByIdAsync(newContact.ClientId)).ReturnsAsync(client);

		// Act
		var result = await _controller.CreateContact(newContact) as BadRequestObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.Equal("Log does not exist", result!.Value);
	}

	[Fact]
	public async Task UpdateContact_ReturnsBadRequest_WhenContactNotFound() {

		// Arrange
		var nonExistingContact = new AptContact();
		// Act
		var result = await _controller.UpdateContact(nonExistingContact) as BadRequestObjectResult;
		// Assert
		Assert.NotNull(result);
		Assert.Equal("Contact does not exist", result!.Value);
	}

	[Fact]
	public async Task UpdateContact_ReturnsOk_WhenContactIsUpdated() {

		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var aptLog = new AptLog(client.Id, business.Id, 30);

		var existingContact = new AptContact(client.Id, business.Id, aptLog.Id);

		_context.AddRange(client, business, aptLog, existingContact);
		_context.SaveChanges();

		existingContact.dateTime = new DateTime(2024, 12, 12);

		// Act
		var result = await _controller.UpdateContact(existingContact) as OkObjectResult;

		// Assert
		Assert.NotNull(result);
		Assert.IsType<AptContact>(result!.Value);
		Assert.Equal(StatusCodes.Status200OK, result!.StatusCode);
		Assert.Equal(existingContact.Id, ((AptContact)result.Value!).Id);
		Assert.Equal(existingContact.dateTime, ((AptContact)result.Value!).dateTime);
	}

	[Fact]
	public async Task DeleteContact_ReturnsBadRequest_WhenContactNotFound() {
		// Arrange
		var nonExistingId = "nonExistingId";
		// Act
		var result = await _controller.DeleteContact(nonExistingId) as BadRequestObjectResult;
		// Assert
		Assert.NotNull(result);
		Assert.Equal("Contact does not exist", result!.Value);
	}

	[Fact]
	public async Task DeleteContact_ReturnsOk_WhenContactIsUpdated() {

		// Arrange
		var client = new Client("validClient", "123456789", null!, "123456789", "validClient@gmail.com", true);
		var business = new Business("business", "60742928330", "5550123", "98999344788", "business@gmail.com", "");
		var aptLog = new AptLog(client.Id, business.Id, 30);

		var existingContact = new AptContact(client.Id, business.Id, aptLog.Id);

		_context.AddRange(client, business, aptLog, existingContact);
		_context.SaveChanges();

		// Act
		var result = await _controller.DeleteContact(existingContact.Id) as NoContentResult;

		// Assert
		var contactInDb = _context.Contacts.Find(existingContact.Id);
		Assert.NotNull(result);
		Assert.IsType<NoContentResult>(result);
		Assert.Null(contactInDb);
	}
}
