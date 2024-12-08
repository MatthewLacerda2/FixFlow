using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Data;
using Server.Models;
using Server.Models.Utils;
using Server.Models.Erros;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

[ApiController]
[Route(Common.api_v1 + "accounts")]
[Produces("application/json")]
public class AccountsController : ControllerBase {

	private readonly SignInManager<Business> _signInManager;
	private readonly UserManager<Business> _userManager;
	private readonly IConfiguration _configuration;
	private readonly ServerContext _context;

	public AccountsController(SignInManager<Business> signInManager, UserManager<Business> userManager,
								IConfiguration configuration, ServerContext context) {
		_signInManager = signInManager;
		_userManager = userManager;
		_configuration = configuration;
		_context = context;
	}

	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
	[HttpGet]
	public async Task<IActionResult> Debug() {
		string mensagem = "Olá mundo, Lendacerda mandou esta mensagem\n";
		int numeros = await _userManager.Users.CountAsync();
		string usuarios = "Atualmente nós temos " + numeros + " empresas cadastradas.\n";
		return Ok(mensagem + usuarios);
	}

	/// <summary>
	/// Login with an email and password
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[HttpPost]
	public async Task<IActionResult> Login([FromBody] FlowLoginRequest model) {

		var userExists = await _userManager.FindByEmailAsync(model.email);
		if (userExists == null) {
			return Unauthorized(ValidatorErrors.WrongUsernameOrPassword);
		}

		var result = await _signInManager.PasswordSignInAsync(userExists, model.password, true, false);
		if (!result.Succeeded) {
			return Unauthorized(ValidatorErrors.WrongUsernameOrPassword);
		}

		var token = GenerateToken(userExists);

		userExists.IsActive = true;
		await _context.SaveChangesAsync();

		return Ok(token);
	}

	private string GenerateToken(Business business) {

		var expirationDate = DateTime.UtcNow.AddMonths(3);

		var claims = new List<Claim>{
			new Claim(ClaimTypes.Name, business.Name),
			new Claim(ClaimTypes.Email, business.Email!),
			new Claim("ExpirationDate", expirationDate.ToString()),
			new Claim("businessId", business.Id)
		};
		var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!);
		var tokenDescriptor = new SecurityTokenDescriptor {
			Issuer = _configuration["Jwt:Issuer"],
			Audience = _configuration["Jwt:Audience"],
			Expires = expirationDate,
			Subject = new ClaimsIdentity(claims),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(token);
	}


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
