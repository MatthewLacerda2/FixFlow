using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Server.Data;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Server.Models;
using Server.Models.Utils;

namespace Server.Controllers;

[ApiController]
[Route("api/v1/login")]
public class LoginController : ControllerBase
{

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ServerContext _context;

    public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration, ServerContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] FlowLoginRequest model)
    {

        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.password, true, false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            var roles = await _userManager.GetRolesAsync(user!);

            var token = GenerateToken(user!, roles.ToArray());

            _context.SaveChanges();

            return Ok(new { token });
        }

        return Unauthorized();
    }

    private string GenerateToken(IdentityUser user, string[] roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF32.GetBytes(_configuration["Jwt:SecretKey"]!);

        var UserDTOJson = JsonConvert.SerializeObject(user);

        var claims = new List<Claim>{
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Role, user.Email!),
            new Claim("UserDTO",UserDTOJson)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(20),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        _context.SaveChanges();

        return tokenHandler.WriteToken(token);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPatch]
    public async Task<IActionResult> PasswordChange([FromBody] FlowLoginRequest userRegister)
    {

        IdentityUser user = _userManager.FindByEmailAsync(userRegister.Email).Result!;
        if (user == null)
        {
            return BadRequest("There is no User with this email");
        }

        if (StringChecker.IsPasswordStrong(userRegister.newPassword))
        {
            return BadRequest("Password must have an Upper-Case, a Lower-Case, a number and a special character");
        }

        await _userManager.ChangePasswordAsync(user, userRegister.password, userRegister.newPassword);

        await _context.SaveChangesAsync();

        return Ok("Password change successfully");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nErro Message: " + ex.Message + "\nErro Data: " + ex.Data);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error logging out");
        }
    }
}