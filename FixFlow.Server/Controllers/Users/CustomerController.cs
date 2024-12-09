using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Controllers;

[ApiController]
[Route(Common.api_v1 + nameof(Customer))]
[Authorize]
[Produces("application/json")]
public class CustomerController : ControllerBase {

	private readonly ServerContext _context;
	private readonly UserManager<Customer> _userManager;

	public CustomerController(ServerContext context, UserManager<Customer> userManager) {
		_context = context;
		_userManager = userManager;
	}

	/// <summary>
	/// Get Customer's Record in the Business
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerRecord))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpGet("record")]
	public async Task<IActionResult> GetCustomerRecord(string customerId) {

		var client = await _userManager.FindByIdAsync(customerId);
		if (client == null) {
			return BadRequest(NotExistErrors.customer);
		}

		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;

		if (client.BusinessId != businessId) {
			return BadRequest(ValidatorErrors.BadCustomerOwnership);
		}

		var clientRecord = (CustomerRecord)client;

		var firstLog = _context.Logs.Where(x => x.CustomerId == customerId).OrderBy(x => x.dateTime).FirstOrDefault();
		if (firstLog != null) {
			clientRecord.firstLog = firstLog.dateTime;
		}

		var lastLog = _context.Logs.Where(x => x.CustomerId == customerId).OrderByDescending(x => x.dateTime).FirstOrDefault();
		if (lastLog != null) {
			clientRecord.firstLog = lastLog.dateTime;
		}

		clientRecord.logs = _context.Logs.Where(x => x.CustomerId == customerId).OrderBy(x => x.dateTime).ToArray();

		for (int i = 0; i < clientRecord.logs.Length - 1; i++) {
			clientRecord.avgTimeBetweenSchedules += (int)(clientRecord.logs[i + 1].dateTime - clientRecord.logs[i].dateTime).TotalDays;
		}

		return Ok(clientRecord);
	}

	/// <summary>
	/// Gets a number of filtered Customers
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDTO[]))]
	[HttpGet]
	public async Task<IActionResult> ReadCustomers(uint offset, uint limit, string? fullname) {
		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;

		var clientsQuery = _context.Customers.AsQueryable();

		clientsQuery = clientsQuery.Where(client => client.BusinessId == businessId);

		if (!string.IsNullOrWhiteSpace(fullname)) {
			//TODO: this is a gambiarra. should've used StringComparison.OrdinalIgnoreCase, but it's bugging. Fix it
			clientsQuery = clientsQuery.Where(client => client.FullName.ToLower().Contains(fullname.ToLower()));
		}

		clientsQuery = clientsQuery.OrderBy(client => client.FullName).ThenBy(client => client.PhoneNumber);

		clientsQuery = clientsQuery.Skip((int)offset).Take((int)limit);

		var clients = await clientsQuery.ToArrayAsync();
		var resultsArray = clients.Select(client => (CustomerDTO)client).ToArray();

		return Ok(resultsArray);
	}

	/// <summary>
	/// Create a Customer's Account
	/// </summary>
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDTO))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	[HttpPost]
	public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreate customerCreate) {

		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;
		customerCreate.BusinessId = businessId;

		bool phoneTaken = _context.Customers.Where(x => x.BusinessId == businessId).Any(x => x.PhoneNumber == customerCreate.PhoneNumber);
		if (phoneTaken) {
			return BadRequest(AlreadyRegisteredErrors.PhoneNumber);
		}

		if (customerCreate.CPF != null) {
			bool cpfTaken = _context.Customers.Where(x => x.BusinessId == businessId).Any(x => x.CPF == customerCreate.CPF);
			if (cpfTaken) {
				return BadRequest(AlreadyRegisteredErrors.CPF);
			}
		}

		if (customerCreate.Email != null) {
			bool emailTaken = _context.Customers.Where(x => x.BusinessId == businessId).Any(x => x.Email == customerCreate.Email);
			if (emailTaken) {
				return BadRequest(AlreadyRegisteredErrors.Email);
			}
		}

		customerCreate.FullName = StringUtils.NameCaseNormalizer(customerCreate.FullName);
		customerCreate.AdditionalNote = StringUtils.PhraseCaseNormalizer(customerCreate.AdditionalNote);

		Customer customer = (Customer)customerCreate;

		IdentityResult userCreationResult = await _userManager.CreateAsync(customer);
		if (!userCreationResult.Succeeded) {
			var errorList = userCreationResult.Errors.Select(e => new { e.Code, e.Description }).ToList();
			var errorJson = System.Text.Json.JsonSerializer.Serialize(errorList);
			return StatusCode(500, "Internal Server Error: Register Business Unsuccessful.\n\n" + errorJson);
		}

		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateCustomer), customer);
	}

	/// <summary>
	/// Updates the Customer's data of the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDTO))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPatch]
	public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDTO upCustomer) {

		var existingCustomer = await _userManager.FindByIdAsync(upCustomer.Id);
		if (existingCustomer == null) {
			return BadRequest(NotExistErrors.customer);
		}

		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;
		if (existingCustomer.BusinessId != businessId) {
			return BadRequest(ValidatorErrors.BadCustomerOwnership);
		}

		if (existingCustomer.CPF != upCustomer.CPF) {
			var existingCPF = _context.Customers.Where(x => x.CPF == upCustomer.CPF);
			if (existingCPF.Any()) {
				return BadRequest(AlreadyRegisteredErrors.CPF);
			}
		}

		if (existingCustomer.PhoneNumber != upCustomer.PhoneNumber) {
			var existingPhonenumber = _context.Customers.Where(x => x.PhoneNumber == upCustomer.PhoneNumber);
			if (existingPhonenumber.Any()) {
				return BadRequest(AlreadyRegisteredErrors.PhoneNumber);
			}
		}

		if (existingCustomer.Email != upCustomer.Email) {
			var existingEmail = _context.Customers.Where(x => x.Email == upCustomer.Email);
			if (existingEmail.Any()) {
				return BadRequest(AlreadyRegisteredErrors.Email);
			}
		}

		upCustomer.FullName = StringUtils.NameCaseNormalizer(upCustomer.FullName);
		upCustomer.AdditionalNote = StringUtils.PhraseCaseNormalizer(upCustomer.AdditionalNote);

		existingCustomer.PhoneNumber = upCustomer.PhoneNumber;
		existingCustomer.CPF = upCustomer.CPF;
		existingCustomer.Email = upCustomer.Email;
		existingCustomer.FullName = upCustomer.FullName;
		existingCustomer.AdditionalNote = upCustomer.AdditionalNote;

		await _context.SaveChangesAsync();

		return Ok((CustomerDTO)existingCustomer);
	}
}
