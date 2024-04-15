using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using Server.Models;
using Server.Models.Utils;
using Server.Models.Appointments;

namespace Server.Controllers;

/// <summary>
/// Controller class for Appointment Log CRUD requests
/// </summary>
[ApiController]
[Route(Common.api_route + "logs")]
[Produces("application/json")]
public class LogController : ControllerBase
{

    private readonly IMongoCollection<AppointmentLog> _logsCollection;
    private readonly UserManager<Client> _userManager;

    /// <summary>
    /// Controller class for Appointment Log CRUD requests
    /// </summary>
    public LogController(IMongoClient mongoClient, UserManager<Client> userManager)
    {
        _logsCollection = mongoClient.GetDatabase("mongo_db").GetCollection<AppointmentLog>("AppointmentLogs");
        _userManager = userManager;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentLog))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadLog(string id)
    {

        var log = await _logsCollection.FindAsync(s => s.Id == id);

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
                                    string? sort, int? offset = 0, int? limit = 10)
    {

        var filterBuilder = Builders<AppointmentLog>.Filter;
        var filter = filterBuilder.Empty;

        if (!string.IsNullOrWhiteSpace(ClientId))
        {
            filter &= filterBuilder.Eq(s => s.ClientId, ClientId);
        }

        if (minPrice.HasValue)
        {
            filter &= filterBuilder.Gte(s => s.Price, minPrice);
        }
        if (maxPrice.HasValue)
        {
            filter &= filterBuilder.Lte(s => s.Price, maxPrice);
        }

        if (minDate.HasValue)
        {
            filter &= filterBuilder.Gte(s => s.DateTime, minDate);
        }
        if (maxDate.HasValue)
        {
            filter &= filterBuilder.Lte(s => s.DateTime, maxDate);
        }

        if (status.HasValue)
        {
            filter &= filterBuilder.Eq(s => s.Status, status);
        }

        var appointments = _logsCollection.Find(filter);

        if (!string.IsNullOrWhiteSpace(sort))
        {
            sort = sort.ToLower();
            if (sort.Contains("client"))
            {
                appointments = appointments.SortBy(s => s.ClientId).ThenBy(s => s.DateTime).ThenBy(s => s.ClientId).ThenBy(s => s.Id);
            }
            else if (sort.Contains("price"))
            {
                appointments = appointments.SortBy(s => s.Price).ThenBy(s => s.DateTime).ThenBy(s => s.ClientId).ThenBy(s => s.Id);
            }
            else if (sort.Contains("date"))
            {
                appointments = appointments.SortBy(s => s.DateTime).ThenBy(s => s.ClientId).ThenBy(s => s.Id);
            }
            else
            {
                return BadRequest("Bad 'Sort' string");
            }
        }

        var result = appointments
            .Skip(offset)
            .Limit(limit)
            .ToList()
            .ToArray();

        if (!string.IsNullOrWhiteSpace(sort) && sort.Contains("desc"))
        {
            result.Reverse();
        }

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
            var existingSchedule = _logsCollection.Find(newAppointment.ScheduleId);

            if (existingSchedule == null)
            {
                return BadRequest("Schedule does not exist");
            }
        }

        await _logsCollection.InsertOneAsync(newAppointment);

        return CreatedAtAction(nameof(CreateLog), newAppointment);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentLog))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPut]
    public async Task<IActionResult> UpdateLog([FromBody] AppointmentLog upAppointment)
    {

        var existingLog = _logsCollection.Find(s => s.Id == upAppointment.Id).FirstOrDefault();
        if (existingLog == null)
        {
            return BadRequest("Log does not exist");
        }

        await _logsCollection.ReplaceOneAsync(s => s.Id == upAppointment.Id, upAppointment);

        return Ok(upAppointment);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLog(string id)
    {

        var logToDelete = _logsCollection.Find(s => s.Id == id).FirstOrDefault();
        if (logToDelete == null)
        {
            return BadRequest("Log Appointment does not exist");
        }

        await _logsCollection.DeleteOneAsync(s => s.Id == id);
        return NoContent();
    }
}