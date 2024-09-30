using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Server.Data;
using Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FixFlow.Tests.Controllers;

public class AccountsControllerTests {

	private readonly Mock<UserManager<Business>> _mockUserManager;
	private readonly Mock<SignInManager<Business>> _mockSignInManager;
	private readonly Mock<IConfiguration> _mockConfiguration;
	private readonly ServerContext _context;
	private readonly AccountsController _controller;

	public AccountsControllerTests() {
		var store = new Mock<IUserStore<Business>>();
		_mockUserManager = new Mock<UserManager<Business>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
		_mockSignInManager = new Mock<SignInManager<Business>>(
			_mockUserManager.Object,
			new Mock<IHttpContextAccessor>().Object,
			new Mock<IUserClaimsPrincipalFactory<Business>>().Object,
			null!, null!, null!, null!
		);

		_mockConfiguration = new Mock<IConfiguration>();

		var options = new DbContextOptionsBuilder<ServerContext>()
			.UseSqlite("TestDatabase")
			.Options;

		_context = new ServerContext(options);

		_controller = new AccountsController(_mockSignInManager.Object, _mockUserManager.Object, _mockConfiguration.Object, _context);
	}

	[Fact]
	public async Task Login_ReturnsUnauthorized_WhenUserDoesNotExist() {
		// Arrange
		var loginRequest = new FlowLoginRequest { email = "nonexistent@example.com", password = "password123" };
		_mockUserManager.Setup(x => x.FindByEmailAsync(loginRequest.email)).ReturnsAsync((Business)null!);
		// Act
		var result = await _controller.Login(loginRequest);
		// Assert
		var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
		Assert.Equal("Wrong UserName/Email or Password", unauthorizedResult.Value);
	}


	[Fact]
	public async Task Login_ReturnsUnauthorized_WhenPasswordIsIncorrect() {
		// Arrange
		var business = new Business { Email = "test@example.com", Name = "Test Business" };
		var loginRequest = new FlowLoginRequest { email = business.Email, password = "wrongpassword" };
		_mockUserManager.Setup(x => x.FindByEmailAsync(loginRequest.email)).ReturnsAsync(business);
		_mockSignInManager.Setup(x => x.PasswordSignInAsync(business, loginRequest.password, true, false))
			.ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

		// Act
		var result = await _controller.Login(loginRequest);

		// Assert
		var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
		Assert.Equal("Wrong UserName/Email or Password", unauthorizedResult.Value);
	}

	[Fact]
	public async Task Login_ReturnsOk_WithToken_WhenLoginIsSuccessful() {
		// Arrange
		var business = new Business { Email = "test@example.com", Name = "Test Business", Id = "1" };
		var loginRequest = new FlowLoginRequest { email = business.Email, password = "password123" };
		_mockUserManager.Setup(x => x.FindByEmailAsync(loginRequest.email)).ReturnsAsync(business);
		_mockSignInManager.Setup(x => x.PasswordSignInAsync(business, loginRequest.password, true, false))
			.ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
		_mockConfiguration.Setup(x => x["Jwt:SecretKey"]).Returns("VerySecretKey");

		// Act
		var result = await _controller.Login(loginRequest);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		Assert.NotNull(okResult.Value); // Assuming GenerateToken() returns a string token.
		Assert.IsType<string>(okResult.Value);
	}

}