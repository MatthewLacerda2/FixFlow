using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Utils;

namespace webserver.Controllers;

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

    /// <summary>
    /// Controller class for Client CRUD requests
    /// </summary>
    public ClientController(ServerContext context, UserManager<Client> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    /// <summary>
    /// Get the Client with the given Id
    /// </summary>
    /// <returns>Client with the given Id. NotFoundResult if there is none</returns>
    /// <response code="200">Returns the Client's DTO</response>
    /// <response code="404">If there is none with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadClient(string id)
    {

        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }

        return Ok((ClientDTO)client);
    }

    /// <summary>
    /// Get all Clients within optional filters
    /// </summary>
    /// <returns>ClientDTO Array</returns>
    /// <param name="username">Filters results to only Users whose username contains this string</param>
    /// <param name="offset">Offsets the result by a given amount</param>
    /// <param name="limit">Limits the number of results</param>
    /// <param name="sort">Orders the result by a given field. Does not order if the field does not exist</param>
    /// <response code="200">Returns an array of Client DTOs</response>
    /// <response code="404">If no Clients fit the given filters</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientDTO[]>))]
    [HttpGet]
    public async Task<IActionResult> ReadClients(string? username, int? offset, int? limit, string? sort)
    {

        var clientsQuery = _context.Clients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(username))
        {
            clientsQuery = clientsQuery.Where(client => client.UserName!.Contains(username, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(sort))
        {
            sort = sort.ToLower();
            if (sort.Contains("name"))
            {
                clientsQuery = clientsQuery.OrderBy(c => c.FullName).ThenBy(c => c.UserName);
            }
        }

        offset = offset.HasValue ? offset : 0;
        limit = limit.HasValue ? limit : 10;

        clientsQuery = clientsQuery.Skip((int)offset).Take((int)limit);

        var resultsArray = await clientsQuery.Select(c => (ClientDTO)c).ToArrayAsync();

        if (!string.IsNullOrWhiteSpace(sort) && sort.Contains("desc"))
        {
            resultsArray.Reverse();
        }

        return Ok(resultsArray);
    }

    /// <summary>
    /// Creates a Client User
    /// </summary>
    /// <returns>The created Client's Data</returns>
    /// <response code="200">ClientDTO</response>
    /// <response code="400">Returns a string with the requirements that were not filled</response>
    /// <response code="400">In case the Client's data is already Registered (it will tell which data)</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
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
            if (existingEmail != null)
            {
                return BadRequest("Email already registered!");
            }
        }
        else
        {
            clientRegister.Email = string.Empty;
        }

        Client client = new Client(clientRegister.FullName, clientRegister.CPF, clientRegister.PhoneNumber, clientRegister.Email, clientRegister.additionalNote);

        var result = await _userManager.CreateAsync(client, clientRegister.newPassword);

        if (!result.Succeeded)
        {
            return StatusCode(500, "Internal Server Error: Register Client Unsuccessful");
        }

        return CreatedAtAction(nameof(CreateClient), (ClientDTO)client);
    }

    /// <summary>
    /// Updates the Client with the given Id
    /// </summary>
    /// <returns>Client's DTO with the updated Data</returns>
    /// <response code="200">Client's DTO with the updated data</response>
    /// <response code="400">If a Client with the given Id was not found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPatch]
    public async Task<IActionResult> UpdateClient([FromBody] ClientRegister upClient)
    {

        var existingClient = _context.Clients.Find(upClient.Id);
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
                await _userManager.SetUserNameAsync(existingClient, upClient.UserName);
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

        existingClient = (Client)upClient;

        if (!string.IsNullOrWhiteSpace(upClient.currentPassword) && !string.IsNullOrWhiteSpace(upClient.newPassword))
        {
            await _userManager.ChangePasswordAsync(existingClient, upClient.currentPassword, upClient.newPassword);
        }

        await _context.SaveChangesAsync();

        return Ok((ClientDTO)existingClient);
    }

    /// <summary>
    /// Deletes the Client with the given Id
    /// </summary>
    /// <returns>NoContent if successfull</returns>
    /// <response code="200">Client was found, and thus deleted</response>
    /// <response code="400">Client not found</response>
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ClientDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(string id)
    {

        var client = _context.Clients.Find(id);
        if (client == null)
        {
            return BadRequest("Client does not Exist!");
        }

        _context.Clients.Remove(client);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}