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
	/// Used when the User logs-in or opens the app
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Business))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpGet]
	public async Task<IActionResult> GetBusiness() {

		var claimBusinessId = User.Claims.FirstOrDefault(c => c.Type == "businessId");
		if (claimBusinessId == null) {
			return BadRequest(ValidatorErrors.TokenNotHaveBusinessId);
		}

		string businessId = claimBusinessId.Value;
		if (businessId == null) {
			return BadRequest(ValidatorErrors.NullBusinessId);
		}

		var business = await _userManager.FindByIdAsync(businessId);
		if (business == null) {
			return BadRequest(NotExistErrors.Business);
		}

		return Ok(business);
	}

	/// <summary>
	/// Creates a Business User
	/// </summary>
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
			return BadRequest(ValidatorErrors.InvalidOTP);
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
		business.Id = new Guid().ToString();

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
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Business))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPatch]
	public async Task<IActionResult> UpdateBusiness([FromBody] Business upBusiness) {

		var businessExists = await _userManager.FindByIdAsync(upBusiness.Id);
		if (businessExists == null) {
			return BadRequest(NotExistErrors.Business);
		}

		if (upBusiness.BusinessDays.Count != 7) {
			return BadRequest(ValidatorErrors.BusinessDayCountMustBe7);
		}

		for (int i = 0; i < 7; i++) {
			if (upBusiness.BusinessDays[i].start >= upBusiness.BusinessDays[i].end) {
				return BadRequest($"Business Day start time must be less than End time, for day {i + 1}.");
			}
			if (upBusiness.BusinessDays[i].dayOfTheWeek != i) {
				return BadRequest($"Business Day dayOfTheWeek must be {i}, for day {i + 1}.");
			}
		}

		businessExists.Name = upBusiness.Name;
		businessExists.BusinessDays = upBusiness.BusinessDays;

		await _context.SaveChangesAsync();

		return Ok(upBusiness);
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
	/// Deletes the Business with the given Id
	/// </summary>
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
