using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace webserver.Controllers;

/// <summary>
/// Controller class for Scheduled Appointment CRUD requests
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/schedules")]
[Produces("application/json")]
public class ScheduleController : ControllerBase {

    private readonly IMongoCollection<ScheduledAppointment> _appointmentsCollection;

    /// <summary>
    /// Controller class for Scheduled Appointment CRUD requests
    /// </summary>
    public ScheduleController(IMongoClient mongoClient) {
        var database = mongoClient.GetDatabase("MongoDbConnection");
        _appointmentsCollection = database.GetCollection<ScheduledAppointment>("ScheduledAppointments");
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduledAppointment))]
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

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ScheduledAppointment>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet]
    public IActionResult GetAppointments( string? clientId, string? attendantId, [FromQuery] DateTime? fromDate, string? sort, int offset = 0, int limit = 20) {
        
        var filterBuilder = Builders<ScheduledAppointment>.Filter;
        var filter = filterBuilder.Empty;

        if (!string.IsNullOrEmpty(clientId)) {
            filter &= filterBuilder.Eq(s => s.clientId, clientId);
        }
        if (!string.IsNullOrEmpty(attendantId)) {
            filter &= filterBuilder.Eq(s => s.AttendantId, attendantId);
        }
        if (fromDate.HasValue) {
            filter &= filterBuilder.Gte(s => s.dateTime, fromDate.Value);
        }

        var appointments = _appointmentsCollection.Find(filter);

        if (!string.IsNullOrEmpty(sort)) {
            if(sort=="client"){
                appointments.SortBy(s=>s.clientId).ThenBy(s=>s.AttendantId).ThenBy(s=>s.dateTime);
            }else if(sort=="attendant"){
                appointments.SortBy(s=>s.AttendantId).ThenBy(s=>s.clientId).ThenBy(s=>s.dateTime);
            }else{
                appointments.SortBy(s=>s.dateTime);
            }
        }

        appointments = appointments
            .Skip(offset)
            .Limit(limit)
            .ToList()
            .ToArray();

        if (appointments.Length == 0) {
            return NotFound();
        }

        return Ok(JsonConvert.SerializeObject(appointments));
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Secretary))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [HttpPost]
    public async Task<IActionResult> CreateSchedule([FromBody] SecretaryDTO SecretaryDto, string password) {

        var existingName = _context.Secretarys.Where(c=>c.FullName == SecretaryDto.FullName);
        if(existingName != null){
            return BadRequest("FullName already registered!");
        }

        Secretary Secretary = new Secretary(SecretaryDto.FullName, SecretaryDto.Email, SecretaryDto.CPF, SecretaryDto.PhoneNumber, SecretaryDto.salary, SecretaryDto.shift);

        var result = await _userManager.CreateAsync(Secretary, password);

        if(!result.Succeeded){
            return StatusCode(500, "Internal Server Error: Register Secretary Unsuccessful");
        }

        return CreatedAtAction(nameof(CreateSchedule), (SecretaryDTO)Secretary);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Secretary))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [HttpPatch]
    public async Task<IActionResult> UpdateSchedule([FromBody] SecretaryDTO upSecretary) {

        var existingSecretary = _context.Secretarys.Find(upSecretary.Id);
        if (existingSecretary==null) {
            return BadRequest("Secretary does not Exist!");
        }

        existingSecretary = (Secretary)upSecretary;
        
        await _context.SaveChangesAsync();

        var response = JsonConvert.SerializeObject((SecretaryDTO)existingSecretary);

        return Ok(response);
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Secretary))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSchedule(string id) {

        var Secretary = _context.Secretarys.Find(id);
        if(Secretary == null){
            return BadRequest("Secretary does not Exist!");
        }
        
        _context.Secretarys.Remove(Secretary);

        await _context.SaveChangesAsync();

        return NoContent();
    }    
}