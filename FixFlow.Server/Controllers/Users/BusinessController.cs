using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;
using Server.Utils;

namespace Server.Controllers;

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
	/// Gets the Business' Data of the given Id.
	/// Used mostly when the User logs-in or opens the app
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusinessDTO))]
	[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Subscription))]
	[HttpGet]
	public async Task<IActionResult> GetBusiness(string businessId) {
		string tokenBusinessId = User.Claims.First(c => c.Type == "businessId")?.Value!;

		if (businessId != tokenBusinessId) {
			return Unauthorized();
		}

		var business = await _userManager.FindByIdAsync(businessId);
		if (business == null) {
			return BadRequest(NotExistErrors.business);
		}
		if (business.IsActive == false) {
			return BadRequest(ValidatorErrors.DeactivatedBusiness);
		}

		var lateBill = _context.Subscriptions
		.Where(s => s.BusinessId == businessId)
		.OrderByDescending(s => s.dateTime).First();

		if (lateBill.timeSpentDeactivated > TimeSpan.Zero) {
			TimeSpan toAdd = new TimeSpan(30, 0, 0, 0) - lateBill.timeSpentDeactivated;
			lateBill.dateTime = DateTime.Now - new TimeSpan(30, 0, 0, 0) + toAdd;
			lateBill.timeSpentDeactivated = TimeSpan.Zero;
		}

		if (lateBill.Payed == false && lateBill.dateTime.AddMonths(1).AddDays(5) <= DateTime.Now) {
			return Unauthorized(lateBill);
		}

		if (lateBill.Payed == true) {
			Subscription newSub = new Subscription(business.Id, business.Name, business.CNPJ, DateTime.Now, 2000, false, null);
			_context.Subscriptions.Add(newSub);
			_context.SaveChanges();
		}

		return Ok(new BusinessDTO(business));
	}

	/// <summary>
	/// Registers a Business User
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

		Business business = (Business)businessRegister;
		Subscription firstMonth = new Subscription(business.Id, business.Name, business.CNPJ, DateTime.Now, 2000, false, null);

		var userCreationResult = await _userManager.CreateAsync(business, businessRegister.ConfirmPassword);

		if (!userCreationResult.Succeeded) {
			var errorList = userCreationResult.Errors.Select(e => new { e.Code, e.Description }).ToList();
			var errorJson = System.Text.Json.JsonSerializer.Serialize(errorList);
			return StatusCode(500, "Internal Server Error: Register Business Unsuccessful.\n\n" + errorJson);
		}

		_context.Subscriptions.Add(firstMonth);
		Console.WriteLine("it breaks after this line");
		await _context.SaveChangesAsync();
		Console.WriteLine("this line doesnt output because the line above breaks the thing");
		return CreatedAtAction(nameof(CreateBusiness), business);
	}

	/// <summary>
	/// Updates the Business of the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusinessDTO))]
	[HttpPatch]
	public async Task<IActionResult> UpdateBusiness([FromBody] BusinessDTO upBusiness) {

		string Id = User.Claims.First(c => c.Type == "businessId")?.Value!;

		var business = await _userManager.FindByIdAsync(Id);

		if (upBusiness.Services.Length > 16) {
			return BadRequest(ValidatorErrors.TooManyServices);
		}

		for (int i = 0; i < upBusiness.Services.Length; i++) {

			if (string.IsNullOrEmpty(upBusiness.Services[i])) {
				return BadRequest(ValidatorErrors.ServiceNameIsBlank);
			}

			string phrase = upBusiness.Services[i];
			phrase = phrase.Trim();
			phrase = char.ToUpper(phrase[0]) + phrase.Substring(1).ToLower();
			if (phrase.Length > 32) {
				return BadRequest(ValidatorErrors.ServiceNameIsTooBig);
			}

			upBusiness.Services[i] = phrase;

		}

		business!.Name = upBusiness.Name;
		business.Services = upBusiness.Services;
		business.AllowListedServicesOnly = upBusiness.AllowListedServicesOnly;
		business.OpenOnHolidays = upBusiness.OpenOnHolidays;

		await _context.SaveChangesAsync();

		return Ok(new BusinessDTO(business));
	}

	/// <summary>
	/// Deactivates the Business Account of the given Id.
	/// That freezes subscription and stops notifications
	/// </summary>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[HttpPatch("deactivate")]
	public async Task<IActionResult> DeactivateBusiness() {
		string Id = User.Claims.First(c => c.Type == "businessId")?.Value!;

		var business = await _userManager.FindByIdAsync(Id);
		business!.IsActive = false;

		Subscription sub = _context.Subscriptions.Where(s => s.BusinessId == business.Id).OrderByDescending(s => s.dateTime).First();

		sub.timeSpentDeactivated = DateTime.Now - sub.dateTime;

		await _context.SaveChangesAsync();

		return Ok();
	}

	/// <summary>
	/// Deletes the Business
	/// </summary>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[HttpDelete]
	public async Task<IActionResult> DeleteBusiness() {
		string Id = User.Claims.First(c => c.Type == "businessId")?.Value!;

		_context.Schedules.RemoveRange(_context.Schedules.Where(x => x.BusinessId == Id));
		_context.Logs.RemoveRange(_context.Logs.Where(x => x.BusinessId == Id));
		_context.Contacts.RemoveRange(_context.Contacts.Where(x => x.BusinessId == Id));
		_context.Customers.RemoveRange(_context.Customers.Where(x => x.BusinessId == Id));

		var business = await _userManager.FindByIdAsync(Id);
		await _userManager.DeleteAsync(business!);

		await _context.SaveChangesAsync();

		return NoContent();
	}
}
