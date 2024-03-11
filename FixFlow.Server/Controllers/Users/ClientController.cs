using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Newtonsoft.Json;

namespace webserver.Controllers;

/// <summary>
/// Controller class for Client CRUD requests
/// </summary>
[Authorize(Roles=Server.Models.Utils.Common.Secretary_Role)]
[ApiController]
[Route("api/v1/client")]
[Produces("application/json")]
public class ClientController : ControllerBase {

    private readonly ServerContext _context;
    private readonly UserManager<Client> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    /// <summary>
    /// Controller class for Client CRUD requests
    /// </summary>
    public ClientController(ServerContext context, UserManager<Client> userManager, RoleManager<IdentityRole> roleManager){
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Get the Client with the given Id
    /// </summary>
    /// <returns>Client with the given Id. NotFoundResult if there is none</returns>
    /// <response code="200">Returns the Client's DTO</response>
    /// <response code="404">If there is none with the given Id</response>
    [HttpGet("{id}")]
    public IActionResult ReadClient(string id) {

        var client = _context.Clients.Find(id);
        if(client==null){
            return NotFound();
        }

        var response = JsonConvert.SerializeObject((ClientDTO)client);

        return Ok(response);
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Client>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet]
    public async Task<IActionResult> ReadClients(string? username, int? offset, int limit, string? sort) {

        if (limit < 1) {
            return BadRequest("Limit parameter must be a natural number greater than 0");
        }

        var clients = _context.Clients.AsQueryable();

        if(!string.IsNullOrEmpty(username)){
            clients = clients.Where(client => client.UserName!.Contains(username)); //Is this case sensitive???
        }

        if(!string.IsNullOrEmpty(sort)){
            sort = sort.ToLower();
            switch (sort) {
                case "name":
                    clients = clients.OrderBy(c => c.UserName);
                    break;
            }
        }

        if(offset.HasValue){
            clients = clients.Skip(offset.Value);
        }
        clients = clients.Take(limit);

        var resultQuery = await clients.ToArrayAsync();
        var resultsArray = resultQuery.Select(c=>(ClientDTO)c).ToArray();
        
        if(resultsArray==null || resultsArray.Length==0){
            return NotFound();
        }
        
        var response = JsonConvert.SerializeObject(resultsArray);

        return Ok(response);
    }

    /// <summary>
    /// Creates a Client User
    /// </summary>
    /// <returns>The created Client's Data</returns>
    /// <response code="200">ClientDTO</response>
    /// <response code="400">Returns a string with the requirements that were not filled</response>
    /// <response code="400">In case the Client's data is already Registered (it will tell which data)</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Client))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] ClientDTO clientDto, string password) {

        var existingName = _context.Clients.Where(c=>c.FullName == clientDto.FullName);
        if(existingName != null){
            return BadRequest("FullName already registered!");
        }

        var existingPhone = _context.Clients.Where(c=>c.PhoneNumber == clientDto.PhoneNumber);
        if(existingPhone != null){
            return BadRequest("PhoneNumber already registered!");
        }

        if(!string.IsNullOrEmpty(clientDto.CPF)){
            var existingCPF = _context.Clients.Where(c=>c.CPF == clientDto.CPF);
            if (existingCPF != null) {
                return BadRequest("CPF already registered!");
            }
        }else{
            clientDto.CPF = string.Empty;
        }
        
        if(!string.IsNullOrEmpty(clientDto.Email)){
            var existingEmail = await _userManager.FindByEmailAsync(clientDto.Email);
            if (existingEmail != null) {
                return BadRequest("Email already registered!");
            }
        }else{
            clientDto.Email = string.Empty;
        }

        Client client = new Client (clientDto.FullName, clientDto.CPF, clientDto.PhoneNumber, clientDto.Email);

        var result = await _userManager.CreateAsync(client, password);

        if(!result.Succeeded){
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Client))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [HttpPatch]
    public async Task<IActionResult> UpdateClient([FromBody] ClientDTO upClient) {

        var existingClient = _context.Clients.Find(upClient.Id);
        if (existingClient==null) {
            return BadRequest("Client does not Exist!");
        }

        existingClient.UserName = upClient.FullName;
        existingClient.PhoneNumber = upClient.PhoneNumber;

        if(!string.IsNullOrEmpty(upClient.CPF)){
            existingClient.CPF = upClient.CPF;
        }
        if(!string.IsNullOrEmpty(upClient.Email)){
            existingClient.Email = upClient.Email;
        }

        await _context.SaveChangesAsync();

        var response = JsonConvert.SerializeObject((ClientDTO)existingClient);

        return Ok(response);
    }

    /// <summary>
    /// Deletes the Client with the given Id
    /// </summary>
    /// <returns>NoContent if successfull</returns>
    /// <response code="200">Client was found, and thus deleted</response>
    /// <response code="400">Client not found</response>
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Client))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(string id) {

        var client = _context.Clients.Find(id);
        if(client == null){
            return BadRequest("Client does not Exist!");
        }
        
        _context.Clients.Remove(client);

        await _context.SaveChangesAsync();

        return NoContent();
    }    
}