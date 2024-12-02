using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Controllers;

/// <summary>
/// Controller class for Appointment Contact .R.U.D requests
/// </summary>
/// <remarks>
/// Contacts are 'reminders' for the Clients to Schedule another appointment
/// </remarks>
[ApiController]
[Route(Common.api_v1 + "contacts")]
[Authorize]
[Produces("application/json")]
public class AptContactController : ControllerBase {

	private readonly ServerContext _context;

	public AptContactController(ServerContext context) {
		_context = context;
	}

	/// <summary>
	/// Gets a number of filtered Contacts
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptContact[]))]
	[HttpGet]
	public async Task<IActionResult> ReadContacts(string? clientName, DateTime minDateTime, DateTime maxDateTime, int offset, int limit) {

		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;

		var contactsQuery = _context.Contacts.AsQueryable();

		contactsQuery = contactsQuery.Where(x => x.BusinessId == businessId);

		if (!string.IsNullOrWhiteSpace(clientName)) {
			contactsQuery = contactsQuery.Where(x => x.customer.FullName.Contains(clientName!));
		}

		contactsQuery = contactsQuery.Where(x => x.dateTime.Date >= minDateTime.Date);
		contactsQuery = contactsQuery.Where(x => x.dateTime.Date <= maxDateTime.Date);

		var resultsArray = await contactsQuery
			.Skip(offset)
			.Take(limit)
			.ToArrayAsync();

		for (int i = 0; i < resultsArray.Length; i++) {
			resultsArray[i].customer = _context.Customers.Find(resultsArray[i].CustomerId)!;
			resultsArray[i].aptLog = _context.Logs.Find(resultsArray[i].aptLogId)!;
		}

		return Ok(resultsArray);
	}

	/// <summary>
	/// Update the Appointment Contact's DateTime with the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptContact))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPatch]
	public async Task<IActionResult> UpdateContactDateTime([FromBody] UpdateAptContact upContact) {

		var existingContact = _context.Contacts.Find(upContact.Id);
		if (existingContact == null) {
			return BadRequest(NotExistErrors.AptContact);
		}

		existingContact.dateTime = upContact.dateTime;

		await _context.SaveChangesAsync();

		return Ok(existingContact);
	}

	/// <summary>
	/// Deletes the Appointment Contact with the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpDelete]
	public async Task<IActionResult> DeleteContact([FromBody] string Id) {

		var contactToDelete = _context.Contacts.Find(Id);
		if (contactToDelete == null) {
			return BadRequest(NotExistErrors.AptContact);
		}

		_context.Contacts.Remove(contactToDelete);

		await _context.SaveChangesAsync();
		return NoContent();
	}
}
