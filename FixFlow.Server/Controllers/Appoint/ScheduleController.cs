using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using Server.Models;
using Server.Models.Utils;
using Server.Models.Appointments;

namespace Server.Controllers;

/// <summary>
/// Controller class for Scheduled Appointment CRUD requests
/// </summary>
[ApiController]
[Route(Common.api_route + "schedules")]
[Produces("application/json")]
public class ScheduleController : ControllerBase
{

    private readonly IMongoCollection<AppointmentSchedule> _schedulesCollection;
    private readonly UserManager<Client> _userManager;
    private readonly IMongoCollection<AppointmentReminder> _reminderCollection;

    /// <summary>
    /// Controller class for Scheduled Appointment CRUD requests
    /// </summary>
    public ScheduleController(IMongoClient mongoClient, UserManager<Client> userManager)
    {
        _schedulesCollection = mongoClient.GetDatabase("mongo_db").GetCollection<AppointmentSchedule>("ScheduledAppointments");
        _reminderCollection = mongoClient.GetDatabase("mongo_db").GetCollection<AppointmentReminder>("AppointmentReminders");
        _userManager = userManager;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentSchedule))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadSchedule(string id)
    {
        DeleteExpiredSchedules();

        var schedule = await _schedulesCollection.FindAsync(s => s.Id == id);

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
                                        string? sort, int? offset = 0, int? limit = 10)
    {
        DeleteExpiredSchedules();

        var filterBuilder = Builders<AppointmentSchedule>.Filter;
        var filter = filterBuilder.Empty;

        if (!string.IsNullOrWhiteSpace(ClientId))
        {
            filter &= filterBuilder.Eq(s => s.ClientId, ClientId);
        }

        var appointments = _schedulesCollection.Find(filter);

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
            var existingReminder = _reminderCollection.Find(r => r.Id == newAppointment.reminderId);

            if (existingReminder == null)
            {
                return BadRequest("Reminder does not exist");
            }
        }

        await _schedulesCollection.InsertOneAsync(newAppointment);

        return CreatedAtAction(nameof(CreateSchedule), newAppointment);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentSchedule))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPut]
    public async Task<IActionResult> UpdateSchedule([FromBody] AppointmentSchedule upAppointment)
    {

        var existingAppointment = _schedulesCollection.Find(s => s.Id == upAppointment.Id).FirstOrDefault();
        if (existingAppointment == null)
        {
            return BadRequest("Schedule does not exist");
        }

        await _schedulesCollection.ReplaceOneAsync(s => s.Id == upAppointment.Id, upAppointment);

        return Ok(upAppointment);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSchedule(string id)
    {

        DeleteExpiredSchedules();

        var scheduleToDelete = _schedulesCollection.Find(s => s.Id == id).FirstOrDefault();
        if (scheduleToDelete == null)
        {
            return BadRequest("Schedule Appointment does not exist");
        }

        await _schedulesCollection.DeleteOneAsync(s => s.Id == id);
        return NoContent();
    }

    public async void DeleteExpiredSchedules()
    {
        var cutoff = DateTime.UtcNow.AddHours(-12);
        await _schedulesCollection.DeleteManyAsync(s => s.DateTime < cutoff);
    }
}