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
[Produces("application/json")]
public class BusinessController : ControllerBase {

	private readonly ServerContext _context;
	private readonly UserManager<Business> _userManager;

	public BusinessController(ServerContext context, UserManager<Business> userManager) {
		_context = context;
		_userManager = userManager;
	}

	/// <summary>
	/// Creates a Business User
	/// </summary>
	/// <returns>The created Business's Data</returns>
	/// <response code="200">BusinessInfo</response>
	/// <response code="400"></response>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Business))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	[HttpPost]
	public async Task<IActionResult> CreateBusiness([FromBody] BusinessRegisterRequest businessRegister) {

		OTP otp = _context.OTPs.Where(o => o.PhoneNumber == businessRegister.PhoneNumber && o.Code == businessRegister.OTPCode).FirstOrDefault()!;
		if (otp == null) {
			return BadRequest("Código inválido");
		}
		if (otp.Purpose != OTP_use_purpose.create_business) {
			return BadRequest("Código inválido");
		}
		if (otp.ExpiryTime < DateTime.UtcNow || otp.IsUsed) {
			return BadRequest("Código expirado");
		}

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

		Business Business = (Business)businessRegister;

		var userCreationResult = await _userManager.CreateAsync(Business, businessRegister.confirmPassword);
		if (!userCreationResult.Succeeded) {
			return StatusCode(500, "Internal Server Error: Register Business Unsuccessful");
		}

		otp.IsUsed = true;
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateBusiness), Business);
	}

	/// <summary>
	/// Updates the Business with the given Id
	/// </summary>
	/// <returns>BusinessDTO</returns>
	/// <response code="200">Updated Business's DTO</response>
	/// <response code="400">There was no Business with the given Id</response>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Business))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPatch]
	public async Task<IActionResult> UpdateBusiness([FromBody] Business upBusiness) {

		var businessExists = await _userManager.FindByIdAsync(upBusiness.Id);
		if (businessExists == null) {
			return BadRequest(NotExistErrors.Business);
		}

		businessExists.Name = upBusiness.Name;
		businessExists.BusinessDays = upBusiness.BusinessDays;
		businessExists.Services = upBusiness.Services;
		businessExists.allowListedServicesOnly = upBusiness.allowListedServicesOnly;
		businessExists.holidayOpen = upBusiness.holidayOpen;
		businessExists.domicileService = upBusiness.domicileService;

		await _context.SaveChangesAsync();

		return Ok(upBusiness);
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
	public async Task<IActionResult> DeleteBusiness(string Id) {

		var business = await _userManager.FindByIdAsync(Id);
		if (business == null) {
			return BadRequest(NotExistErrors.Business);
		}

		_context.Logs.RemoveRange(_context.Logs.Where(x => x.businessId == Id));
		_context.Contacts.RemoveRange(_context.Contacts.Where(x => x.businessId == Id));
		_context.Schedules.RemoveRange(_context.Schedules.Where(x => x.businessId == Id));

		_context.Clients.RemoveRange(_context.Clients.Where(x => x.BusinessId == Id));

		await _userManager.DeleteAsync(business);

		await _context.SaveChangesAsync();

		return NoContent();
	}
}
