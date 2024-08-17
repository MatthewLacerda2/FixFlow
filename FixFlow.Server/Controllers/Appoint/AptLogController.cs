using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Server.Models;
using Server.Models.Utils;
using Server.Models.Appointments;
using Server.Data;
using Server.Models.Filters;

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
public class AptLogController : ControllerBase
{

    private readonly ServerContext _context;
    private readonly UserManager<Client> _userManager;

    public AptLogController(ServerContext context, UserManager<Client> userManager)
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
            return NotFound("Log does not exist");
        }

        return Ok(log);
    }

    /// <summary>
    /// Gets a number of Appointment Logs, with optional filters
    /// </summary>
    /// <remarks>
    /// Does not return Not Found, but an Array of size 0 instead
    /// </remarks>
    /// <param name="filter">The Filter Properties of the Query</param>
    /// <returns>AptLog[]</returns>
    /// <response code="200">Returns an array of AppointmentLog</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AptLog[]>))]
    [HttpGet]
    public IActionResult ReadLogs([FromBody] AptLogFilter filter)
    {

        var logsQuery = _context.Logs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.clientId))
        {
            logsQuery = logsQuery.Where(x => x.clientId == filter.clientId);
        }

        if (!string.IsNullOrWhiteSpace(filter.businessId))
        {
            logsQuery = logsQuery.Where(x => x.businessId == filter.businessId);
        }

        logsQuery = logsQuery.Where(x => x.price >= filter.minPrice);
        logsQuery = logsQuery.Where(x => x.price <= filter.maxPrice);
        
        logsQuery = logsQuery.Where(x => x.dateTime.Date >= filter.minDateTime.ToDateTime(TimeOnly.MinValue).Date);
        logsQuery = logsQuery.Where(x => x.dateTime.Date <= filter.maxDateTime.ToDateTime(TimeOnly.MaxValue).Date);

        switch (filter.sort)
        {
            case LogSort.ClientId:
                logsQuery = logsQuery.OrderBy(s => s.clientId).ThenByDescending(s => s.dateTime).ThenByDescending(s => s.price).ThenBy(s => s.Id);
                break;
            case LogSort.BusinessId:
                logsQuery = logsQuery.OrderBy(s => s.businessId).ThenBy(s => s.clientId).ThenByDescending(s => s.dateTime).ThenBy(s => s.Id);
                break;
            case LogSort.Date:
                logsQuery = logsQuery.OrderByDescending(s => s.dateTime).ThenBy(s => s.businessId).ThenBy(s => s.clientId).ThenBy(s => s.Id);
                break;
            case LogSort.Price:
                logsQuery = logsQuery.OrderByDescending(s => s.price).ThenBy(s => s.businessId).ThenBy(s => s.clientId).ThenBy(s => s.Id);
                break;
        }

        if (filter.descending)
        {
            logsQuery.Order();
        }

        var resultsArray = logsQuery
            .Skip(filter.offset)
            .Take(filter.limit)
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
    public async Task<IActionResult> CreateLog([FromBody] AptLog newLog)
    {

        if (!string.IsNullOrWhiteSpace(newLog.scheduleId))
        {
            var existingSchedule = _context.Schedules.Find(newLog.scheduleId);

            if (existingSchedule == null)
            {
                return BadRequest("Schedule does not exist");
            }
        }

        _context.Logs.Add(newLog);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateLog), newLog);
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
    public async Task<IActionResult> UpdateLog([FromBody] AptLog upLog)
    {

        var existingLog = _context.Logs.Find(upLog.Id);
        if (existingLog == null)
        {
            return BadRequest("Log does not exist");
        }

        if (!string.IsNullOrWhiteSpace(upLog.scheduleId))
        {
            var existingSchedule = _context.Schedules.Find(upLog.scheduleId);

            if (existingSchedule == null)
            {
                return BadRequest("Schedule does not exist");
            }
        }

        _context.Logs.Update(upLog);
        await _context.SaveChangesAsync();

        return Ok(upLog);
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
            return BadRequest("Log does not exist");
        }

        _context.Logs.Remove(logToDelete);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}