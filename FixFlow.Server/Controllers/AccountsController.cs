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
using Server.Models.PasswordReset;
using Server.Services;

namespace Server.Controllers;

[ApiController]
[Route("api/v1/accounts")]
public class AccountsController : ControllerBase
{

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ServerContext _context;
    private readonly EmailResetPasswordService _emailResetPasswordService;

    public static readonly int ResetEmailTokenExpirationInMinutes = 15;

    public AccountsController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
                                IConfiguration configuration, ServerContext context, EmailResetPasswordService emailResetPasswordService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
        _emailResetPasswordService = emailResetPasswordService;
    }

    /// <summary>
    /// Get a Token when Logging in, with either UserName or Email, and Password
    /// </summary>
    /// <returns>string</returns>
    /// <response code="200">Successfull login</response>
    /// <response code="401">Unauthorized login</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] FlowLoginRequest model)
    {

        if (!string.IsNullOrEmpty(model.Email))
        {

            var userExists = _userManager.FindByEmailAsync(model.Email).Result;

            if (userExists == null)
            {
                return Unauthorized("Wrong UserName/Email or Password");
            }
            else
            {
                var hasPassword = await _userManager.HasPasswordAsync(userExists);

                if (!hasPassword)
                {
                    return Unauthorized("Wrong UserName/Email or Password");
                }

                model.UserName = userExists.UserName!;
            }

        }

        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.password, true, false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            var roles = await _userManager.GetRolesAsync(user!);

            var token = GenerateToken(user!, roles.ToArray());

            await _context.SaveChangesAsync();
            Console.WriteLine(token);
            return Ok(token);
        }

        return Unauthorized("Wrong UserName/Email or Password");
    }

    private string GenerateToken(IdentityUser user, string[] roles)
    {
        var UserDTOJson = JsonConvert.SerializeObject(user);

        var claims = new List<Claim>{
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim("UserDTO",UserDTOJson)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = Encoding.UTF32.GetBytes(_configuration["Jwt:SecretKey"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(20),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Change password of the User with the given Email
    /// </summary>
    /// <returns>string</returns>
    /// <response code="200">Password change successfull</response>
    /// <response code="400">Unauthorized</response>
    /// <response code="401">Password change unsucessfull</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[Authorize]
    [HttpPatch]
    public async Task<IActionResult> PasswordChange([FromBody] FlowLoginRequest userRegister)
    {

        IdentityUser user = _userManager.FindByEmailAsync(userRegister.Email).Result!;
        if (user == null)
        {
            return BadRequest("Password Change Unsuccessfull.");
        }

        var result = await _userManager.ChangePasswordAsync(user, userRegister.password, userRegister.newPassword);

        if (!result.Succeeded)
        {
            return BadRequest("Password Changed Unsuccessfull. " + result.Errors);
        }

        await _context.SaveChangesAsync();

        return Ok("Password change successfully");
    }

    /// <summary>
    /// Sends an email with the link for resetting the password
    /// </summary>
    /// <returns>NoContentResult</returns>
    /// <response code="404">Email not found</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("reset/request")]
    public async Task<ActionResult> PasswordResetEmail([FromBody] string email)
    {

        IdentityUser user = _userManager.FindByEmailAsync(email).Result!;
        if (user == null)
        {
            return BadRequest("Email not found");
        }

        var hasEmail = _context.Resets.Where(r => r.Email == email).First();
        if (hasEmail != null && hasEmail.dateTime >= DateTime.Now.AddMinutes(-ResetEmailTokenExpirationInMinutes))
        {
            return BadRequest("Cannot send Email at this time");
        }

        var PasswordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        PasswordReset passwordReset = new PasswordReset(email, PasswordResetToken, DateTime.Now);
        await _context.Resets.AddAsync(passwordReset);

        await _emailResetPasswordService.SendResetPasswordEmailAsync(passwordReset);

        return Ok("Password Reset Email sent");

    }

    /// <summary>
    /// Changes password for the User who wanted to reset it
    /// 
    /// User must have the link sent in the 'Password Reset' email
    /// </summary>
    /// <returns>string</returns>
    /// <response code="200">Password change successfull</response>
    /// <response code="401">Password change unsucessfull</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPatch("reset/link")]
    public async Task<IActionResult> PasswordReset([FromBody] PasswordResetRequest passwordReset)
    {

        IdentityUser user = _userManager.FindByEmailAsync(passwordReset.Email).Result!;
        if (user == null)
        {
            return BadRequest("Email not found");
        }

        PasswordReset pr = _context.Resets.Find(passwordReset.token)!;

        if (pr == null || pr.dateTime < DateTime.Now.AddMinutes(-ResetEmailTokenExpirationInMinutes))
        {
            return BadRequest("Password Change Unsuccessfull. ");
        }

        var result = await _userManager.ResetPasswordAsync(user, passwordReset.token, passwordReset.password);

        if (!result.Succeeded)
        {
            return BadRequest("Password Change Unsuccessfull. " + result.Errors);
        }

        await _context.SaveChangesAsync();

        return Ok("Password changed successfully");
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