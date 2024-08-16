using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Moq;
using Server.Controllers;
using Server.Models.Appointments;
using Server.Models;
using Server.Data;
using Microsoft.Data.Sqlite;

public class ContactControllerTests
{
    private readonly DbContextOptions<ServerContext> _dbContextOptions;
    private readonly Mock<UserManager<Client>> _userManagerMock;
    private readonly ServerContext _context;
    private readonly ContactController _controller;

    public ContactControllerTests()
    {
        var connectionStringBuilder = new SqliteConnectionStringBuilder
        {
            DataSource = ":memory:"
        };
        var connection = new SqliteConnection(connectionStringBuilder.ToString());

        // Configure DbContext to use SQLite in-memory database
        _dbContextOptions = new DbContextOptionsBuilder<ServerContext>()
            .UseSqlite(connection)
            .Options;

        // Mock UserManager dependencies
        var userStoreMock = new Mock<IUserStore<Client>>();
        _userManagerMock = new Mock<UserManager<Client>>(userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);


        // Initialize DbContext with the configured options
        _context = new ServerContext(_dbContextOptions);

        // Open SQLite connection and ensure database is created
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _controller = new ContactController(_context, _userManagerMock.Object);
    }

    [Fact]
    public async Task ReadContact_ReturnsContact_WhenContactExists()
    {
        // Arrange
        var client = new Client("fulano", "123456789", "", "88263255", "fulano@hotmail.com", true);
        var business = new Business("business","60742928330","5550123","98999344788","business@gmail.com","");
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
        Assert.Equal(200, result!.StatusCode);
        var returnedContact = Assert.IsType<AptContact>(result.Value);
        Assert.Equal(contact.Id, returnedContact.Id);
    }

    [Fact]
    public async Task ReadContact_ReturnsNotFound_WhenContactDoesNotExist()
    {
        // Arrange
        var contactId = "2";
        var controller = new ContactController(_context, _userManagerMock.Object);

        // Act
        var result = await controller.ReadContact(contactId) as NotFoundObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result!.StatusCode);
        Assert.Equal("Contact does not exist", result.Value);
    }
}
