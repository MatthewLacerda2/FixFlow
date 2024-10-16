using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Controllers;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Erros;

namespace FixFlow.Tests.Controllers;

public class CustomerControllerTests {

	private readonly ServerContext _context;
	private readonly CustomerController _controller;
	private readonly Mock<UserManager<Customer>> _mockUserManager;

	public CustomerControllerTests() {

		_context = new Util().SetupDbContextForTests();

		var store = new Mock<IUserStore<Customer>>();
		_mockUserManager = new Mock<UserManager<Customer>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

		_controller = new CustomerController(_context, _mockUserManager.Object);
	}

	[Fact]
	public async Task GetCustomerRecord_CustomerExists_ReturnsOk() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");
		business.Id = Guid.NewGuid().ToString();

		var customer = new Customer(business.Id, "98988263255", "fulano da silva bezerra", null, null, null);
		customer.Id = Guid.NewGuid().ToString();

		_mockUserManager.Setup(x => x.FindByIdAsync(customer.Id)).ReturnsAsync(customer);

		_context.Business.Add(business);
		_context.Customers.Add(customer);

		_context.SaveChanges();

		// Act
		var result = await _controller.GetCustomerRecord(customer.Id);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var customerRecord = Assert.IsType<CustomerRecord>(okResult.Value);
	}

	[Fact]
	public async Task GetCustomerRecord_CustomerDoesNotExist_ReturnsBadRequest() {
		// Arrange
		var customerId = "non-existent-customer-id";
		_mockUserManager.Setup(x => x.FindByIdAsync(customerId)).ReturnsAsync((Customer)null!);

		// Act
		var result = await _controller.GetCustomerRecord(customerId);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(NotExistErrors.Customer, badRequestResult.Value);
	}

	[Fact]
	public async Task ReadCustomers_ReturnsFilteredCustomers() {
		// Arrange
		var business1 = new Business("Business1", "business1@gmail.com", "123.4567.789-0001", "98999344788");
		var business2 = new Business("Business2", "business2@gmail.com", "123.4567.789-0002", "98999344789");
		business1.Id = Guid.NewGuid().ToString();
		business2.Id = Guid.NewGuid().ToString();

		_context.Business.AddRange(business1, business2);

		Customer[] customers = new Customer[6];

		for (int i = 0; i < 6; i++) {
			var customer = new Customer(business1.Id, $"9899934478{i}", "matthew de la cerda", null, null, null);

			customer.Id = Guid.NewGuid().ToString();
			customers[i] = customer;
		}

		customers[0].BusinessId = business2.Id;
		customers[1].FullName = "matheus lacerda";

		_context.Customers.AddRange(customers);
		await _context.SaveChangesAsync();

		// Act
		var result = await _controller.ReadCustomers(business1.Id, 1, 1, "matthew");

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var customersArray = Assert.IsType<CustomerDTO[]>(okResult.Value);
		Assert.Single(customersArray);
		Assert.Equal(customers[3].PhoneNumber, customersArray[0].PhoneNumber);
	}

	[Fact]
	public async Task CreateCustomer_ValidCustomer_ReturnsCreated() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");
		var customerCreate = new CustomerCreate(business.Id, "John Doe", "789.456.123-90", null, "1234567890", "customer@gmail.com");

		_mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(IdentityResult.Success);

		_context.Business.Add(business);
		_context.SaveChanges();

		// Act
		var result = await _controller.CreateCustomer(customerCreate);

		// Assert
		var createdResult = Assert.IsType<CreatedAtActionResult>(result);
		var customerDto = Assert.IsType<Customer>(createdResult.Value);
		Assert.Equal(customerCreate.PhoneNumber, customerDto.PhoneNumber);
		Assert.Equal(customerCreate.FullName, customerDto.FullName);
		Assert.Equal(customerCreate.Email, customerDto.Email);
		Assert.Equal(customerCreate.CPF, customerDto.CPF);
	}

	[Fact]
	public async Task CreateCustomer_PhoneNumberAlreadyExists_ReturnsBadRequest() {
		// Arrange
		var customerCreate = new CustomerCreate("business-id", "John Doe", "789.456.123-90", null, "1234567890", "customer@gmail.com");
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");

		_mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(IdentityResult.Success);

		_context.Business.Add(business);
		_context.Customers.Add((Customer)customerCreate);
		_context.SaveChanges();

		// Act
		var result = await _controller.CreateCustomer(customerCreate);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.PhoneNumber, badRequestResult.Value);
	}

	[Fact]
	public async Task CreateCustomer_EmailAlreadyExists_ReturnsBadRequest() {
		// Arrange
		var customerCreate = new CustomerCreate("business-id", "John Doe", "789.456.123-90", null, "1234567890", "customer@gmail.com");
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");

		_mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(IdentityResult.Success);

		_context.Business.Add(business);
		_context.Customers.Add((Customer)customerCreate);
		_context.SaveChanges();

		customerCreate.PhoneNumber = "98998765432";
		customerCreate.CPF = "123.456.789-00";

		// Act
		var result = await _controller.CreateCustomer(customerCreate);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.Email, badRequestResult.Value);
	}

	[Fact]
	public async Task CreateCustomer_CPFAlreadyExists_ReturnsBadRequest() {
		// Arrange
		var customerCreate = new CustomerCreate("business-id", "John Doe", "789.456.123-90", null, "1234567890", "customer@gmail.com");
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");

		_mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(IdentityResult.Success);

		_context.Business.Add(business);
		_context.Customers.Add((Customer)customerCreate);
		_context.SaveChanges();

		customerCreate.PhoneNumber = "98998765432";
		customerCreate.Email = "lenda@outlook.com";

		// Act
		var result = await _controller.CreateCustomer(customerCreate);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.CPF, badRequestResult.Value);
	}

	[Fact]
	public async Task UpdateCustomer_ValidCustomer_ReturnsOk() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");
		business.Id = Guid.NewGuid().ToString();

		var customer = new Customer(business.Id, "98988263255", "fulano da silva bezerra", "fulano@gmail.com", "123.456.789-00", null);
		customer.Id = Guid.NewGuid().ToString();

		_mockUserManager.Setup(x => x.FindByIdAsync(customer.Id)).ReturnsAsync(customer);
		_mockUserManager.Setup(x => x.SetPhoneNumberAsync(customer, It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
		_mockUserManager.Setup(x => x.SetEmailAsync(customer, It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

		_context.Business.Add(business);
		_context.Customers.Add(customer);
		_context.SaveChanges();

		var updatedCustomer = new CustomerDTO(customer.Id, "matheus lacerda bezerra", "98999344788", "lenda@hotmail.com", "A solid 4", "987.654.321-00");

		// Act
		var result = await _controller.UpdateCustomer(updatedCustomer);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var customerDto = Assert.IsType<CustomerDTO>(okResult.Value);
		Assert.Equal(updatedCustomer.FullName, customerDto.FullName);
		Assert.Equal(updatedCustomer.CPF, customerDto.CPF);
		Assert.Equal(updatedCustomer.AdditionalNote, customerDto.AdditionalNote);
		Assert.Equal(updatedCustomer.PhoneNumber, customerDto.PhoneNumber);
		Assert.Equal(updatedCustomer.Email, customerDto.Email);
	}

	[Fact]
	public async Task UpdateCustomer_CustomerDoesNotExist_ReturnsBadRequest() {
		// Arrange
		var updatedCustomer = new CustomerDTO("non-existing-id", "fulano da silva bezerra", "98988263256",
											null, null, null);

		_mockUserManager.Setup(x => x.FindByIdAsync(updatedCustomer.Id)).ReturnsAsync((Customer)null!);

		// Act
		var result = await _controller.UpdateCustomer(updatedCustomer);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(NotExistErrors.Customer, badRequestResult.Value);
	}

	[Fact]
	public async Task UpdateCustomer_CPFAlreadyExists_ReturnsBadRequest() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");
		business.Id = Guid.NewGuid().ToString();

		var customer1 = new Customer(business.Id, "98988263255", "fulano da silva bezerra", "fulano@gmail.com", "123.456.789-00", null);
		customer1.Id = Guid.NewGuid().ToString();

		var customer2 = new Customer(business.Id, "98988263256", "ciclano da silva", "ciclano@gmail.com", "123.456.789-01", null);
		customer2.Id = Guid.NewGuid().ToString();

		_mockUserManager.Setup(x => x.FindByIdAsync(customer1.Id)).ReturnsAsync(customer1);

		_context.Business.Add(business);
		_context.Customers.AddRange(customer1, customer2);
		_context.SaveChanges();

		var updatedCustomer = (CustomerDTO)customer1;
		updatedCustomer.CPF = customer2.CPF;

		// Act
		var result = await _controller.UpdateCustomer(updatedCustomer);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.CPF, badRequestResult.Value);
	}

	[Fact]
	public async Task UpdateCustomer_PhoneNumberAlreadyExists_ReturnsBadRequest() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");
		business.Id = Guid.NewGuid().ToString();

		var customer1 = new Customer(business.Id, "98988263255", "fulano da silva bezerra", "123.456.789-00", "fulano@gmail.com", null);
		customer1.Id = Guid.NewGuid().ToString();

		var customer2 = new Customer(business.Id, "98988263256", "ciclano da silva", "123.456.789-01", "ciclano@gmail.com", null);
		customer2.Id = Guid.NewGuid().ToString();

		_mockUserManager.Setup(x => x.FindByIdAsync(customer1.Id)).ReturnsAsync(customer1);

		_context.Business.Add(business);
		_context.Customers.AddRange(customer1, customer2);
		_context.SaveChanges();

		var updatedCustomer = (CustomerDTO)customer1;
		updatedCustomer.PhoneNumber = customer2.PhoneNumber!;

		// Act
		var result = await _controller.UpdateCustomer(updatedCustomer);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.PhoneNumber, badRequestResult.Value);
	}

	[Fact]
	public async Task UpdateCustomer_EmailAlreadyExists_ReturnsBadRequest() {
		// Arrange
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255");
		business.Id = Guid.NewGuid().ToString();

		var customer1 = new Customer(business.Id, "98988263255", "fulano da silva bezerra", "fulano@gmail.com", "123.456.789-00", null);
		customer1.Id = Guid.NewGuid().ToString();

		var customer2 = new Customer(business.Id, "98988263256", "ciclano da silva", "ciclano@gmail.com", "123.456.789-01", null);
		customer2.Id = Guid.NewGuid().ToString();

		_mockUserManager.Setup(x => x.FindByIdAsync(customer1.Id)).ReturnsAsync(customer1);

		_context.Business.Add(business);
		_context.Customers.AddRange(customer1, customer2);
		_context.SaveChanges();

		var updatedCustomer = (CustomerDTO)customer1;
		updatedCustomer.Email = customer2.Email;

		// Act
		var result = await _controller.UpdateCustomer(updatedCustomer);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.Email, badRequestResult.Value);
	}
}
