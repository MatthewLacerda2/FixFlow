using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Controllers;

/// <summary>
/// Controller class for Customer's stuff
/// </summary>
[ApiController]
[Route(Common.api_v1 + nameof(Customer))]
//[Authorize]
[Produces("application/json")]
public class CustomerController : ControllerBase {

	private readonly ServerContext _context;
	private readonly UserManager<Customer> _userManager;

	public CustomerController(ServerContext context, UserManager<Customer> userManager) {
		_context = context;
		_userManager = userManager;
	}

	/// <summary>
	/// Get Customer Record in the Business.
	/// Credentials, but also schedules and logs history
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerRecord))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpGet("{id}")]
	public async Task<IActionResult> GetCustomerRecord(string customerId) {

		var client = await _userManager.FindByIdAsync(customerId);
		if (client == null) {
			return BadRequest(NotExistErrors.Customer);
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

		clientRecord.numSchedules = _context.Schedules.Where(x => x.CustomerId == customerId).Count();

		for (int i = 0; i < clientRecord.logs.Length - 1; i++) {
			clientRecord.avgTimeBetweenSchedules += (int)(clientRecord.logs[i + 1].dateTime - clientRecord.logs[i].dateTime).TotalDays;
		}

		return Ok(clientRecord);
	}

	/// <summary>
	/// Gets a number of filtered Customers
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerDTO[]>))]
	[HttpGet]
	public async Task<IActionResult> ReadCustomers(string businessId, uint offset, uint limit, string? fullname) {
		var clientsQuery = _context.Customers.AsQueryable();

		clientsQuery = clientsQuery.Where(client => client.BusinessId == businessId);

		if (!string.IsNullOrWhiteSpace(fullname)) {
			clientsQuery = clientsQuery.Where(client => client.FullName.Contains(fullname));
		}

		clientsQuery = clientsQuery.OrderBy(client => client.FullName).ThenBy(client => client.PhoneNumber);

		clientsQuery = clientsQuery.Skip((int)offset).Take((int)limit);

		var clients = await clientsQuery.ToArrayAsync();
		var resultsArray = clients.Select(client => (CustomerDTO)client).ToArray();

		return Ok(resultsArray);
	}

	/// <summary>
	/// Create a Customer Account
	/// </summary>
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDTO))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	[HttpPost]
	public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreate customerCreate) {

		bool phoneTaken = _context.Customers.Where(x => x.BusinessId == customerCreate.BusinessId).Any(x => x.PhoneNumber == customerCreate.PhoneNumber);
		if (phoneTaken) {
			return BadRequest(AlreadyRegisteredErrors.PhoneNumber);
		}

		if (customerCreate.CPF != null) {
			bool cpfTaken = _context.Customers.Where(x => x.BusinessId == customerCreate.BusinessId).Any(x => x.CPF == customerCreate.CPF);
			if (cpfTaken) {
				return BadRequest(AlreadyRegisteredErrors.CPF);
			}
		}

		if (customerCreate.Email != null) {
			bool emailTaken = _context.Customers.Where(x => x.BusinessId == customerCreate.BusinessId).Any(x => x.Email == customerCreate.Email);
			if (emailTaken) {
				return BadRequest(AlreadyRegisteredErrors.Email);
			}
		}

		Customer customer = (Customer)customerCreate;

		IdentityResult userCreationResult = await _userManager.CreateAsync(customer);
		if (!userCreationResult.Succeeded) {
			return StatusCode(500, "Internal Server Error: Register Customer Unsuccessful");
		}

		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateCustomer), (CustomerDTO)customer);
	}

	/// <summary>
	/// Updates the Customer with the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDTO))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPatch]
	public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDTO upCustomer) {

		var existingCustomer = await _userManager.FindByIdAsync(upCustomer.Id);
		if (existingCustomer == null) {
			return BadRequest(NotExistErrors.Customer);
		}

		if (existingCustomer.CPF != upCustomer.CPF) {
			var existingCPF = _context.Customers.Where(x => x.CPF == upCustomer.CPF);
			if (existingCPF.Any()) {
				return BadRequest(AlreadyRegisteredErrors.CPF);
			}
			else {
				existingCustomer.CPF = upCustomer.CPF;
			}
		}

		if (existingCustomer.PhoneNumber != upCustomer.PhoneNumber) {
			var existingPhonenumber = _context.Customers.Where(x => x.PhoneNumber == upCustomer.PhoneNumber);
			if (existingPhonenumber.Any()) {
				return BadRequest(AlreadyRegisteredErrors.PhoneNumber);
			}
			else {
				existingCustomer.PhoneNumber = upCustomer.PhoneNumber;
			}
		}

		if (existingCustomer.Email != upCustomer.Email) {
			var existingEmail = _context.Customers.Where(x => x.Email == upCustomer.Email);
			if (existingEmail.Any()) {
				return BadRequest(AlreadyRegisteredErrors.Email);
			}
			else {
				existingCustomer.Email = upCustomer.Email;
			}
		}

		existingCustomer.FullName = upCustomer.FullName;
		existingCustomer.AdditionalNote = upCustomer.AdditionalNote;

		await _context.SaveChangesAsync();

		return Ok((CustomerDTO)existingCustomer);
	}
}
