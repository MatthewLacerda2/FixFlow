using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Controllers;

/// <summary>
/// Controller class for Business CRUD requests
/// </summary>
[ApiController]
[Route(Common.api_v1 + nameof(Business))]
[Authorize]
[Produces("application/json")]
public class BusinessController : ControllerBase {

	private readonly ServerContext _context;
	private readonly UserManager<Business> _userManager;

	public BusinessController(ServerContext context, UserManager<Business> userManager) {
		_context = context;
		_userManager = userManager;
	}

	/// <summary>
	/// Gets the Business with the given Id.
	/// Used when the User logs-in or opens the app
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusinessDTO))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpGet]
	public async Task<IActionResult> GetBusiness(string businessId) {

		var business = await _userManager.FindByIdAsync(businessId);
		if (business == null) {
			return BadRequest(NotExistErrors.Business);
		}

		return Ok(new BusinessDTO(business));
	}

	/// <summary>
	/// Creates a Business User
	/// </summary>
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Business))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	[AllowAnonymous]
	[HttpPost]
	public async Task<IActionResult> CreateBusiness([FromBody] BusinessRegisterRequest businessRegister) {

		var existingEmail = await _userManager.FindByEmailAsync(businessRegister.Email);
		if (existingEmail != null) {
			return BadRequest(AlreadyRegisteredErrors.Email);
		}
		var existingPhone = _context.Business.Where(c => c.PhoneNumber == businessRegister.PhoneNumber);
		if (existingPhone.Any()) {
			return BadRequest(AlreadyRegisteredErrors.PhoneNumber);
		}
		var existingCNPJ = _context.Business.Where(c => c.CNPJ == businessRegister.CNPJ);
		if (existingCNPJ.Any()) {
			return BadRequest(AlreadyRegisteredErrors.CNPJ);
		}

		//TODO:validate CNPJ

		Business business = (Business)businessRegister;

		var userCreationResult = await _userManager.CreateAsync(business, businessRegister.ConfirmPassword);

		if (!userCreationResult.Succeeded) {
			var errorList = userCreationResult.Errors.Select(e => new { e.Code, e.Description }).ToList();
			var errorJson = System.Text.Json.JsonSerializer.Serialize(errorList);
			return StatusCode(500, "Internal Server Error: Register Business Unsuccessful.\n\n" + errorJson);
		}

		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateBusiness), business);
	}

	/// <summary>
	/// Updates the Business with the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusinessDTO))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPatch]
	public async Task<IActionResult> UpdateBusiness([FromBody] BusinessDTO upBusiness) {

		var businessExists = await _userManager.FindByIdAsync(upBusiness.Id);
		if (businessExists == null) {
			return BadRequest(NotExistErrors.Business);
		}

		businessExists.Name = upBusiness.Name;
		businessExists.BusinessWeek = upBusiness.BusinessWeek;
		businessExists.services = upBusiness.Services;
		businessExists.allowListedServicesOnly = upBusiness.AllowListedServicesOnly;
		businessExists.openOnHolidays = upBusiness.OpenOnHolidays;

		await _context.SaveChangesAsync();

		return Ok(new BusinessDTO(businessExists));
	}

	/// <summary>
	/// Deactivates the Business Account with the given Id.
	/// That freezes the subscription, and stops notifications
	/// </summary>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPatch("deactivate")]
	public async Task<IActionResult> DeactivateBusiness([FromBody] string Id) {

		var business = await _userManager.FindByIdAsync(Id);
		if (business == null) {
			return BadRequest(NotExistErrors.Business);
		}

		business.IsActive = false;

		await _context.SaveChangesAsync();

		return Ok();
	}

	/// <summary>
	/// Deletes the Business with the given Id and all it's data owned by it
	/// </summary>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpDelete]
	public async Task<IActionResult> DeleteBusiness([FromBody] string Id) {

		var business = await _userManager.FindByIdAsync(Id);
		if (business == null) {
			return BadRequest(NotExistErrors.Business);
		}

		_context.Contacts.RemoveRange(_context.Contacts.Where(x => x.businessId == Id));
		_context.Schedules.RemoveRange(_context.Schedules.Where(x => x.BusinessId == Id));
		_context.Logs.RemoveRange(_context.Logs.Where(x => x.BusinessId == Id));

		_context.Customers.RemoveRange(_context.Customers.Where(x => x.BusinessId == Id));

		await _userManager.DeleteAsync(business);

		await _context.SaveChangesAsync();

		return NoContent();
	}
}
