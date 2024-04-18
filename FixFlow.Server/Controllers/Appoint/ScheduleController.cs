using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Server.Models;
using Server.Models.Utils;
using Server.Models.Appointments;
using Server.Data;

namespace Server.Controllers;

/// <summary>
/// Controller class for Scheduled Appointment CRUD requests
/// </summary>
/// <remarks>
/// Schedules are simply the setup of an Appointment, not the Appointment itself
/// </remarks>
[ApiController]
[Route(Common.api_route + "schedules")]
[Produces("application/json")]
public class ScheduleController : ControllerBase
{

    private readonly ServerContext _context;
    private readonly UserManager<Client> _userManager;

    public ScheduleController(ServerContext context, UserManager<Client> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    /// <summary>
    /// Get the Schedule with the given Id
    /// </summary>
    /// <param name="Id">The Schedule's Id</param>
    /// <returns>AptSchedule</returns>
    /// <response code="200">The AppointmentSchedule with the given Id</response>
    /// <response code="404">There was no Appointment Schedule with the given Id</response>/// 
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptSchedule))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{Id}")]
    public async Task<IActionResult> ReadSchedule(string Id)
    {
        var schedule = await _context.Schedules.FindAsync(Id);

        if (schedule == null)
        {
            return NotFound();
        }

        return Ok(schedule);
    }

    /// <summary>
    /// Gets a number of Appointment Schedules, with optional filters
    /// </summary>
    /// <remarks>
    /// Does not return Not Found, but an Array of size 0 instead
    /// </remarks>
    /// <param name="ClientId">Filter by a specific Client</param>
    /// <param name="minPrice">Minimum Price of the Appointments</param>
    /// <param name="maxPrice">Maximum Price of the Appointments</param>/// 
    /// <param name="minDateTime">The nearest Reminder set up</param>
    /// <param name="maxDateTime">The furthest Reminder set up</param>/// 
    /// <param name="sort">Orders the result by Client, Price or DateTime. Add suffix 'desc' to order descending</param>
    /// <param name="offset">Offsets the result by a given amount</param>
    /// <param name="limit">Limits the result by a given amount</param>
    /// <returns>AptSchedule[]</returns>
    /// <response code="200">Returns an array of AppointmentSchedule</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AptSchedule[]>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpGet]
    public IActionResult ReadSchedules(string? ClientId, float? minPrice, float? maxPrice,
                                        DateTime? minDateTime, DateTime? maxDateTime,
                                        string? sort, int? offset = 0, int? limit = 10)
    {
        var schedulesQuery = _context.Schedules.AsQueryable();

        if (!string.IsNullOrWhiteSpace(ClientId))
        {
            schedulesQuery = schedulesQuery.Where(x => x.ClientId == ClientId);
        }

        if (minPrice.HasValue)
        {
            schedulesQuery = schedulesQuery.Where(x => x.Price >= minPrice);
        }

        if (maxPrice.HasValue)
        {
            schedulesQuery = schedulesQuery.Where(x => x.Price <= maxPrice);
        }

        if (minDateTime.HasValue)
        {
            schedulesQuery = schedulesQuery.Where(x => x.DateTime >= minDateTime);
        }

        if (maxDateTime.HasValue)
        {
            schedulesQuery = schedulesQuery.Where(x => x.DateTime <= maxDateTime);
        }


        if (!string.IsNullOrWhiteSpace(sort))
        {
            sort = sort.ToLower();
            if (sort.Contains("client"))
            {
                schedulesQuery = schedulesQuery.OrderBy(s => s.ClientId).ThenByDescending(s => s.DateTime).ThenBy(s => s.Id);
            }
            else if (sort.Contains("price"))
            {
                schedulesQuery = schedulesQuery.OrderBy(s => s.Price).ThenByDescending(s => s.DateTime).ThenBy(s => s.ClientId).ThenBy(s => s.Id);
            }
            else if (sort.Contains("date"))
            {
                schedulesQuery = schedulesQuery.OrderByDescending(s => s.DateTime).ThenBy(s => s.ClientId).ThenBy(s => s.Id);
            }
        }

        if (!string.IsNullOrWhiteSpace(sort) && sort.Contains("desc"))
        {
            schedulesQuery.Reverse();
        }

        var resultsArray = schedulesQuery
            .Skip(offset ?? 0)
            .Take(limit ?? 10)
            .ToArray();

        return Ok(resultsArray);
    }

    /// <summary>
    /// Create an Appointment Schedule
    /// </summary>
    /// <returns>AptSchedule</returns>
    /// <response code="200">The created Appointment Schedule</response>
    /// <response code="400">The given (ClientId || ReminderId) does not exist</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AptSchedule))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPost]
    public async Task<IActionResult> CreateSchedule([FromBody] AptSchedule newAppointment)
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

    /// <summary>
    /// Update the Appointment Schedule with the given Id
    /// </summary>
    /// <returns>AptSchedule</returns>
    /// <response code="200">The updated Appointment Schedule</response>
    /// <response code="400">There was no AptSchedule with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptSchedule))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPut]
    public async Task<IActionResult> UpdateSchedule([FromBody] AptSchedule upAppointment)
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

    /// <summary>
    /// Deletes the Appointment Schedule with the given Id
    /// </summary>
    /// <param name="Id">The Id of the AptSchedule to be deleted</param>
    /// <returns>NoContentResult</returns>
    /// <response code="204">No Content</response>
    /// <response code="400">There was no Schedule with the given Id</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteSchedule(string Id)
    {
        var scheduleToDelete = await _context.Schedules.FindAsync(Id);
        if (scheduleToDelete == null)
        {
            return BadRequest("Schedule Appointment does not exist");
        }

        _context.Schedules.Remove(scheduleToDelete);
        return NoContent();
    }
}