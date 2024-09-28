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
	/// Gets the Business with the given Id.
	/// Used when the User opens the app, but is already logged in
	/// </summary>
	/// <returns>The Business' Data</returns>
	/// <response code="200"></response>
	/// <response code="400"></response>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Business))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpGet]
	public async Task<IActionResult> GetBusiness([FromBody] string businessId) {

		var business = await _userManager.FindByIdAsync(businessId);
		if (business == null) {
			return BadRequest(NotExistErrors.Business);
		}

		return Ok(business);
	}

	/// <summary>
	/// Creates a Business User
	/// </summary>
	/// <returns>The created Business' Data</returns>
	/// <response code="200"></response>
	/// <response code="400"></response>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Business))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	[HttpPost]
	public async Task<IActionResult> CreateBusiness([FromBody] BusinessRegisterRequest businessRegister) {

		OTP otp = _context.OTPs.Where(o => o.PhoneNumber == businessRegister.PhoneNumber)
								.Where(o => o.ExpiryTime <= DateTime.Now.AddMinutes(Common.otpExpirationTimeInMinutes))
								.Where(o => o.IsUsed == false)
								.First();
		if (otp == null) {
			return BadRequest("Código inválido");
		}

		//TODO:validate CNPJ

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

		Business business = (Business)businessRegister;

		DateTime[,] businessDays = new DateTime[2, 7];
		for (int i = 0; i < 7; i++) {
			business.BusinessDays[0, i] = new DateTime(2024, 1, 1, 8, 0, 0);
			business.BusinessDays[1, i] = new DateTime(2024, 1, 1, 18, 0, 0);
		}
		business.BusinessDays = businessDays;

		var userCreationResult = await _userManager.CreateAsync(business, businessRegister.confirmPassword);
		if (!userCreationResult.Succeeded) {
			return StatusCode(500, "Internal Server Error: Register Business Unsuccessful");
		}

		otp.IsUsed = true;
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateBusiness), business);
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

		if (upBusiness.BusinessDays.GetLength(0) != 2 || upBusiness.BusinessDays.GetLength(1) != 7) {
			return BadRequest(ValidatorErrors.BusinessDaysInvalidMatrix);
		}

		for (int i = 0; i < 7; i++) {
			if (upBusiness.BusinessDays[0, i] > upBusiness.BusinessDays[1, i]) {
				return BadRequest($"Business Days start time must be less than End time, for day {i + 1}.");
			}
		}

		businessExists.Name = upBusiness.Name;
		businessExists.BusinessDays = upBusiness.BusinessDays;

		await _context.SaveChangesAsync();

		return Ok(upBusiness);
	}

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
	/// Deletes the Business with the given Id
	/// </summary>
	/// <param name="Id">The Id of the Business to be deleted</param>
	/// <returns>NoContentResult</returns>
	/// <response code="200">Business was found, and thus deleted</response>
	/// <response code="400">There was no Business with the given Id</response>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpDelete]
	public async Task<IActionResult> DeleteBusiness([FromBody] string Id) {

		var business = await _userManager.FindByIdAsync(Id);
		if (business == null) {
			return BadRequest(NotExistErrors.Business);
		}

		_context.Logs.RemoveRange(_context.Logs.Where(x => x.BusinessId == Id));
		_context.Contacts.RemoveRange(_context.Contacts.Where(x => x.businessId == Id));
		_context.Schedules.RemoveRange(_context.Schedules.Where(x => x.BusinessId == Id));

		_context.Clients.RemoveRange(_context.Clients.Where(x => x.BusinessId == Id));

		await _userManager.DeleteAsync(business);

		await _context.SaveChangesAsync();

		return NoContent();
	}
}
