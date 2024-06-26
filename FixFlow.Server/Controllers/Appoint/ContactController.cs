using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Server.Models;
using Server.Models.Utils;
using Server.Models.Appointments;
using Server.Data;
using Server.Models.Filters;

namespace Server.Controllers;

/// <summary>
/// Controller class for Appointment Contact CRUD requests
/// </summary>
/// <remarks>
/// At the Contact's date, we are reminded to 'Contact' the Client to setup a new appointment
/// </remarks>
[ApiController]
[Route(Common.api_route + "contacts")]
[Produces("application/json")]
public class ContactController : ControllerBase
{

    private readonly ServerContext _context;
    private readonly UserManager<Client> _userManager;

    public ContactController(ServerContext context, UserManager<Client> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    /// <summary>
    /// Get the Contact with the given Id
    /// </summary>
    /// <returns>AptContact</returns>
    /// <param name="Id">The Contact's Id</param>
    /// <response code="200">The AppointmentContact with the given Id</response>
    /// <response code="404">There was no Appointment Contact with the given Id</response>/// 
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptContact))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet("{Id}")]
    public async Task<IActionResult> ReadContact(string Id)
    {

        var contact = await _context.Contacts.FindAsync(Id);

        if (contact == null)
        {
            return NotFound("Contact does not exist");
        }

        return Ok(contact);
    }

    /// <summary>
    /// Gets a number of Appointment Contacts, with optional filters
    /// </summary>
    /// <remarks>
    /// Does not return Not Found, but an Array of size 0 instead
    /// </remarks>
    /// <param name="filter">The Filter Properties of the Query</param>
    /// <returns>AptContact[]</returns>
    /// <response code="200">Returns an array of AppointmentContact</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AptContact[]>))]
    [HttpGet]
    public IActionResult ReadContact([FromBody] AptContactFilter filter)
    {

        var ContactsQuery = _context.Contacts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.ClientId))
        {
            ContactsQuery = ContactsQuery.Where(x => x.ClientId == filter.ClientId);
        }

        ContactsQuery = ContactsQuery.Where(x => x.dateTime >= filter.minDateTime);
        ContactsQuery = ContactsQuery.Where(x => x.dateTime <= filter.maxDateTime);

        switch (filter.sort)
        {
            case ContactSort.date:
                ContactsQuery = ContactsQuery.OrderBy(s => s.dateTime).ThenByDescending(s => s.ClientId).ThenBy(s => s.Id);
                break;
            case ContactSort.ClientId:
                ContactsQuery = ContactsQuery.OrderBy(s => s.ClientId).ThenByDescending(s => s.dateTime).ThenBy(s => s.Id);
                break;
        }

        if (filter.descending)
        {
            ContactsQuery.Reverse();
        }

        var resultArray = ContactsQuery
            .Skip(filter.offset)
            .Take(filter.limit)
            .ToArray();

        return Ok(resultArray);
    }

    /// <summary>
    /// Create an Appointment Contact
    /// </summary>
    /// <returns>AptContact</returns>
    /// <response code="200">The created Appointment Contact</response>
    /// <response code="400">The given (ClientId || LogId) does not exist</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AptContact))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] AptContact newContact)
    {

        var existingClient = _userManager.FindByIdAsync(newContact.ClientId);
        if (existingClient == null)
        {
            return BadRequest("Client does not exist");
        }

        var existingBusiness = _context.Business.Find(newContact.businessId);
        if (existingBusiness == null)
        {
            return BadRequest("Business does not exist");
        }

        var existingLog = _context.Logs.Find(newContact.aptLogId);
        if (existingLog == null)
        {
            return BadRequest("Log does not exist");
        }


        _context.Contacts.Add(newContact);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateContact), newContact);
    }

    /// <summary>
    /// Update the Appointment Contact with the given Id
    /// </summary>
    /// <returns>AptContact</returns>
    /// <response code="200">The updated Appointment Contact</response>
    /// <response code="400">There was no AptContact with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptContact))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPut]
    public async Task<IActionResult> UpdateContact([FromBody] AptContact upAppointment)
    {

        var existingContact = _context.Contacts.Find(upAppointment.Id);
        if (existingContact == null)
        {
            return BadRequest("Contact does not exist");
        }

        _context.Contacts.Update(upAppointment);
        await _context.SaveChangesAsync();

        return Ok(upAppointment);
    }

    /// <summary>
    /// Deletes the Appointment Contact with the given Id
    /// </summary>
    /// <param name="Id">The Id of the AptContact to be deleted</param>
    /// <returns>NoContentResult</returns>
    /// <response code="204">No Content</response>
    /// <response code="400">There was no Contact with the given Id</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteContact(string Id)
    {

        var ContactToDelete = _context.Contacts.Find(Id);
        if (ContactToDelete == null)
        {
            return BadRequest("Contact does not exist");
        }

        _context.Contacts.Remove(ContactToDelete);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}