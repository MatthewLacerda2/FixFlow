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
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");
		business.Id = Guid.NewGuid().ToString();

		var client = new Client(business.Id, "98988263255", "fulano da silva bezerra", null, null, null);
		client.Id = Guid.NewGuid().ToString();

		_mockUserManager.Setup(x => x.FindByIdAsync(client.Id)).ReturnsAsync(client);

		_context.Business.Add(business);
		_context.Clients.Add(client);

		_context.SaveChanges();

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

	[Fact]
	public async Task ReadClients_ReturnsFilteredClients() {
		// Arrange
		var business1 = new Business("Business1", "business1@gmail.com", "123.4567.789-0001", "98999344788");
		var business2 = new Business("Business2", "business2@gmail.com", "123.4567.789-0002", "98999344789");
		business1.Id = Guid.NewGuid().ToString();
		business2.Id = Guid.NewGuid().ToString();

		_context.Business.AddRange(business1, business2);

		Client[] clients = new Client[6];

		for (int i = 0; i < 6; i++) {
			var client = new Client(business1.Id, $"9899934478{i}", "matthew de la cerda", null, null, null);

			client.Id = Guid.NewGuid().ToString();
			clients[i] = client;
		}

		clients[0].BusinessId = business2.Id;
		clients[1].FullName = "matheus lacerda";

		_context.Clients.AddRange(clients);
		await _context.SaveChangesAsync();

		// Act
		var result = await _controller.ReadClients(business1.Id, 1, 1, "matthew");

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var clientsArray = Assert.IsType<ClientDTO[]>(okResult.Value);
		Assert.Single(clientsArray);
		Assert.Equal(clients[3].PhoneNumber, clientsArray[0].PhoneNumber);
	}

	[Fact]
	public async Task CreateClient_ValidClient_ReturnsCreated() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");
		var clientCreate = new ClientCreate(business.Id, "John Doe", "789.456.123-90", null, "1234567890", "client@gmail.com");

		_mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Client>())).ReturnsAsync(IdentityResult.Success);

		_context.Business.Add(business);
		_context.SaveChanges();

		// Act
		var result = await _controller.CreateClient(clientCreate);

		// Assert
		var createdResult = Assert.IsType<CreatedAtActionResult>(result);
		var clientDto = Assert.IsType<ClientDTO>(createdResult.Value);
		Assert.Equal(clientCreate.PhoneNumber, clientDto.PhoneNumber);
		Assert.Equal(clientCreate.FullName, clientDto.FullName);
		Assert.Equal(clientCreate.Email, clientDto.Email);
		Assert.Equal(clientCreate.CPF, clientDto.CPF);
	}

	[Fact]
	public async Task CreateClient_PhoneNumberAlreadyExists_ReturnsBadRequest() {
		// Arrange
		var clientCreate = new ClientCreate("business-id", "John Doe", "789.456.123-90", null, "1234567890", "client@gmail.com");
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");

		_mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Client>())).ReturnsAsync(IdentityResult.Success);

		_context.Business.Add(business);
		_context.Clients.Add((Client)clientCreate);
		_context.SaveChanges();

		// Act
		var result = await _controller.CreateClient(clientCreate);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.PhoneNumber, badRequestResult.Value);
	}

	[Fact]
	public async Task CreateClient_EmailAlreadyExists_ReturnsBadRequest() {
		// Arrange
		var clientCreate = new ClientCreate("business-id", "John Doe", "789.456.123-90", null, "1234567890", "client@gmail.com");
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");

		_mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Client>())).ReturnsAsync(IdentityResult.Success);

		_context.Business.Add(business);
		_context.Clients.Add((Client)clientCreate);
		_context.SaveChanges();

		clientCreate.PhoneNumber = "98998765432";
		clientCreate.CPF = "123.456.789-00";

		// Act
		var result = await _controller.CreateClient(clientCreate);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.Email, badRequestResult.Value);
	}

	[Fact]
	public async Task CreateClient_CPFAlreadyExists_ReturnsBadRequest() {
		// Arrange
		var clientCreate = new ClientCreate("business-id", "John Doe", "789.456.123-90", null, "1234567890", "client@gmail.com");
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");

		_mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Client>())).ReturnsAsync(IdentityResult.Success);

		_context.Business.Add(business);
		_context.Clients.Add((Client)clientCreate);
		_context.SaveChanges();

		clientCreate.PhoneNumber = "98998765432";
		clientCreate.Email = "lenda@outlook.com";

		// Act
		var result = await _controller.CreateClient(clientCreate);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.CPF, badRequestResult.Value);
	}

	[Fact]
	public async Task UpdateClient_ValidClient_ReturnsOk() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");
		business.Id = Guid.NewGuid().ToString();

		var client = new Client(business.Id, "98988263255", "fulano da silva bezerra", "fulano@gmail.com", "123.456.789-00", null);
		client.Id = Guid.NewGuid().ToString();

		_mockUserManager.Setup(x => x.FindByIdAsync(client.Id)).ReturnsAsync(client);
		_mockUserManager.Setup(x => x.SetPhoneNumberAsync(client, It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
		_mockUserManager.Setup(x => x.SetEmailAsync(client, It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

		_context.Business.Add(business);
		_context.Clients.Add(client);
		_context.SaveChanges();

		var updatedClient = new ClientDTO(client.Id, "matheus lacerda bezerra", "98999344788", "lenda@hotmail.com", "A solid 4", "987.654.321-00");

		// Act
		var result = await _controller.UpdateClient(updatedClient);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var clientDto = Assert.IsType<ClientDTO>(okResult.Value);
		Assert.Equal(updatedClient.FullName, clientDto.FullName);
		Assert.Equal(updatedClient.CPF, clientDto.CPF);
		Assert.Equal(updatedClient.AdditionalNote, clientDto.AdditionalNote);
		Assert.Equal(updatedClient.PhoneNumber, clientDto.PhoneNumber);
		Assert.Equal(updatedClient.Email, clientDto.Email);
	}

	[Fact]
	public async Task UpdateClient_ClientDoesNotExist_ReturnsBadRequest() {
		// Arrange
		var updatedClient = new ClientDTO("non-existing-id", "fulano da silva bezerra", "98988263256",
											null, null, null);

		_mockUserManager.Setup(x => x.FindByIdAsync(updatedClient.Id)).ReturnsAsync((Client)null!);

		// Act
		var result = await _controller.UpdateClient(updatedClient);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(NotExistErrors.Client, badRequestResult.Value);
	}

	[Fact]
	public async Task UpdateClient_CPFAlreadyExists_ReturnsBadRequest() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");
		business.Id = Guid.NewGuid().ToString();

		var client1 = new Client(business.Id, "98988263255", "fulano da silva bezerra", "fulano@gmail.com", "123.456.789-00", null);
		client1.Id = Guid.NewGuid().ToString();

		var client2 = new Client(business.Id, "98988263256", "ciclano da silva", "ciclano@gmail.com", "123.456.789-01", null);
		client2.Id = Guid.NewGuid().ToString();

		_mockUserManager.Setup(x => x.FindByIdAsync(client1.Id)).ReturnsAsync(client1);

		_context.Business.Add(business);
		_context.Clients.AddRange(client1, client2);
		_context.SaveChanges();

		var updatedClient = (ClientDTO)client1;
		updatedClient.CPF = client2.CPF;

		// Act
		var result = await _controller.UpdateClient(updatedClient);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.CPF, badRequestResult.Value);
	}

	[Fact]
	public async Task UpdateClient_PhoneNumberAlreadyExists_ReturnsBadRequest() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");
		business.Id = Guid.NewGuid().ToString();

		var client1 = new Client(business.Id, "98988263255", "fulano da silva bezerra", "123.456.789-00", "fulano@gmail.com", null);
		client1.Id = Guid.NewGuid().ToString();

		var client2 = new Client(business.Id, "98988263256", "ciclano da silva", "123.456.789-01", "ciclano@gmail.com", null);
		client2.Id = Guid.NewGuid().ToString();

		_mockUserManager.Setup(x => x.FindByIdAsync(client1.Id)).ReturnsAsync(client1);

		_context.Business.Add(business);
		_context.Clients.AddRange(client1, client2);
		_context.SaveChanges();

		var updatedClient = (ClientDTO)client1;
		updatedClient.PhoneNumber = client2.PhoneNumber!;

		// Act
		var result = await _controller.UpdateClient(updatedClient);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.PhoneNumber, badRequestResult.Value);
	}

	[Fact]
	public async Task UpdateClient_EmailAlreadyExists_ReturnsBadRequest() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");
		business.Id = Guid.NewGuid().ToString();

		var client1 = new Client(business.Id, "98988263255", "fulano da silva bezerra", "fulano@gmail.com", "123.456.789-00", null);
		client1.Id = Guid.NewGuid().ToString();

		var client2 = new Client(business.Id, "98988263256", "ciclano da silva", "ciclano@gmail.com", "123.456.789-01", null);
		client2.Id = Guid.NewGuid().ToString();

		_mockUserManager.Setup(x => x.FindByIdAsync(client1.Id)).ReturnsAsync(client1);

		_context.Business.Add(business);
		_context.Clients.AddRange(client1, client2);
		_context.SaveChanges();

		var updatedClient = (ClientDTO)client1;
		updatedClient.Email = client2.Email;

		// Act
		var result = await _controller.UpdateClient(updatedClient);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.Email, badRequestResult.Value);
	}
}