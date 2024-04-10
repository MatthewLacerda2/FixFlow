using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Server.Models;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace webserver.Controllers;

/// <summary>
/// Controller class for Scheduled Appointment CRUD requests
/// </summary>
[ApiController]
[Route("api/v1/schedules")]
[Produces("application/json")]
public class ScheduleController : ControllerBase {

    private readonly IMongoCollection<AppointmentSchedule> _appointmentsCollection;
    private readonly UserManager<Client> _userManager;

    /// <summary>
    /// Controller class for Scheduled Appointment CRUD requests
    /// </summary>
    public ScheduleController(UserManager<Client> userManager, IMongoClient mongoClient) {
        _userManager = userManager;
        _appointmentsCollection = mongoClient.GetDatabase("mongo_db").GetCollection<AppointmentSchedule>("ScheduledAppointments");
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentSchedule))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadSchedule(Guid id) {

        var schedule = await _appointmentsCollection.FindAsync(s => s.Id == id);

        if(schedule==null) {
            return NotFound();
        }

        var response = JsonConvert.SerializeObject(schedule);
        return Ok(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AppointmentSchedule[]>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet]
    public IActionResult ReadSchedules( string? clientId, string? attendantId, [FromQuery] DateTime? fromDate, string? sort, int offset = 0, int limit = 20) {

        var filterBuilder = Builders<AppointmentSchedule>.Filter;
        var filter = filterBuilder.Empty;

        if (!string.IsNullOrEmpty(clientId)) {
            filter &= filterBuilder.Eq(s => s.ClientId, clientId);
        }
        if (!string.IsNullOrEmpty(attendantId)) {
            filter &= filterBuilder.Eq(s => s.AttendantId, attendantId);
        }
        if (fromDate.HasValue) {
            filter &= filterBuilder.Gte(s => s.DateTime, fromDate.Value);
        }

        var appointments = _appointmentsCollection.Find(filter);

        if (!string.IsNullOrEmpty(sort)) {
            if(sort=="client"){
                appointments = appointments.SortBy(s => s.ClientId).ThenBy(s => s.AttendantId).ThenBy(s => s.DateTime);
            }else if(sort=="attendant"){
                appointments = appointments.SortBy(s => s.AttendantId).ThenBy(s => s.ClientId).ThenBy(s => s.DateTime);
            }else{
                appointments = appointments.SortBy(s => s.DateTime);
            }
        }

        var result = appointments
            .Skip(offset)
            .Limit(limit)
            .ToList()
            .ToArray();

        if (result.Length == 0) {
            return NotFound();
        }

        return Ok(JsonConvert.SerializeObject(result));
    }
    
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AppointmentSchedule))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPost]
    public async Task<IActionResult> CreateSchedule([FromBody] AppointmentSchedule newAppointment) {

        await _appointmentsCollection.InsertOneAsync(newAppointment);

        return CreatedAtAction(nameof(CreateSchedule), newAppointment);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentSchedule))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPut]
    public async Task<IActionResult> UpdateSchedule([FromBody] AppointmentSchedule upAppointment) {
        
        var existingAppointment = await _appointmentsCollection.Find(s => s.Id == upAppointment.Id).FirstOrDefaultAsync();
        if (existingAppointment == null) {
            return NotFound("Schedule not found");
        }
        
        await _appointmentsCollection.ReplaceOneAsync(s => s.Id == upAppointment.Id, upAppointment);

        return Ok(upAppointment);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSchedule(Guid id) {

        var scheduleToDelete = await _appointmentsCollection.Find(s => s.Id == id).FirstOrDefaultAsync();
        if (scheduleToDelete == null) {
            return BadRequest("Schedule Appointment does not exist");
        }
        
        await _appointmentsCollection.DeleteOneAsync(s => s.Id == id);
        return NoContent();
    }    
}