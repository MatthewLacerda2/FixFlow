using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Controllers;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.DTO;
using Server.Models.Erros;

namespace FixFlow.Tests.Controllers;

public class ClientControllerTests {

	private readonly ServerContext _context;
	private readonly ClientController _controller;
	private readonly Mock<UserManager<Client>> _mockUserManager;

	public ClientControllerTests() {

		_context = new Util().SetupDbContextForTests();

		var store = new Mock<IUserStore<Client>>();
		_mockUserManager = new Mock<UserManager<Client>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

		_controller = new ClientController(_context, _mockUserManager.Object);
	}

	[Fact]
	public async Task GetClientRecord_ClientExists_ReturnsOk() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98999344788");
		business.Id = Guid.NewGuid().ToString();

		var client = new Client(business.Id, "98999344788", "fulano da silva bezerra", null, null, null);
		client.Id = Guid.NewGuid().ToString();

		_mockUserManager.Setup(x => x.FindByIdAsync(client.Id)).ReturnsAsync(client);

		_context.Business.Add(business);
		_context.Clients.Add(client);

		await _context.SaveChangesAsync();

		// Act
		var result = await _controller.GetClientRecord(client.Id);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var clientRecord = Assert.IsType<ClientRecord>(okResult.Value);
	}


	[Fact]
	public async Task GetClientRecord_ClientDoesNotExist_ReturnsBadRequest() {
		// Arrange
		var clientId = "non-existent-client-id";
		_mockUserManager.Setup(x => x.FindByIdAsync(clientId)).ReturnsAsync((Client)null!);

		// Act
		var result = await _controller.GetClientRecord(clientId);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(NotExistErrors.Client, badRequestResult.Value);
	}
}
