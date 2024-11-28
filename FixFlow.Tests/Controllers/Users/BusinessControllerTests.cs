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

public class BusinessControllerTests {

	private readonly ServerContext _context;
	private readonly BusinessController _controller;
	private readonly Mock<UserManager<Business>> _mockUserManager;

	public BusinessControllerTests() {

		_context = new Util().SetupDbContextForTests();

		var store = new Mock<IUserStore<Business>>();
		_mockUserManager = new Mock<UserManager<Business>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

		_controller = new BusinessController(_context, _mockUserManager.Object);
	}

	[Fact]
	public async Task GetBusiness_ReturnsBadRequest_WhenBusinessDoesNotExist() {
		// Arrange
		var businessId = "non-existent-business-id";
		_mockUserManager.Setup(x => x.FindByIdAsync(businessId)).ReturnsAsync((Business)null!);

		// Act
		var result = await _controller.GetBusiness(businessId);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(NotExistErrors.Business, badRequestResult.Value);
	}

	[Fact]
	public async Task GetBusiness_ReturnsOkResult_WhenBusinessExists() {
		// Arrange
		var businessId = "test-business-id";
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98999344788");
		_mockUserManager.Setup(x => x.FindByIdAsync(businessId)).ReturnsAsync(business);

		// Act
		var result = await _controller.GetBusiness(businessId);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var okResultValue = Assert.IsType<BusinessDTO>(okResult.Value);
		Assert.Equal(business.Id, okResultValue.Id);
		Assert.Equal(business.Name, okResultValue.Name);
		Assert.Equal(business.Email, okResultValue.Email);
		Assert.Equal(business.CNPJ, okResultValue.CNPJ);
		Assert.Equal(business.PhoneNumber, okResultValue.PhoneNumber);
		Assert.Equal(business.BusinessWeek, okResultValue.BusinessWeek);
		Assert.Equal(business.services, okResultValue.Services);
		Assert.Equal(business.allowListedServicesOnly, okResultValue.AllowListedServicesOnly);
		Assert.Equal(business.openOnHolidays, okResultValue.OpenOnHolidays);
	}

	[Fact]
	public async Task CreateBusiness_ReturnsBadRequest_WhenEmailAlreadyExists() {
		// Arrange
		var phoneNumber = "98999344788";
		var password = "@Password123!";
		var businessRegister = new BusinessRegisterRequest("lenda", "lenda@gmail.com", "123.4567.789-0001", phoneNumber, password, password);

		_mockUserManager.Setup(x => x.FindByEmailAsync(businessRegister.Email)).ReturnsAsync((Business)businessRegister);

		_context.SaveChanges();

		// Act
		var result = await _controller.CreateBusiness(businessRegister);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.Email, badRequestResult.Value);
	}

	[Fact]
	public async Task CreateBusiness_ReturnsBadRequest_WhenPhoneNumberAlreadyExists() {
		// Arrange
		var phoneNumber = "98999344788";
		var password = "@Password123!";
		var businessRegister = new BusinessRegisterRequest("lenda", "lenda@gmail.com", "123.4567.789-0001", phoneNumber, password, password);

		_mockUserManager.Setup(x => x.FindByEmailAsync(businessRegister.Email)).ReturnsAsync((Business)businessRegister);

		_context.Business.Add((Business)businessRegister);
		_context.SaveChanges();

		businessRegister.Email = "bezerra@hotmail.com";

		// Act
		var result = await _controller.CreateBusiness(businessRegister);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.PhoneNumber, badRequestResult.Value);
	}

	[Fact]
	public async Task CreateBusiness_ReturnsBadRequest_WhenCNPJAlreadyExists() {
		// Arrange
		var password = "@Password123!";
		var businessRegister = new BusinessRegisterRequest("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255", password, password);

		_mockUserManager.Setup(x => x.FindByEmailAsync(businessRegister.Email)).ReturnsAsync((Business)businessRegister);

		_context.Business.Add((Business)businessRegister);
		_context.SaveChanges();

		businessRegister.Email = "outroemail@gmail.com";
		businessRegister.PhoneNumber = "981199344788";

		// Act
		var result = await _controller.CreateBusiness(businessRegister);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(AlreadyRegisteredErrors.CNPJ, badRequestResult.Value);
	}

	[Fact]
	public async Task CreateBusiness_ReturnsCreatedAtAction_WhenBusinessIsCreatedSuccessfully() {
		// Arrange
		var phoneNumber = "98999344788";
		var password = "@Password123!";
		var businessRegister = new BusinessRegisterRequest("lenda", "lenda@gmail.com", "123.4567.789-0001", phoneNumber, password, password);

		_mockUserManager.Setup(x => x.FindByEmailAsync(businessRegister.Email)).ReturnsAsync((Business)null!);
		_mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Business>(), businessRegister.ConfirmPassword)).ReturnsAsync(IdentityResult.Success);

		_context.SaveChanges();

		// Act
		var result = await _controller.CreateBusiness(businessRegister);

		// Assert
		var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
		Assert.Equal(nameof(_controller.CreateBusiness), createdAtActionResult.ActionName);
		var createdBusiness = Assert.IsType<Business>(createdAtActionResult.Value);

		Assert.Equal(createdBusiness.Name, businessRegister.Name);
		Assert.Equal(createdBusiness.Email, businessRegister.Email);
		Assert.Equal(createdBusiness.CNPJ, businessRegister.CNPJ);
		Assert.Equal(createdBusiness.PhoneNumber, businessRegister.PhoneNumber);
	}

	[Fact]
	public async Task UpdateBusiness_ReturnsOkResult_WhenBusinessExists() {
		// Arrange
		var businessId = "test-business-id";
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98999344788") { Id = businessId };
		var upBusiness = new Business("updatedName", "updated@gmail.com", "123.4567.789-0001", "98999344788") { Id = businessId };
		var upBusinessDTO = new BusinessDTO(upBusiness);

		_mockUserManager.Setup(x => x.FindByIdAsync(businessId)).ReturnsAsync(business);

		// Act
		var result = await _controller.UpdateBusiness(upBusinessDTO);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var okResultValue = Assert.IsType<BusinessDTO>(okResult.Value);

		Assert.Equal(business.BusinessWeek, upBusiness.BusinessWeek);
		Assert.Equal(business.services, upBusiness.services);
		Assert.Equal(business.allowListedServicesOnly, upBusiness.allowListedServicesOnly);
		Assert.Equal(business.openOnHolidays, upBusiness.openOnHolidays);
	}

	[Fact]
	public async Task UpdateBusiness_ReturnsBadRequest_WhenBusinessDoesNotExist() {
		// Arrange
		var businessId = "non-existent-business-id";
		var updatedBusiness = new Business("updatedName", "updated@gmail.com", "123.4567.789-0001", "98999344788") { Id = businessId };

		// Act
		var result = await _controller.UpdateBusiness(new BusinessDTO(updatedBusiness));

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(NotExistErrors.Business, badRequestResult.Value);
	}

	[Fact]
	public async Task DeactivateBusiness_ReturnsOkResult_WhenBusinessExists() {
		// Arrange
		var businessId = "test-business-id";
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98999344788") { Id = businessId, IsActive = true };
		_mockUserManager.Setup(x => x.FindByIdAsync(businessId)).ReturnsAsync(business);

		// Act
		var result = await _controller.DeactivateBusiness(businessId);

		// Assert
		var okResult = Assert.IsType<OkResult>(result);
		Assert.False(business.IsActive);
	}

	[Fact]
	public async Task DeactivateBusiness_ReturnsBadRequest_WhenBusinessDoesNotExist() {
		// Arrange
		var businessId = "non-existent-business-id";

		// Act
		var result = await _controller.DeactivateBusiness(businessId);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(NotExistErrors.Business, badRequestResult.Value);
	}

	[Fact]
	public async Task DeleteBusiness_ReturnsOkResult_WhenBusinessExists() {
		// Arrange
		var businessId = "test-business-id";
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98999344788") { Id = businessId };
		_mockUserManager.Setup(x => x.FindByIdAsync(businessId)).ReturnsAsync(business);
		_mockUserManager.Setup(x => x.DeleteAsync(business)).ReturnsAsync(IdentityResult.Success);

		Customer client = new Customer(businessId, "98912345678", "fulano da silva bezerra", null, null, null);
		AptSchedule schedule = new AptSchedule(client.Id, businessId, DateTime.UtcNow.AddDays(-1), 100f, null);
		CreateAptLog createAptLog = new CreateAptLog(client.Id, schedule.Id, DateTime.UtcNow, 100f, null, null, DateTime.UtcNow.AddDays(90));
		AptLog log = new AptLog(createAptLog);
		AptContact contact = new AptContact(log, DateTime.UtcNow);

		_context.Business.Add(business);
		_context.Customers.Add(client);
		_context.Schedules.Add(schedule);
		_context.Logs.Add(log);
		_context.Contacts.Add(contact);
		_context.SaveChanges();

		// Act
		var result = await _controller.DeleteBusiness(businessId);

		// Assert
		var okResult = Assert.IsType<NoContentResult>(result);

		Assert.Empty(_context.Customers.Where(x => x.BusinessId == businessId));
		Assert.Empty(_context.Contacts.Where(x => x.businessId == businessId));
		Assert.Empty(_context.Schedules.Where(x => x.BusinessId == businessId));
		Assert.Empty(_context.Logs.Where(x => x.BusinessId == businessId));
	}

	[Fact]
	public async Task DeleteBusiness_ReturnsBadRequest_WhenBusinessDoesNotExist() {
		// Arrange
		var businessId = "non-existent-business-id";
		_mockUserManager.Setup(x => x.FindByIdAsync(businessId)).ReturnsAsync((Business)null!);

		// Act
		var result = await _controller.DeleteBusiness(businessId);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(NotExistErrors.Business, badRequestResult.Value);
	}
}
