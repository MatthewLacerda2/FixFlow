using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Server.Models;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace webserver.Controllers;

/// <summary>
/// Controller class for Appointment Log CRUD requests
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/logs")]
[Produces("application/json")]
public class LogController : ControllerBase {

    private readonly IMongoCollection<AppointmentLog> _appointmentsCollection;
    private readonly UserManager<Client> _userManager;

    /// <summary>
    /// Controller class for Appointment Log CRUD requests
    /// </summary>
    public LogController(UserManager<Client> userManager, IMongoClient mongoClient) {
        _userManager = userManager;
        _appointmentsCollection = mongoClient.GetDatabase("mongo_db").GetCollection<AppointmentLog>("AppointmentLogs");
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentLog))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadLog(Guid id) {

        var log = await _appointmentsCollection.FindAsync(s => s.Id == id);

        if(log==null) {
            return NotFound();
        }

        var response = JsonConvert.SerializeObject(log);
        return Ok(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AppointmentLog>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet]
    public IActionResult ReadLogs( string? clientId, string? attendantId, [FromQuery] DateTime? fromDate, string? sort, int offset = 0, int limit = 20) {

        var filterBuilder = Builders<AppointmentLog>.Filter;
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
                appointments = appointments.SortBy(s => s.clientId).ThenBy(s => s.AttendantId).ThenBy(s => s.dateTime);
            }else if(sort=="attendant"){
                appointments = appointments.SortBy(s => s.AttendantId).ThenBy(s => s.clientId).ThenBy(s => s.dateTime);
            }else{
                appointments = appointments.SortBy(s => s.dateTime);
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
    
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AppointmentLog))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPost]
    public async Task<IActionResult> CreateLog([FromBody] AppointmentLog newAppointment) {

        var existingClient = await _userManager.FindByIdAsync(newAppointment.clientId);
        if(existingClient==null){
            return BadRequest("Client does not exist");
        }

        newAppointment.Id = Guid.NewGuid();

        _appointmentsCollection.InsertOne(newAppointment);

        return CreatedAtAction(nameof(CreateLog), newAppointment);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentLog))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPut]
    public async Task<IActionResult> UpdateLog([FromBody] AppointmentLog upAppointment) {
        
        var existingLog = await _appointmentsCollection.Find(s => s.Id == upAppointment.Id).FirstOrDefaultAsync();
        if (existingLog == null) {
            return NotFound("Log not found");
        }
        
        await _appointmentsCollection.ReplaceOneAsync(s => s.Id == upAppointment.Id, upAppointment);

        return Ok(upAppointment);
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Secretary))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [HttpDelete]
    public async Task<IActionResult> DeleteLog(Guid id) {

        var logToDelete = await _appointmentsCollection.Find(s => s.Id == id).FirstOrDefaultAsync();
        if (logToDelete == null) {
            return BadRequest("Log Appointment does not exist");
        }
        
        await _appointmentsCollection.DeleteOneAsync(s => s.Id == id);
        return NoContent();
    }    
}