using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Server.Models;
using Server.Models.Utils;
using Server.Models.Appointments;
using Server.Data;

namespace Server.Controllers;

/// <summary>
/// Controller class for Appointment Reminder CRUD requests
/// </summary>
/// <remarks>
/// At the Reminder's date, we are 'reminded' to contact the Client to setup a new appointment
/// </remarks>
[ApiController]
[Route(Common.api_route + "reminders")]
[Produces("application/json")]
public class ReminderController : ControllerBase
{

    private readonly ServerContext _context;
    private readonly UserManager<Client> _userManager;

    public ReminderController(ServerContext context, UserManager<Client> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    /// <summary>
    /// Get the Reminder with the given Id
    /// </summary>
    /// <returns>AppointmentReminder</returns>
    /// <param name="Id">The Reminder's Id</param>
    /// <response code="200">Returns an array of AppointmentLogs</response>
    /// <response code="404">There was no Appointment Reminder with the given Id</response>/// 
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptReminder))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet("{Id}")]
    public async Task<IActionResult> ReadReminder(string Id)
    {

        var remind = await _context.Reminders.FindAsync(Id);

        if (remind == null)
        {
            return NotFound();
        }

        return Ok(remind);
    }

    /// <summary>
    /// Gets a number of Appointment Reminders, with optional filters
    /// </summary>
    /// <remarks>
    /// Does not return Not Found, but an Array of size 0 instead
    /// </remarks>
    /// <returns>AppointmentReminder Array</returns>
    /// <param name="ClientId">Filter by a specific Client</param>
    /// <param name="minDateTime">The oldest DateTime the Appointment took place</param>
    /// <param name="maxDateTime">The most recent DateTime the Appointment took placet</param>/// 
    /// <param name="sort">Orders the result by Client, or DateTime. Add suffix 'desc' to order descending</param>
    /// <param name="offset">Offsets the result by a given amount</param>
    /// <param name="limit">Limits the result by a given amount</param>
    /// <response code="200">Returns an array of AppointmentLogs</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AptReminder[]>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpGet]
    public IActionResult ReadReminder(string? ClientId, DateTime? minDateTime, DateTime? maxDateTime,
                                    string? sort, int? offset, int? limit)
    {

        var reminders = _context.Reminders.AsQueryable();

        if (!string.IsNullOrWhiteSpace(ClientId))
        {
            reminders = reminders.Where(x => x.ClientId == ClientId);
        }

        if (minDateTime.HasValue)
        {
            reminders = reminders.Where(x => x.dateTime >= minDateTime);
        }

        if (maxDateTime.HasValue)
        {
            reminders = reminders.Where(x => x.dateTime <= maxDateTime);
        }

        if (!string.IsNullOrWhiteSpace(sort))
        {
            sort = sort.ToLower();
            if (sort.Contains("client"))
            {
                reminders = reminders.OrderBy(s => s.ClientId).ThenByDescending(s => s.dateTime).ThenBy(s => s.Id);
            }
            else
            {
                reminders = reminders.OrderBy(s => s.dateTime).ThenByDescending(s => s.ClientId).ThenBy(s => s.Id);
            }
        }

        if (!string.IsNullOrWhiteSpace(sort) && sort.Contains("desc"))
        {
            reminders.Reverse();
        }

        var result = reminders
            .Skip(offset ?? 0)
            .Take(limit ?? 10)
            .ToArray();

        return Ok(result);
    }

    /// <summary>
    /// Create an Appointment Reminder
    /// </summary>
    /// <returns>The created Appointment Reminder</returns>
    /// <response code="200">The created Appointment Reminder</response>
    /// <response code="400">The given (ClientId || LogId) does not exist</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AptReminder))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPost]
    public async Task<IActionResult> CreateReminder([FromBody] AptReminder newReminder)
    {

        var existingClient = _userManager.FindByIdAsync(newReminder.ClientId);
        if (existingClient == null)
        {
            return BadRequest("Client does not exist");
        }

        if (!string.IsNullOrWhiteSpace(newReminder.previousAppointmentId))
        {
            var existingLog = _context.Logs.Find(newReminder.previousAppointmentId);

            if (existingLog == null)
            {
                return BadRequest("Log does not exist");
            }
        }

        await _context.Reminders.AddAsync(newReminder);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateReminder), newReminder);
    }

    /// <summary>
    /// Update the Appointment Reminder with the given Id
    /// </summary>
    /// <returns>The updated Appointment Reminder</returns>
    /// <response code="200">The updated Appointment Reminder</response>
    /// <response code="400">There was no AptReminder with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptReminder))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPut]
    public async Task<IActionResult> UpdateReminder([FromBody] AptReminder upAppointment)
    {

        var existingReminder = _context.Reminders.Find(upAppointment.Id);
        if (existingReminder == null)
        {
            return BadRequest("Reminder does not exist");
        }

        _context.Reminders.Update(upAppointment);
        await _context.SaveChangesAsync();

        return Ok(upAppointment);
    }

    /// <summary>
    /// Deletes the Appointment Reminder with the given Id
    /// </summary>
    /// <param name="Id">The Id of the AptReminder to be deleted</param>
    /// <response code="204">No Content</response>
    /// <response code="400">There was no Reminder with the given Id</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteReminder(string Id)
    {

        var reminderToDelete = await _context.Reminders.FindAsync(Id);
        if (reminderToDelete == null)
        {
            return BadRequest("Reminder does not exist");
        }

        _context.Reminders.Remove(reminderToDelete);
        return NoContent();
    }
}