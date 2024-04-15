using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Server.Models;
using Server.Models.Utils;
using Server.Models.Appointments;
using Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

/// <summary>
/// Controller class for Appointment Log CRUD requests
/// </summary>
[ApiController]
[Route(Common.api_route + "logs")]
[Produces("application/json")]
public class LogController : ControllerBase
{

    private readonly ServerContext _context;
    private readonly UserManager<Client> _userManager;

    /// <summary>
    /// Controller class for Appointment Log CRUD requests
    /// </summary>
    public LogController(ServerContext context, UserManager<Client> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentLog))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadLog(string id)
    {

        var log = await _context.Logs.FindAsync(id);

        if (log == null)
        {
            return NotFound();
        }

        return Ok(log);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AppointmentLog[]>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpGet]
    public IActionResult ReadLogs(string? ClientId, float? minPrice, float? maxPrice,
                                    DateTime? minDate, DateTime? maxDate, CompletedStatus? status,
                                    string? sort, int? offset, int? limit)
    {

        var logs = _context.Logs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(ClientId))
        {
            logs = logs.Where(x => x.ClientId == ClientId);
        }

        if (minPrice.HasValue)
        {
            logs = logs.Where(x => x.Price >= minPrice);
        }

        if (maxPrice.HasValue)
        {
            logs = logs.Where(x => x.Price <= maxPrice);
        }

        if (minDate.HasValue)
        {
            logs = logs.Where(x => x.DateTime >= minDate);
        }

        if (maxDate.HasValue)
        {
            logs = logs.Where(x => x.DateTime <= maxDate);
        }

        if (status.HasValue)
        {
            logs = logs.Where(x => x.Status == status);
        }

        if (!string.IsNullOrWhiteSpace(sort))
        {
            sort = sort.ToLower();
            if (sort.Contains("client"))
            {
                logs = logs.OrderBy(s => s.ClientId).ThenByDescending(s => s.DateTime).ThenBy(s => s.Id);
            }
            else if (sort.Contains("price"))
            {
                logs = logs.OrderBy(s => s.Price).ThenByDescending(s => s.DateTime).ThenBy(s => s.Id);
            }
            else if (sort.Contains("date"))
            {
                logs = logs.OrderByDescending(s => s.DateTime).ThenBy(s => s.ClientId).ThenBy(s => s.Id);
            }
        }

        if (!string.IsNullOrWhiteSpace(sort) && sort.Contains("desc"))
        {
            logs.Reverse();
        }

        var result = logs
            .Skip(offset ?? 0)
            .Take(limit ?? 10)
            .ToArray();

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AppointmentLog))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPost]
    public async Task<IActionResult> CreateLog([FromBody] AppointmentLog newAppointment)
    {

        var existingClient = _userManager.FindByIdAsync(newAppointment.ClientId);
        if (existingClient == null)
        {
            return BadRequest("Client does not exist");
        }

        if (!string.IsNullOrWhiteSpace(newAppointment.ScheduleId))
        {
            var existingSchedule = _context.Schedules.Find(newAppointment.ScheduleId);

            if (existingSchedule == null)
            {
                return BadRequest("Schedule does not exist");
            }
        }

        await _context.Logs.AddAsync(newAppointment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateLog), newAppointment);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentLog))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPut]
    public async Task<IActionResult> UpdateLog([FromBody] AppointmentLog upAppointment)
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

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLog(string id)
    {

        var logToDelete = await _context.Logs.FindAsync(id);
        if (logToDelete == null)
        {
            return BadRequest("Log Appointment does not exist");
        }

        _context.Logs.Remove(logToDelete);
        return NoContent();
    }
}