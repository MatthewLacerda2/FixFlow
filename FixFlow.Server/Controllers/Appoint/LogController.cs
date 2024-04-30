using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Server.Models;
using Server.Models.Utils;
using Server.Models.Appointments;
using Server.Data;

namespace Server.Controllers;

/// <summary>
/// Controller class for Appointment Log CRUD requests
/// </summary>
/// <remarks>
/// Logs are simply registration that the Appointment was done
/// </remarks>
[ApiController]
[Route(Common.api_route + "logs")]
[Produces("application/json")]
public class LogController : ControllerBase
{

    private readonly ServerContext _context;
    private readonly UserManager<Client> _userManager;

    public LogController(ServerContext context, UserManager<Client> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    /// <summary>
    /// Get the Log with the given Id
    /// </summary>
    /// <param name="Id">The Log's Id</param>
    /// <returns>AptLog</returns>
    /// <response code="200">The Appointment Log</response>
    /// <response code="404">There was no Appointment Log with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptLog))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet("{Id}")]
    public async Task<IActionResult> ReadLog(string Id)
    {

        var log = await _context.Logs.FindAsync(Id);

        if (log == null)
        {
            return NotFound();
        }

        return Ok(log);
    }

    /// <summary>
    /// Gets a number of Appointment Logs, with optional filters
    /// </summary>
    /// <remarks>
    /// Does not return Not Found, but an Array of size 0 instead
    /// </remarks>
    /// <param name="ClientId">Filter by a specific Client</param>
    /// <param name="minPrice">Minimum Price of the Appointments</param>
    /// <param name="maxPrice">Maximum Price of the Appointments</param>/// 
    /// <param name="minDateTime">The oldest DateTime the Appointment took place</param>
    /// <param name="maxDateTime">The most recent DateTime the Appointment took placet</param>/// 
    /// <param name="sort">Orders the result by Client, Price or DateTime. Add suffix 'desc' to order descending</param>
    /// <param name="offset">Offsets the result by a given amount</param>
    /// <param name="limit">Limits the result by a given amount</param>
    /// <returns>AptLog[]</returns>
    /// <response code="200">Returns an array of AppointmentLog</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AptLog[]>))]
    [HttpGet]
    public IActionResult ReadLogs(string? ClientId, float? minPrice, float? maxPrice,
                                    DateTime? minDateTime, DateTime? maxDateTime,
                                    string? sort, int? offset, int? limit)
    {

        var logsQuery = _context.Logs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(ClientId))
        {
            logsQuery = logsQuery.Where(x => x.ClientId == ClientId);
        }

        if (minPrice.HasValue)
        {
            logsQuery = logsQuery.Where(x => x.price >= minPrice);
        }
        if (maxPrice.HasValue)
        {
            logsQuery = logsQuery.Where(x => x.price <= maxPrice);
        }

        if (minDateTime.HasValue)
        {
            logsQuery = logsQuery.Where(x => x.dateTime >= minDateTime);
        }

        if (maxDateTime.HasValue)
        {
            logsQuery = logsQuery.Where(x => x.dateTime <= maxDateTime);
        }

        if (!string.IsNullOrWhiteSpace(sort))
        {
            sort = sort.ToLower();
            if (sort.Contains("client"))
            {
                logsQuery = logsQuery.OrderBy(s => s.ClientId).ThenByDescending(s => s.dateTime).ThenBy(s => s.Id);
            }
            else if (sort.Contains("price"))
            {
                logsQuery = logsQuery.OrderBy(s => s.price).ThenByDescending(s => s.dateTime).ThenBy(s => s.Id);
            }
            else if (sort.Contains("date"))
            {
                logsQuery = logsQuery.OrderByDescending(s => s.dateTime).ThenBy(s => s.ClientId).ThenBy(s => s.Id);
            }
        }

        if (!string.IsNullOrWhiteSpace(sort) && sort.Contains("desc"))
        {
            logsQuery.Reverse();
        }

        var resultsArray = logsQuery
            .Skip(offset ?? 0)
            .Take(limit ?? 10)
            .ToArray();

        return Ok(resultsArray);
    }

    /// <summary>
    /// Create an Appointment Log
    /// </summary>
    /// <returns>AptLog</returns>
    /// <response code="200">The created Appointment Log</response>
    /// <response code="400">The given (ClientId || ScheduleId) does not exist</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AptLog))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPost]
    public async Task<IActionResult> CreateLog([FromBody] AptLog newAppointment)
    {

        var existingClient = _userManager.FindByIdAsync(newAppointment.ClientId);
        if (existingClient == null)
        {
            return BadRequest("Client does not exist");
        }

        if (!string.IsNullOrWhiteSpace(newAppointment.scheduleId))
        {
            var existingSchedule = _context.Schedules.Find(newAppointment.scheduleId);

            if (existingSchedule == null)
            {
                return BadRequest("Schedule does not exist");
            }
        }

        await _context.Logs.AddAsync(newAppointment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateLog), newAppointment);
    }

    /// <summary>
    /// Update the Appointment Log with the given Id
    /// </summary>
    /// <returns>AptLog</returns>
    /// <response code="200">The updated Appointment Log</response>
    /// <response code="400">There was no AptLog with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptLog))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPut]
    public async Task<IActionResult> UpdateLog([FromBody] AptLog upAppointment)
    {

        var existingLog = _context.Logs.Find(upAppointment.Id);
        if (existingLog == null)
        {
            return BadRequest("Log does not exist");
        }

        _context.Logs.Update(upAppointment);
        await _context.SaveChangesAsync();

        return Ok(upAppointment);
    }

    /// <summary>
    /// Deletes the Appointment Log with the given Id
    /// </summary>
    /// <param name="Id">The Id of the AptLog to be deleted</param>
    /// <returns>NoContentResult</returns>
    /// <response code="204">No Content</response>
    /// <response code="400">There was no Log with the given Id</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteLog(string Id)
    {

        var logToDelete = _context.Logs.Find(Id);
        if (logToDelete == null)
        {
            return BadRequest("Log Appointment does not exist");
        }

        _context.Logs.Remove(logToDelete);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}