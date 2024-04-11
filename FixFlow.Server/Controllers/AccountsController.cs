using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Server.Models;
using Server.Models.Utils;
using Server.Data;

namespace webserver.Controllers;

/// <summary>
/// Controller class for Login, Logout and Password Reset
/// </summary>
[ApiController]
[Route(Common.api_route + "accounts")]
[Produces("application/json")]
public class AccountsController : ControllerBase
{

    private readonly UserManager<Client> _userManager;

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ServerContext _context;

    public AccountsController(UserManager<Client> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, ServerContext context)
    {
        _userManager = userManager;

        _signInManager = signInManager;
        _configuration = configuration;
        _context = context;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> Login([FromBody] FlowLoginRequest credentials)
    {

    }
}