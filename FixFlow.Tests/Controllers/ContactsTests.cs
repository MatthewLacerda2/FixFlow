using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Server.Controllers;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Xunit;

public class ContactControllerTests
{
    private readonly DbContextOptions<ServerContext> _dbContextOptions;
    private readonly Mock<UserManager<Client>> _userManagerMock;

    public ContactControllerTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ServerContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var userStoreMock = new Mock<IUserStore<Client>>();
        _userManagerMock = new Mock<UserManager<Client>>(userStoreMock.Object);
        //_userManagerMock = new Mock<UserManager<Client>>(userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        // Ensure the database is created
        using (var context = new ServerContext(_dbContextOptions))
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
        }
    }

    [Fact]
    public async Task ReadContact_ReturnsContact_WhenContactExists()
    {
        // Arrange
        var contactId = "1";
        var contact = new AptContact("clientId123", "prev123");

        using (var context = new ServerContext(_dbContextOptions))
        {
            context.Contacts.Add(contact);
            await context.SaveChangesAsync();
        }

        using (var context = new ServerContext(_dbContextOptions))
        {
            var controller = new ContactController(context, _userManagerMock.Object);

            // Act
            var result = await controller.ReadContact(contactId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var returnedContact = Assert.IsType<AptContact>(result.Value);
            Assert.Equal(contactId, returnedContact.Id);
        }
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
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Contact does not exist", result.Value);
        }
    }
}
