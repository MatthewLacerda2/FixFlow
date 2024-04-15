using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Server.Models;
using Server.Models.Utils;
using Server.Models.Appointments;
using Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

/// <summary>
/// Controller class for Scheduled Appointment CRUD requests
/// </summary>
[ApiController]
[Route(Common.api_route + "schedules")]
[Produces("application/json")]
public class ScheduleController : ControllerBase
{

    private readonly ServerContext _context;
    private readonly UserManager<Client> _userManager;

    /// <summary>
    /// Controller class for Scheduled Appointment CRUD requests
    /// </summary>
    public ScheduleController(ServerContext context, UserManager<Client> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentSchedule))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadSchedule(string id)
    {
        var schedule = await _context.Schedules.FindAsync(id);

        if (schedule == null)
        {
            return NotFound();
        }

        return Ok(schedule);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AppointmentSchedule[]>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpGet]
    public IActionResult ReadSchedules(string? ClientId, float? minPrice, float? maxPrice,
                                        DateTime? minDateTime, DateTime? maxDateTime,
                                        string? sort, int? offset = 0, int? limit = 10)
    {
        var schedules = _context.Schedules.AsQueryable();

        if (!string.IsNullOrWhiteSpace(ClientId))
        {
            schedules = schedules.Where(x => x.ClientId == ClientId);
        }

        if (minPrice.HasValue)
        {
            schedules = schedules.Where(x => x.Price >= minPrice);
        }

        if (maxPrice.HasValue)
        {
            schedules = schedules.Where(x => x.Price <= maxPrice);
        }

        if (minDateTime.HasValue)
        {
            schedules = schedules.Where(x => x.DateTime >= minDateTime);
        }

        if (maxDateTime.HasValue)
        {
            schedules = schedules.Where(x => x.DateTime <= maxDateTime);
        }


        if (!string.IsNullOrWhiteSpace(sort))
        {
            sort = sort.ToLower();
            if (sort.Contains("client"))
            {
                schedules = schedules.OrderBy(s => s.ClientId).ThenByDescending(s => s.DateTime).ThenBy(s => s.Id);
            }
            else if (sort.Contains("price"))
            {
                schedules = schedules.OrderBy(s => s.Price).ThenByDescending(s => s.DateTime).ThenBy(s => s.ClientId).ThenBy(s => s.Id);
            }
            else if (sort.Contains("date"))
            {
                schedules = schedules.OrderByDescending(s => s.DateTime).ThenBy(s => s.ClientId).ThenBy(s => s.Id);
            }
        }

        if (!string.IsNullOrWhiteSpace(sort) && sort.Contains("desc"))
        {
            schedules.Reverse();
        }

        var result = schedules
            .Skip(offset ?? 0)
            .Take(limit ?? 10)
            .ToList()
            .ToArray();

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AppointmentSchedule))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPost]
    public async Task<IActionResult> CreateSchedule([FromBody] AppointmentSchedule newAppointment)
    {

        var existingClient = _userManager.FindByIdAsync(newAppointment.ClientId);
        if (existingClient == null)
        {
            return BadRequest("Client does not exist");
        }

        if (!string.IsNullOrWhiteSpace(newAppointment.reminderId))
        {
            var existingReminder = _context.Reminders.Find(newAppointment.reminderId);

            if (existingReminder == null)
            {
                return BadRequest("Reminder does not exist");
            }
        }

        await _context.Schedules.AddAsync(newAppointment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateSchedule), newAppointment);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentSchedule))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPut]
    public async Task<IActionResult> UpdateSchedule([FromBody] AppointmentSchedule upAppointment)
    {

        var existingAppointment = _context.Schedules.Find(upAppointment.Id);
        if (existingAppointment == null)
        {
            return BadRequest("Schedule does not exist");
        }

        _context.Schedules.Update(upAppointment);
        await _context.SaveChangesAsync();

        return Ok(upAppointment);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSchedule(string id)
    {
        var scheduleToDelete = await _context.Schedules.FindAsync(id);
        if (scheduleToDelete == null)
        {
            return BadRequest("Schedule Appointment does not exist");
        }

        _context.Schedules.Remove(scheduleToDelete);
        return NoContent();
    }
}