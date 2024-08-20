using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Utils;
using Server.Models.Erros;

namespace Server.Controllers;

/// <summary>
/// Controller class for Business CRUD requests
/// </summary>
[ApiController]
[Route(Common.api_route + "business")]
[Produces("application/json")]
public class BusinessController : ControllerBase
{

    private readonly ServerContext _context;
    private readonly UserManager<Business> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public BusinessController(ServerContext context, UserManager<Business> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Get the Business with the given Id
    /// </summary>
    /// <param name="Id">The Client's Id</param>
    /// <returns>BusinessDTO</returns>/// 
    /// <response code="200">The Business's DTO</response>
    /// <response code="404">There was no Business with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BusinessDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{Id}")]
    public async Task<IActionResult> ReadBusiness(string Id)
    {

        var business = await _userManager.FindByIdAsync(Id);

        if (business == null)
        {
            return NotFound(NotExistErrors.Business);
        }

        return Ok((BusinessDTO)business);
    }

    /// <summary>
    /// Gets a number of Business, with optional filters
    /// </summary>
    /// <remarks>
    /// Does not return Not Found, but an Array of size 0 instead
    /// </remarks>
    /// <param name="username">Filters results to only Users whose username contains this string</param>
    /// <param name="offset">Offsets the result by a given amount</param>
    /// <param name="limit">Limits the number of results</param>
    /// <param name="sort">Orders the result by a given field. Does not order if the field does not exist</param>
    /// <returns>BusinessDTO[]</returns>
    /// <response code="200">Returns an array ofBusinessDTO</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BusinessDTO[]>))]
    [HttpGet]
    public async Task<IActionResult> ReadBusiness(string? username, uint? offset, uint? limit, string? sort)
    {

        var businessQuery = _context.Business.AsQueryable();

        if (!string.IsNullOrWhiteSpace(username))
        {
            businessQuery = businessQuery.Where(Business => Business.UserName!.Contains(username, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(sort))
        {
            sort = sort.ToLower();
            if (sort.Contains("name"))
            {
                businessQuery.OrderBy(s => s.Name).ThenBy(s => s.Name);
            }
        }

        if (!string.IsNullOrWhiteSpace(sort) && sort.Contains("desc"))
        {
            businessQuery.Reverse();
        }

        offset = offset.HasValue ? offset : 0;
        limit = limit.HasValue ? limit : 10;

        businessQuery = businessQuery.Skip((int)offset).Take((int)limit);

        var resultsArray = await businessQuery.Select(c => (BusinessDTO)c).ToArrayAsync();

        return Ok(resultsArray);
    }

    /// <summary>
    /// Creates a Business User
    /// </summary>
    /// <returns>The created Business's Data</returns>
    /// <response code="200">BusinessDTO</response>
    /// <response code="400">The Client's (PhoneNumber || CPF || Email) does not exist</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusinessDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    [HttpPost]
    public async Task<IActionResult> CreateBusiness([FromBody] BusinessRegister businessDto)
    {

        var existingEmail = await _userManager.FindByEmailAsync(businessDto.Email);
        if (existingEmail != null)
        {
            return BadRequest(AlreadyRegisteredErrors.Email);
        }

        var existingCPF = _context.Business.Where(c => c.CPF == businessDto.CPF);
        if (existingCPF.Any())
        {
            return BadRequest(AlreadyRegisteredErrors.CPF);
        }

        var existingPhone = _context.Business.Where(c => c.PhoneNumber == businessDto.PhoneNumber);
        if (existingPhone.Any())
        {
            return BadRequest(AlreadyRegisteredErrors.PhoneNumber);
        }

        Business Business = new Business(businessDto);

        var userCreationResult = await _userManager.CreateAsync(Business, businessDto.confirmPassword);

        if (!userCreationResult.Succeeded)
        {
            return StatusCode(500, "Internal Server Error: Register Business Unsuccessful");
        }

        var roleExist = await _roleManager.RoleExistsAsync(Common.Business_Role);
        if (!roleExist)
        {
            await _roleManager.CreateAsync(new IdentityRole(Common.Business_Role));
        }

        var userRoleAddResult = await _userManager.AddToRoleAsync(Business, Common.Business_Role);

        if (!userRoleAddResult.Succeeded)
        {
            return StatusCode(500, "Internal Server Error: Add Business Role Unsuccessful");
        }

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateBusiness), (BusinessDTO)Business);
    }

    /// <summary>
    /// Updates the Business with the given Id
    /// </summary>
    /// <returns>BusinessDTO</returns>
    /// <response code="200">Updated Business's DTO</response>
    /// <response code="400">There was no Business with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusinessDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPatch]
    public async Task<IActionResult> UpdateBusiness([FromBody] BusinessRegister upBusiness)
    {

        var existingBusiness = await _userManager.FindByIdAsync(upBusiness.Id);
        if (existingBusiness == null)
        {
            return BadRequest(NotExistErrors.Business);
        }

        if (existingBusiness.CPF != upBusiness.CPF)
        {
            var existingCPF = _context.Business.Where(x => x.CPF == upBusiness.CPF);
            if (existingCPF.Any())
            {
                return BadRequest(AlreadyRegisteredErrors.CPF);
            }
            else
            {
                existingBusiness.CPF = upBusiness.CPF;
            }
        }

        if (existingBusiness.CNPJ != upBusiness.CNPJ)
        {
            var existingCNPJ = _context.Business.Where(x => x.CNPJ == upBusiness.CNPJ);
            if (existingCNPJ.Any())
            {
                return BadRequest(AlreadyRegisteredErrors.CNPJ);
            }
            else
            {
                existingBusiness.CNPJ = upBusiness.CNPJ;
            }
        }

        if (existingBusiness.UserName != upBusiness.Name)
        {
            var existingUsername = _context.Business.Where(x => x.UserName == upBusiness.Name);
            if (existingUsername.Any())
            {
                return BadRequest(AlreadyRegisteredErrors.UserName);
            }
            else
            {
                await _userManager.SetUserNameAsync(existingBusiness, upBusiness.Name);
            }
        }

        if (existingBusiness.PhoneNumber != upBusiness.PhoneNumber)
        {
            var existingPhonenumber = _context.Business.Where(x => x.PhoneNumber == upBusiness.PhoneNumber);
            if (existingPhonenumber.Any())
            {
                return BadRequest(AlreadyRegisteredErrors.PhoneNumber);
            }
            else
            {
                await _userManager.SetPhoneNumberAsync(existingBusiness, upBusiness.PhoneNumber);
            }
        }

        if (!string.IsNullOrWhiteSpace(upBusiness.password) && !string.IsNullOrWhiteSpace(upBusiness.confirmPassword))
        {
            await _userManager.ChangePasswordAsync(existingBusiness, upBusiness.password, upBusiness.confirmPassword);
        }

        existingBusiness.Name = upBusiness.Name;

        await _context.SaveChangesAsync();

        return Ok((BusinessDTO)existingBusiness);
    }

    /// <summary>
    /// Deletes the Business with the given Id
    /// </summary>
    /// <param name="Id">The Id of the Business to be deleted</param>
    /// <returns>NoContentResult</returns>
    /// <response code="200">Business was found, and thus deleted</response>
    /// <response code="400">There was no Business with the given Id</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteBusiness(string Id)
    {

        var business = await _userManager.FindByIdAsync(Id);
        if (business == null)
        {
            return BadRequest(NotExistErrors.Business);
        }

        await _userManager.DeleteAsync(business);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}