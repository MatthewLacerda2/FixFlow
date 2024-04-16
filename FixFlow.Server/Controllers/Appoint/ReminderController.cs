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
[ApiController]
[Route(Common.api_route + "reminders")]
[Produces("application/json")]
public class ReminderController : ControllerBase
{

    private readonly ServerContext _context;
    private readonly UserManager<Client> _userManager;

    /// <summary>
    /// Controller class for Appointment Reminder CRUD requests
    /// </summary>
    public ReminderController(ServerContext context, UserManager<Client> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptReminder))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadReminder(string id)
    {

        var remind = await _context.Reminders.FindAsync(id);

        if (remind == null)
        {
            return NotFound();
        }

        return Ok(remind);
    }

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

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpDelete("{id}")]
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