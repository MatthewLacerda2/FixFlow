using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Utils;

namespace Server.Controllers;

/// <summary>
/// Controller class for Client CRUD requests
/// </summary>
[ApiController]
[Route(Common.api_route + "client")]
[Produces("application/json")]
public class ClientController : ControllerBase
{

    private readonly ServerContext _context;
    private readonly UserManager<Client> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ClientController(ServerContext context, UserManager<Client> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Get a Client with the given Id
    /// </summary>
    /// <returns>ClientDTO</returns>
    /// <param name="Id">The Client's Id</param>
    /// <response code="200">The ClientDTO</response>
    /// <response code="404">There was no Client with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet("{Id}")]
    public async Task<IActionResult> ReadClient(string Id)
    {

        var client = await _userManager.FindByIdAsync(Id);
        if (client == null)
        {
            return NotFound("Client does not exist");
        }

        return Ok((ClientDTO)client);
    }

    /// <summary>
    /// Gets a number of Clients, with optional filters
    /// </summary>
    /// <remarks>
    /// Does not return Not Found, but an Array of size 0 instead
    /// </remarks>
    /// <returns>ClientDTO[]</returns>
    /// <param name="fullname">Filter the Clients whose fullname contain the given string</param>
    /// <param name="offset">Offsets the result by a given amount</param>
    /// <param name="limit">Limits the result by a given amount</param>
    /// <param name="sort">Orders the result by Client, Price or DateTime. Add suffix 'desc' to order descending</param>
    /// <response code="200">Returns an array of ClientDTO</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientDTO[]>))]
    [HttpGet]
    public async Task<IActionResult> ReadClients(string? fullname, int? offset, int? limit, string? sort)
    {

        var clientsQuery = _context.Clients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(fullname))
        {
            clientsQuery = clientsQuery.Where(client => client.FullName!.Contains(fullname, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(sort))
        {
            sort = sort.ToLower();
            if (sort.Contains("name"))
            {
                clientsQuery = clientsQuery.OrderBy(c => c.FullName).ThenBy(c => c.UserName);
            }
        }

        if (!string.IsNullOrWhiteSpace(sort) && sort.Contains("desc"))
        {
            clientsQuery.Reverse();
        }

        offset = offset.HasValue ? offset : 0;
        limit = limit.HasValue ? limit : 10;

        clientsQuery = clientsQuery.Skip((int)offset).Take((int)limit);

        var resultsArray = await clientsQuery.Select(c => (ClientDTO)c).ToArrayAsync();

        return Ok(resultsArray);
    }

    /// <summary>
    /// Create a Client Account
    /// </summary>
    /// <returns>ClientDTO</returns>
    /// <response code="200">The created Client's DTO</response>
    /// <response code="400">The Client's (PhoneNumber || CPF || Email) does not exist</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] ClientRegister clientRegister)
    {

        if (!string.IsNullOrWhiteSpace(clientRegister.PhoneNumber))
        {

            var existingPhone = _context.Clients.Where(c => c.PhoneNumber == clientRegister.PhoneNumber);
            if (existingPhone != null)
            {
                return BadRequest("PhoneNumber already registered!");
            }

        }
        else
        {
            clientRegister.PhoneNumber = string.Empty;
        }

        if (!string.IsNullOrWhiteSpace(clientRegister.CPF))
        {

            var existingCPF = _context.Clients.Where(c => c.CPF == clientRegister.CPF);
            if (existingCPF != null)
            {
                return BadRequest("CPF already registered!");
            }

        }
        else
        {
            clientRegister.CPF = string.Empty;
        }

        if (!string.IsNullOrWhiteSpace(clientRegister.Email))
        {
            var existingEmail = await _userManager.FindByEmailAsync(clientRegister.Email);
            if (existingEmail == null)
            {
                existingEmail = await _context.Clients.FirstOrDefaultAsync(x => x.Email == clientRegister.Email);
            }
            if (existingEmail != null)
            {
                return BadRequest("Email already registered!");
            }
        }
        else
        {
            clientRegister.Email = string.Empty;
        }

        Client client = new Client(clientRegister);

        IdentityResult userCreationResult = new IdentityResult();

        if (client.signedUp)
        {
            userCreationResult = await _userManager.CreateAsync(client, clientRegister.newPassword);
        }
        else
        {
            userCreationResult = await _userManager.CreateAsync(client);
        }

        if (!userCreationResult.Succeeded)
        {
            return StatusCode(500, "Internal Server Error: Register Client Unsuccessful");
        }

        var roleExist = await _roleManager.RoleExistsAsync(Common.Client_Role);
        if (!roleExist)
        {
            await _roleManager.CreateAsync(new IdentityRole(Common.Client_Role));
        }

        var userRoleAddResult = await _userManager.AddToRoleAsync(client, Common.Client_Role);

        if (!userRoleAddResult.Succeeded)
        {
            return StatusCode(500, "Internal Server Error: Add Client Role Unsuccessful");
        }

        _context.SaveChanges();

        return CreatedAtAction(nameof(CreateClient), (ClientDTO)client);
    }

    /// <summary>
    /// Updates the Client with the given Id
    /// </summary>
    /// <returns>ClientDTO</returns>
    /// <response code="200">Updated Client's DTO</response>
    /// <response code="400">There was no Client with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPatch]
    public async Task<IActionResult> UpdateClient([FromBody] ClientRegister upClient)
    {

        var existingClient = await _userManager.FindByIdAsync(upClient.Id);
        if (existingClient == null)
        {
            return BadRequest("Client does not Exist!");
        }

        if (existingClient.CPF != upClient.CPF)
        {
            var existingCPF = _context.Clients.Where(x => x.CPF == upClient.CPF);
            if (existingCPF.Any())
            {
                return BadRequest("CPF taken");
            }
            else
            {
                existingClient.CPF = upClient.CPF;
            }
        }

        if (existingClient.UserName != upClient.UserName)
        {
            var existingUsername = _context.Clients.Where(x => x.UserName == upClient.UserName);
            if (existingUsername.Any())
            {
                return BadRequest("Username already exists");
            }
            else
            {
                await _userManager.SetUserNameAsync(existingClient, upClient.UserName);
            }
        }

        if (existingClient.PhoneNumber != upClient.PhoneNumber)
        {
            var existingPhonenumber = _context.Clients.Where(x => x.PhoneNumber == upClient.PhoneNumber);
            if (existingPhonenumber.Any())
            {
                return BadRequest("PhoneNumber taken");
            }
            else
            {
                await _userManager.SetPhoneNumberAsync(existingClient, upClient.PhoneNumber);
            }
        }

        existingClient.additionalNote = upClient.additionalNote;

        await _context.SaveChangesAsync();

        return Ok((ClientDTO)existingClient);
    }

    /// <summary>
    /// Deletes the Client with the given Id
    /// </summary>
    /// <param name="Id">The Id of the Client to be deleted</param>
    /// <returns>NoContentResult</returns>
    /// <response code="204">Client was found, and thus deleted</response>
    /// <response code="400">There was no Client with the given Id</response>
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ClientDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteClient(string Id)
    {

        var client = await _userManager.FindByIdAsync(Id);
        if (client == null)
        {
            return BadRequest("Client does not Exist!");
        }

        await _userManager.DeleteAsync(client);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}