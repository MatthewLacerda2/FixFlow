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
/// Controller class for Client's stuff
/// </summary>
[ApiController]
[Route(Common.api_v1 + nameof(Client))]
[Produces("application/json")]
public class ClientController : ControllerBase {

	private readonly ServerContext _context;
	private readonly UserManager<Client> _userManager;

	public ClientController(ServerContext context, UserManager<Client> userManager) {
		_context = context;
		_userManager = userManager;
	}

	/// <summary>
	/// Get Client Record in the Business.
	/// Credentials, but also schedules and logs history
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientRecord))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpGet("{id}")]
	public async Task<IActionResult> GetClientRecord(string clientId) {

		var client = await _userManager.FindByIdAsync(clientId);
		if (client == null) {
			return BadRequest(NotExistErrors.Client);
		}

		var clientRecord = (ClientRecord)client;

		var firstLog = _context.Logs.Where(x => x.ClientId == clientId).OrderBy(x => x.dateTime).FirstOrDefault();
		if (firstLog != null) {
			clientRecord.firstLog = firstLog.dateTime;
		}

		var lastLog = _context.Logs.Where(x => x.ClientId == clientId).OrderByDescending(x => x.dateTime).FirstOrDefault();
		if (lastLog != null) {
			clientRecord.firstLog = lastLog.dateTime;
		}

		clientRecord.logs = _context.Logs.Where(x => x.ClientId == clientId).ToArray();

		clientRecord.numSchedules = _context.Schedules.Where(x => x.ClientId == clientId).Count();

		for (int i = 0; i < clientRecord.logs.Length - 1; i++) {
			clientRecord.avgTimeBetweenSchedules += (int)(clientRecord.logs[i + 1].dateTime - clientRecord.logs[i].dateTime).TotalDays;
		}

		return Ok(clientRecord);
	}

	/// <summary>
	/// Gets a number of filtered Clients
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientDTO[]>))]
	[HttpGet]
	public async Task<IActionResult> ReadClients(string businessId, uint offset, uint limit, string? fullname) {
		var clientsQuery = _context.Clients.AsQueryable();

		clientsQuery = clientsQuery.Where(client => client.BusinessId == businessId);

		if (!string.IsNullOrWhiteSpace(fullname)) {
			clientsQuery = clientsQuery.Where(client => client.FullName.Contains(fullname));
		}

		clientsQuery = clientsQuery.OrderBy(client => client.FullName).ThenBy(client => client.PhoneNumber);

		clientsQuery = clientsQuery.Skip((int)offset).Take((int)limit);

		var clients = await clientsQuery.ToArrayAsync();
		var resultsArray = clients.Select(client => (ClientDTO)client).ToArray();

		return Ok(resultsArray);
	}

	/// <summary>
	/// Create a Client Account
	/// </summary>
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientDTO))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	[HttpPost]
	public async Task<IActionResult> CreateClient([FromBody] ClientCreate clientCreate) {

		bool phoneTaken = _context.Clients.Where(x => x.BusinessId == clientCreate.businessId).Any(x => x.PhoneNumber == clientCreate.PhoneNumber);
		if (phoneTaken) {
			return BadRequest(AlreadyRegisteredErrors.PhoneNumber);
		}

		if (clientCreate.CPF == null) {
			bool cpfTaken = _context.Clients.Where(x => x.BusinessId == clientCreate.businessId).Any(x => x.CPF == clientCreate.CPF);
			if (cpfTaken) {
				return BadRequest(AlreadyRegisteredErrors.CPF);
			}
		}

		if (clientCreate.Email != null) {

			bool emailTaken = _context.Clients.Where(x => x.BusinessId == clientCreate.businessId).Any(x => x.Email == clientCreate.Email);
			if (emailTaken) {
				return BadRequest(AlreadyRegisteredErrors.Email);
			}
		}

		Client client = (Client)clientCreate;

		IdentityResult userCreationResult = await _userManager.CreateAsync(client);
		if (!userCreationResult.Succeeded) {
			return StatusCode(500, "Internal Server Error: Register Client Unsuccessful");
		}

		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateClient), (ClientDTO)client);
	}

	/// <summary>
	/// Updates the Client with the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDTO))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPatch]
	public async Task<IActionResult> UpdateClient([FromBody] ClientDTO upClient) {

		var existingClient = await _userManager.FindByIdAsync(upClient.Id);
		if (existingClient == null) {
			return BadRequest(NotExistErrors.Client);
		}

		if (existingClient.CPF != upClient.CPF) {
			var existingCPF = _context.Clients.Where(x => x.CPF == upClient.CPF);
			if (existingCPF.Any()) {
				return BadRequest(AlreadyRegisteredErrors.CPF);
			}
			else {
				existingClient.CPF = upClient.CPF;
			}
		}

		if (existingClient.PhoneNumber != upClient.PhoneNumber) {
			var existingPhonenumber = _context.Clients.Where(x => x.PhoneNumber == upClient.PhoneNumber);
			if (existingPhonenumber.Any()) {
				return BadRequest(AlreadyRegisteredErrors.PhoneNumber);
			}
			else {
				await _userManager.SetPhoneNumberAsync(existingClient, upClient.PhoneNumber);
			}
		}

		if (existingClient.Email != upClient.Email) {
			var existingEmail = _context.Clients.Where(x => x.Email == upClient.Email);
			if (existingEmail.Any()) {
				return BadRequest(AlreadyRegisteredErrors.Email);
			}
			else {
				await _userManager.SetEmailAsync(existingClient, upClient.Email);
			}
		}

		existingClient.AdditionalNote = upClient.AdditionalNote;

		await _context.SaveChangesAsync();

		return Ok((ClientDTO)existingClient);
	}
}
