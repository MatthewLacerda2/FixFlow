using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Server.Data;
using Server.Models;
using Server.Models.Utils;
using Server.Services;

namespace Server.Controllers;

[ApiController]
[Route("api/v1/accounts")]
public class AccountsController : ControllerBase {

	private readonly SignInManager<Business> _signInManager;
	private readonly UserManager<Business> _userManager;
	private readonly IConfiguration _configuration;
	private readonly ServerContext _context;
	private readonly MailResetPassword _emailResetPasswordService;

	const string wrongCredentialsMessage = "Wrong UserName/Email or Password";

	public AccountsController(SignInManager<Business> signInManager, UserManager<Business> userManager,
								IConfiguration configuration, ServerContext context, MailResetPassword emailResetPasswordService) {
		_signInManager = signInManager;
		_userManager = userManager;
		_configuration = configuration;
		_context = context;
		_emailResetPasswordService = emailResetPasswordService;
	}

	/// <summary>
	/// Login with an email and password
	/// </summary>
	/// <returns>Business</returns>
	/// <response code="200">Successfull login</response>
	/// <response code="401">Unauthorized login</response>
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[HttpPost]
	public async Task<IActionResult> Login([FromBody] FlowLoginRequest model) {

		var userExists = _userManager.FindByEmailAsync(model.email).Result;
		if (userExists == null) {
			return Unauthorized(wrongCredentialsMessage);
		}

		var result = await _signInManager.PasswordSignInAsync(userExists, model.password, true, false);
		if (!result.Succeeded) {
			return Unauthorized(wrongCredentialsMessage);
		}

		var token = GenerateToken(userExists);

		userExists.LastLogin = DateTime.Now;
		userExists.isActive = true;
		await _context.SaveChangesAsync();

		userExists.token = token;

		return Ok(userExists);
	}

	private string GenerateToken(Business business) {

		var businessJson = JsonConvert.SerializeObject(business);

		var claims = new List<Claim>{
			new Claim("Business", businessJson)
		};
		var key = Encoding.UTF32.GetBytes(_configuration["Jwt:SecretKey"]!);
		var tokenDescriptor = new SecurityTokenDescriptor {
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow.AddMinutes(Common.tokenExpirationTimeInMinutes),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(token);
	}

	/// <summary>
	/// Logout method
	/// </summary>
	/// <returns>string</returns>
	/// <response code="200">Logout successfull</response>
	/// <response code="500">Logout unsucessfull. Probable Internal Server Error</response>
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[Authorize]
	[HttpPost("logout")]
	public async Task<IActionResult> Logout() {
		try {
			await _signInManager.SignOutAsync();
			return Ok("Logged out successfully");
		}
		catch (Exception ex) {
			Console.WriteLine("\nErro Message: " + ex.Message + "\nErro Data: " + ex.Data);
			return StatusCode(StatusCodes.Status500InternalServerError, "Error logging out");
		}
	}
}
