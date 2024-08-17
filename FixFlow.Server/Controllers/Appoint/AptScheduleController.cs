using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Server.Models;
using Server.Models.Utils;
using Server.Models.Appointments;
using Server.Data;
using Server.Models.Filters;

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
public class AptScheduleController : ControllerBase
{

    private readonly ServerContext _context;
    private readonly UserManager<Client> _userManager;

    public AptScheduleController(ServerContext context, UserManager<Client> userManager)
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
            return NotFound("Schedule does not exist");
        }

        return Ok(schedule);
    }

    /// <summary>
    /// Gets a number of Appointment Schedules, with optional filters
    /// </summary>
    /// <remarks>
    /// Does not return Not Found, but an Array of size 0 instead
    /// </remarks>
    /// <param name="filter">The Filter Properties of the Query</param>
    /// <returns>AptSchedule[]</returns>
    /// <response code="200">Returns an array of AppointmentSchedule</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AptSchedule[]>))]
    [HttpGet]
    public IActionResult ReadSchedules([FromBody] AptScheduleFilter filter)
    {
        var schedulesQuery = _context.Schedules.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.clientId))
        {
            schedulesQuery = schedulesQuery.Where(x => x.clientId == filter.clientId);
        }

        if (!string.IsNullOrWhiteSpace(filter.businessId))
        {
            schedulesQuery = schedulesQuery.Where(x => x.businessId == filter.businessId);
        }

        schedulesQuery = schedulesQuery.Where(x => x.price >= filter.minPrice);
        schedulesQuery = schedulesQuery.Where(x => x.price <= filter.maxPrice);
        
        schedulesQuery = schedulesQuery.Where(x => x.dateTime >= new DateTime(filter.minDateTime, new TimeOnly(0)));
        schedulesQuery = schedulesQuery.Where(x => x.dateTime <= new DateTime(filter.maxDateTime, new TimeOnly(0)));

        switch (filter.sort)
        {
            case ScheduleSort.ClientId:
                schedulesQuery = schedulesQuery.OrderBy(s => s.clientId).ThenByDescending(s => s.dateTime).ThenBy(s => s.price).ThenBy(s => s.Id);
                break;
            case ScheduleSort.Date:
                schedulesQuery = schedulesQuery.OrderByDescending(s => s.dateTime).ThenBy(s => s.clientId).ThenBy(s => s.price).ThenBy(s => s.Id);
                break;
            case ScheduleSort.Price:
                schedulesQuery = schedulesQuery.OrderBy(s => s.price).ThenByDescending(s => s.dateTime).ThenBy(s => s.clientId).ThenBy(s => s.Id);
                break;
        }


        if (filter.descending)
        {
            schedulesQuery.Reverse();
        }

        var resultsArray = schedulesQuery
            .Skip(filter.offset)
            .Take(filter.limit)
            .ToArray();

        return Ok(resultsArray);
    }

    /// <summary>
    /// Create an Appointment Schedule
    /// </summary>
    /// <returns>AptSchedule</returns>
    /// <response code="200">The created Appointment Schedule</response>
    /// <response code="400">The given (ClientId || ContactId) does not exist</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AptSchedule))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPost]
    public async Task<IActionResult> CreateSchedule([FromBody] AptSchedule newAppointment)
    {

        var existingClient = _userManager.FindByIdAsync(newAppointment.clientId);
        if (existingClient == null)
        {
            return BadRequest("Client does not exist");
        }

        var existingBusiness = _context.Business.Find(newAppointment.businessId);
        if (existingBusiness == null)
        {
            return BadRequest("Business does not exist");
        }

        if (!string.IsNullOrWhiteSpace(newAppointment.contactId))
        {
            var existingContact = _context.Contacts.Find(newAppointment.contactId);

            if (existingContact == null)
            {
                return BadRequest("Contact reminder does not exist");
            }
        }

        _context.Schedules.Add(newAppointment);
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
        var scheduleToDelete = _context.Schedules.Find(Id);
        if (scheduleToDelete == null)
        {
            return BadRequest("Schedule Appointment does not exist");
        }

        _context.Schedules.Remove(scheduleToDelete);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}