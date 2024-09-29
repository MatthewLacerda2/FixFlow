using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Controllers;
using Server.Data;
using Server.Models;
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
	public async Task GetBusiness_ReturnsOkResult_WhenBusinessExists() {
		// Arrange
		var businessId = "test-business-id";
		var business = new Business("lenda", "lenda@gmail.com", "123.4567.789-0001", "98999344788");
		_mockUserManager.Setup(x => x.FindByIdAsync(businessId)).ReturnsAsync(business);

		// Act
		var result = await _controller.GetBusiness(businessId);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		Assert.Equal(business, okResult.Value);
	}

	[Fact]
	public async Task CreateBusiness_ReturnsBadRequest_WhenOTPIsInvalid() {
		// Arrange
		var phoneNumber = "98999344788";
		var password = "@Password123!";
		var businessRegister = new BusinessRegisterRequest("lenda", "lenda@gmail.com", "123.4567.789-0001", phoneNumber, password, password, "XXXXXX");

		_context.OTPs.Add(new OTP(phoneNumber));
		_context.SaveChanges();

		// Act
		var result = await _controller.CreateBusiness(businessRegister);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(ValidatorErrors.InvalidOTP, badRequestResult.Value);
	}

	[Fact]
	public async Task CreateBusiness_ReturnsBadRequest_WhenEmailAlreadyExists() {
		// Arrange
		var phoneNumber = "98999344788";
		var password = "@Password123!";
		OTP otp = new OTP(phoneNumber);
		var businessRegister = new BusinessRegisterRequest("lenda", "lenda@gmail.com", "123.4567.789-0001", phoneNumber, password, password, otp.Code);

		_mockUserManager.Setup(x => x.FindByEmailAsync(businessRegister.Email)).ReturnsAsync((Business)businessRegister);

		_context.OTPs.Add(otp);
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
		OTP otp = new OTP(phoneNumber);
		var businessRegister = new BusinessRegisterRequest("lenda", "lenda@gmail.com", "123.4567.789-0001", phoneNumber, password, password, otp.Code);

		_mockUserManager.Setup(x => x.FindByEmailAsync(businessRegister.Email)).ReturnsAsync((Business)businessRegister);

		_context.Business.Add((Business)businessRegister);
		_context.OTPs.Add(otp);
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
		var phoneNumber = "98999344788";
		var password = "@Password123!";
		OTP otp = new OTP(phoneNumber);
		var businessRegister = new BusinessRegisterRequest("lenda", "lenda@gmail.com", "123.4567.789-0001", "98988263255", password, password, otp.Code);

		_mockUserManager.Setup(x => x.FindByEmailAsync(businessRegister.Email)).ReturnsAsync((Business)businessRegister);

		_context.Business.Add((Business)businessRegister);
		_context.OTPs.Add(otp);
		_context.SaveChanges();

		businessRegister.Email = "bezerra@hotmail.com";
		businessRegister.PhoneNumber = otp.PhoneNumber;

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
		OTP otp = new OTP(phoneNumber);
		var businessRegister = new BusinessRegisterRequest("lenda", "lenda@gmail.com", "123.4567.789-0001", phoneNumber, password, password, otp.Code);

		_mockUserManager.Setup(x => x.FindByEmailAsync(businessRegister.Email)).ReturnsAsync((Business)null!);
		_mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Business>(), businessRegister.ConfirmPassword)).ReturnsAsync(IdentityResult.Success);

		_context.OTPs.Add(otp);
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
		var updatedBusiness = new Business("updatedName", "updated@gmail.com", "123.4567.789-0001", "98999344788") { Id = businessId };

		_mockUserManager.Setup(x => x.FindByIdAsync(businessId)).ReturnsAsync(business);

		// Act
		var result = await _controller.UpdateBusiness(updatedBusiness);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		Assert.Equal(updatedBusiness, okResult.Value);

		Assert.Equal(business.Name, updatedBusiness.Name);
		Assert.Equal(business.Email, updatedBusiness.Email);
		Assert.Equal(business.BusinessDays, updatedBusiness.BusinessDays);
		Assert.Equal(business.allowListedServicesOnly, updatedBusiness.allowListedServicesOnly);
		Assert.Equal(business.openOnHolidays, updatedBusiness.openOnHolidays);
	}

	[Fact]
	public async Task UpdateBusiness_ReturnsBadRequest_WhenBusinessDoesNotExist() {
		// Arrange
		var businessId = "non-existent-business-id";
		var updatedBusiness = new Business("updatedName", "updated@gmail.com", "123.4567.789-0001", "98999344788") { Id = businessId };

		_mockUserManager.Setup(x => x.FindByIdAsync(businessId)).ReturnsAsync((Business)null!);

		// Act
		var result = await _controller.UpdateBusiness(updatedBusiness);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal(NotExistErrors.Business, badRequestResult.Value);
	}
}
