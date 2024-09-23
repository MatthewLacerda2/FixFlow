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
[Route(Common.api_route + "client")]
[Produces("application/json")]
public class ClientController : ControllerBase {

	private readonly ServerContext _context;
	private readonly UserManager<Client> _userManager;

	public ClientController(ServerContext context, UserManager<Client> userManager) {
		_context = context;
		_userManager = userManager;
	}

	/// <summary>
	/// Get a Client with the given Id
	/// </summary>
	/// <returns>ClientDTO</returns>
	/// <param name="Id">The Client's Id</param>
	/// <response code="200">The ClientDTO</response>
	/// <response code="404">There was no Client with the given Id</response>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientDTO>))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
	[HttpGet("{Id}")]
	public async Task<IActionResult> ReadClient(string Id) {

		var client = await _userManager.FindByIdAsync(Id);
		if (client == null) {
			return NotFound(NotExistErrors.Client);
		}

		return Ok((ClientDTO)client);
	}

	/// <summary>
	/// Gets a number of Clients, with optional filters
	/// </summary>
	/// <remarks>
	/// Does not return Not Found, but an Array of size 0 instead
	/// </remarks>
	/// <returns>ClientDTO[]</returns>
	/// <param name="fullname">Filter the Clients whose fullname contain the given string</param>
	/// <param name="offset">Offsets the result by a given amount</param>
	/// <param name="limit">Limits the result by a given amount</param>
	/// <param name="sort">Orders the result by Client, Price or DateTime. Add suffix 'desc' to order descending</param>
	/// <response code="200">Returns an array of ClientDTO</response>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientDTO[]>))]
	[HttpGet]
	public async Task<IActionResult> ReadClients(string? fullname, uint? offset, uint? limit, string? sort) {

		var clientsQuery = _context.Clients.AsQueryable();

		if (!string.IsNullOrWhiteSpace(fullname)) {
			clientsQuery = clientsQuery.Where(client => client.FullName!.Contains(fullname, StringComparison.OrdinalIgnoreCase));
		}

		if (!string.IsNullOrWhiteSpace(sort)) {
			sort = sort.ToLower();
			if (sort.Contains("name")) {
				clientsQuery = clientsQuery.OrderBy(c => c.FullName).ThenBy(c => c.UserName);
			}
		}

		if (!string.IsNullOrWhiteSpace(sort) && sort.Contains("desc")) {
			clientsQuery.Reverse();
		}

		offset = offset.HasValue ? offset : 0;
		limit = limit.HasValue ? limit : 10;

		clientsQuery = clientsQuery.Skip((int)offset).Take((int)limit);

		var resultsArray = await clientsQuery.Select(c => (ClientDTO)c).ToArrayAsync();

		return Ok(resultsArray);
	}

	/// <summary>
	/// Create a Client Account
	/// </summary>
	/// <returns>ClientDTO</returns>
	/// <response code="200">The created Client's DTO</response>
	/// <response code="400">The Client's (PhoneNumber || CPF || Email) does not exist</response>
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientDTO))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	[HttpPost]
	public async Task<IActionResult> CreateClient([FromBody] ClientCreate clientRegister) {

		Client client = new Client(clientRegister);

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
	/// <returns>ClientDTO</returns>
	/// <response code="200">Updated Client's DTO</response>
	/// <response code="400">There was no Client with the given Id</response>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDTO))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPatch]
	public async Task<IActionResult> UpdateClient([FromBody] ClientCreate upClient) {

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

		existingClient.additionalNote = upClient.additionalNote;

		await _context.SaveChangesAsync();

		return Ok((ClientDTO)existingClient);
	}
}
