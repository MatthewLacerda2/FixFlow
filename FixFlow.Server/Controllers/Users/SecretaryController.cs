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
/// Controller class for Secretary CRUD requests
/// </summary>
[Authorize(Roles = Server.Models.Utils.Common.Secretary_Role)]
[ApiController]
[Route("api/v1/Secretary")]
[Produces("application/json")]
public class SecretaryController : ControllerBase
{

    private readonly ServerContext _context;
    private readonly UserManager<Secretary> _userManager;

    /// <summary>
    /// Controller class for Secretary CRUD requests
    /// </summary>
    public SecretaryController(ServerContext context, UserManager<Secretary> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    /// <summary>
    /// Get the Secretary with the given Id
    /// </summary>
    /// <returns>Secretary with the given Id. NotFoundResult if there is none</returns>
    /// <response code="200">Returns the Secretary's DTO</response>
    /// <response code="404">If there is none with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SecretaryDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadSecretary(string id)
    {

        var Secretary = await _context.Secretarys.FindAsync(id);
        if (Secretary == null)
        {
            return NotFound();
        }

        var response = JsonConvert.SerializeObject((SecretaryDTO)Secretary);

        return Ok(response);
    }

    /// <summary>
    /// Get all Secretarys within optional filters
    /// </summary>
    /// <returns>SecretaryDTO Array</returns>
    /// <param name="username">Filters results to only Users whose username contains this string</param>
    /// <param name="offset">Offsets the result by a given amount</param>
    /// <param name="limit">Limits the number of results</param>
    /// <param name="sort">Orders the result by a given field. Does not order if the field does not exist</param>
    /// <response code="200">Returns an array of Secretary DTOs</response>
    /// <response code="404">If no Secretarys fit the given filters</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SecretaryDTO[]>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet]
    public async Task<IActionResult> ReadSecretarys(string? username, int? offset, int limit, string? sort)
    {

        if (limit < 1)
        {
            return BadRequest("Limit parameter must be a natural number greater than 0");
        }

        var Secretarys = _context.Secretarys.AsQueryable();

        if (!string.IsNullOrEmpty(username))
        {
            Secretarys = Secretarys.Where(Secretary => Secretary.UserName!.Contains(username));
        }

        if (!string.IsNullOrEmpty(sort))
        {
            sort = sort.ToLower();
            switch (sort)
            {
                case "name":
                    Secretarys = Secretarys.OrderBy(emp => emp.UserName);
                    break;
            }
        }

        if (offset.HasValue)
        {
            Secretarys = Secretarys.Skip(offset.Value);
        }
        Secretarys = Secretarys.Take(limit);

        var resultQuery = await Secretarys.ToArrayAsync();
        var resultsArray = resultQuery.Select(c => (SecretaryDTO)c).ToArray();

        if (resultsArray.Length == 0)
        {
            return NotFound();
        }

        var response = JsonConvert.SerializeObject(resultsArray);

        return Ok(response);
    }

    /// <summary>
    /// Creates a Secretary User
    /// </summary>
    /// <returns>The created Secretary's Data</returns>
    /// <response code="200">SecretaryDTO</response>
    /// <response code="400">Returns a string with the requirements that were not filled</response>
    /// <response code="400">In case the Secretary's data is already Registered (it will tell which data)</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SecretaryDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [HttpPost]
    public async Task<IActionResult> CreateSecretary([FromBody] SecretaryDTO SecretaryDto, string password)
    {

        var existingName = _context.Secretarys.Where(c => c.FullName == SecretaryDto.FullName);
        if (existingName != null)
        {
            return BadRequest("FullName already registered!");
        }

        var existingEmail = await _userManager.FindByEmailAsync(SecretaryDto.Email);
        if (existingEmail != null)
        {
            return BadRequest("Email already registered!");
        }

        var existingCPF = _context.Secretarys.Where(c => c.CPF == SecretaryDto.CPF);
        if (existingCPF != null)
        {
            return BadRequest("CPF already registered!");
        }

        var existingPhone = _context.Secretarys.Where(c => c.PhoneNumber == SecretaryDto.PhoneNumber);
        if (existingPhone != null)
        {
            return BadRequest("PhoneNumber already registered!");
        }

        Secretary Secretary = new Secretary(SecretaryDto.FullName, SecretaryDto.Email, SecretaryDto.CPF, SecretaryDto.PhoneNumber, SecretaryDto.salary);

        var result = await _userManager.CreateAsync(Secretary, password);

        if (!result.Succeeded)
        {
            return StatusCode(500, "Internal Server Error: Register Secretary Unsuccessful");
        }

        return CreatedAtAction(nameof(CreateSecretary), (SecretaryDTO)Secretary);
    }

    /// <summary>
    /// Updates the Secretary with the given Id
    /// </summary>
    /// <returns>Secretary's DTO with the updated Data</returns>
    /// <response code="200">Secretary's DTO with the updated data</response>
    /// <response code="400">If a Secretary with the given Id was not found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SecretaryDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [HttpPatch]
    public async Task<IActionResult> UpdateSecretary([FromBody] SecretaryDTO upSecretary)
    {

        var existingSecretary = _context.Secretarys.Find(upSecretary.Id);
        if (existingSecretary == null)
        {
            return BadRequest("Secretary does not Exist!");
        }

        existingSecretary = (Secretary)upSecretary;

        await _context.SaveChangesAsync();

        var response = JsonConvert.SerializeObject((SecretaryDTO)existingSecretary);

        return Ok(response);
    }

    /// <summary>
    /// Deletes the Secretary with the given Id
    /// </summary>
    /// <returns>NoContent if successfull</returns>
    /// <response code="200">Secretary was found, and thus deleted</response>
    /// <response code="400">Secretary not found</response>
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(SecretaryDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSecretary(string id)
    {

        var Secretary = _context.Secretarys.Find(id);
        if (Secretary == null)
        {
            return BadRequest("Secretary does not Exist!");
        }

        _context.Secretarys.Remove(Secretary);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}