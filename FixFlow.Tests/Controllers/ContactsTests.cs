using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Server.Controllers;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;

public class ContactControllerTests
{
    private readonly DbContextOptions<ServerContext> _dbContextOptions;
    private readonly Mock<UserManager<Client>> _userManagerMock;
    private readonly ServerContext _context;

    public ContactControllerTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ServerContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var userStoreMock = new Mock<IUserStore<Client>>();
        _userManagerMock = new Mock<UserManager<Client>>(userStoreMock.Object);

        _context = new ServerContext(_dbContextOptions);

        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task ReadContact_ReturnsContact_WhenContactExists()
    {
        // Arrange
        var contactId = "1";
        var contact = new AptContact("clientId123", "prev123");

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();

        var controller = new ContactController(_context, _userManagerMock.Object);

        // Act
        var result = await controller.ReadContact(contactId) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result!.StatusCode);
        var returnedContact = Assert.IsType<AptContact>(result.Value);
        Assert.Equal(contactId, returnedContact.Id);

    }

    [Fact]
    public async Task ReadContact_ReturnsNotFound_WhenContactDoesNotExist()
    {
        // Arrange
        var contactId = "2";

        using (var context = new ServerContext(_dbContextOptions))
        {
            var controller = new ContactController(context, _userManagerMock.Object);

            // Act
            var result = await controller.ReadContact(contactId) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result!.StatusCode);
            Assert.Equal("Contact does not exist", result.Value);
        }
    }
}
