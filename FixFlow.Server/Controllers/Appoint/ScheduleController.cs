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

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Secretary>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet]
    public async Task<IActionResult> ReadSchedules(string? username, TimeInterval? shift, int? offset, int limit, string? sort) {
        
        if (limit < 1) {
            return BadRequest("Limit parameter must be a natural number greater than 0");
        }

        var Secretarys = _context.Secretarys.AsQueryable();

        if(!string.IsNullOrEmpty(username)){
            Secretarys = Secretarys.Where(Secretary => Secretary.UserName!.Contains(username));
        }

        if(shift!=null){
            Secretarys = Secretarys.Where(Secretary => Secretary.shift.start >= shift.start);
            Secretarys = Secretarys.Where(Secretary => Secretary.shift.finish <= shift.finish);
        }

        if(!string.IsNullOrEmpty(sort)){
            sort = sort.ToLower();
            switch (sort) {
                case "name":
                    Secretarys = Secretarys.OrderBy(emp => emp.UserName);
                    break;
                case "shift":
                    Secretarys = Secretarys.OrderBy(emp => emp.shift.start).ThenBy(emp=>emp.shift.finish);
                    break;
            }
        }

        if(offset.HasValue){
            Secretarys = Secretarys.Skip(offset.Value);
        }
        Secretarys = Secretarys.Take(limit);

        var resultQuery = await Secretarys.ToArrayAsync();
        var resultsArray = resultQuery.Select(c=>(SecretaryDTO)c).ToArray();
        
        if(resultsArray.Length==0){
            return NotFound();
        }
        
        var response = JsonConvert.SerializeObject(resultsArray);

        return Ok(response);
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