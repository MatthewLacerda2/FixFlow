using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Newtonsoft.Json;

namespace webserver.Controllers;

/// <summary>
/// Controller class for Employee CRUD requests
/// </summary>
[Authorize(Roles=Server.Models.Utils.Common.Secretary_Role)]
[ApiController]
[Route("api/v1/employee")]
[Produces("application/json")]
public class EmployeeController : ControllerBase {

    private readonly ServerContext _context;
    private readonly UserManager<Employee> _userManager;

    /// <summary>
    /// Controller class for Employee CRUD requests
    /// </summary>
    public EmployeeController(ServerContext context, UserManager<Employee> userManager){
        _context = context;
        _userManager = userManager;
    }

    /// <summary>
    /// Get the Employee with the given Id
    /// </summary>
    /// <returns>Employee with the given Id. NotFoundResult if there is none</returns>
    /// <response code="200">Returns the Employee's DTO</response>
    /// <response code="404">If there is none with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Client>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet("{id}")]
    public IActionResult ReadEmployee(string id) {

        var employee = _context.Employees.Find(id);
        if(employee==null){
            return NotFound();
        }

        var response = JsonConvert.SerializeObject((EmployeeDTO)employee);

        return Ok(response);
    }

    /// <summary>
    /// Get all Employees within optional filters
    /// </summary>
    /// <returns>EmployeeDTO Array</returns>
    /// <param name="username">Filters results to only Users whose username contains this string</param>
    /// <param name="offset">Offsets the result by a given amount</param>
    /// <param name="limit">Limits the number of results</param>
    /// <param name="sort">Orders the result by a given field. Does not order if the field does not exist</param>
    /// <response code="200">Returns an array of Employee DTOs</response>
    /// <response code="404">If no Employees fit the given filters</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Employee>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [HttpGet]
    public async Task<IActionResult> ReadEmployees(string? username, TimeInterval? shift, int? offset, int limit, string? sort) {
        
        if (limit < 1) {
            return BadRequest("Limit parameter must be a natural number greater than 0");
        }

        var Employees = _context.Employees.AsQueryable();

        if(!string.IsNullOrEmpty(username)){
            Employees = Employees.Where(Employee => Employee.UserName!.Contains(username));
        }

        if(shift!=null){
            Employees = Employees.Where(Employee => Employee.shift.start >= shift.start);
            Employees = Employees.Where(Employee => Employee.shift.finish <= shift.finish);
        }

        if(!string.IsNullOrEmpty(sort)){
            sort = sort.ToLower();
            switch (sort) {
                case "name":
                    Employees = Employees.OrderBy(emp => emp.UserName);
                    break;
                case "shift":
                    Employees = Employees.OrderBy(emp => emp.shift.start).ThenBy(emp=>emp.shift.finish);
                    break;
            }
        }

        if(offset.HasValue){
            Employees = Employees.Skip(offset.Value);
        }
        Employees = Employees.Take(limit);

        var resultQuery = await Employees.ToArrayAsync();
        var resultsArray = resultQuery.Select(c=>(EmployeeDTO)c).ToArray();
        
        if(resultsArray==null || resultsArray.Length==0){
            return NotFound();
        }
        
        var response = JsonConvert.SerializeObject(resultsArray);

        return Ok(response);
    }

    /// <summary>
    /// Creates a Employee User
    /// </summary>
    /// <returns>The created Employee's Data</returns>
    /// <response code="200">EmployeeDTO</response>
    /// <response code="400">Returns a string with the requirements that were not filled</response>
    /// <response code="400">In case the Employee's data is already Registered (it will tell which data)</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDTO EmployeeDto, string password) {

        var existingName = _context.Employees.Where(c=>c.FullName == EmployeeDto.FullName);
        if(existingName != null){
            return BadRequest("FullName already registered!");
        }

        var existingEmail = await _userManager.FindByEmailAsync(EmployeeDto.Email);
        if (existingEmail != null) {
            return BadRequest("Email already registered!");
        }

        var existingCPF = _context.Employees.Where(c=>c.CPF == EmployeeDto.CPF);
        if (existingCPF != null) {
            return BadRequest("CPF already registered!");
        }
        
        var existingPhone = _context.Employees.Where(c=>c.PhoneNumber == EmployeeDto.PhoneNumber);
        if(existingPhone != null){
            return BadRequest("PhoneNumber already registered!");
        }

        Employee Employee = new Employee(EmployeeDto.FullName, EmployeeDto.Email, EmployeeDto.CPF, EmployeeDto.PhoneNumber, EmployeeDto.salary, EmployeeDto.shift);

        var result = await _userManager.CreateAsync(Employee, password);

        if(!result.Succeeded){
            return StatusCode(500, "Internal Server Error: Register Employee Unsuccessful");
        }

        return CreatedAtAction(nameof(CreateEmployee), (EmployeeDTO)Employee);
    }

    /// <summary>
    /// Updates the Employee with the given Id
    /// </summary>
    /// <returns>Employee's DTO with the updated Data</returns>
    /// <response code="200">Employee's DTO with the updated data</response>
    /// <response code="400">If a Employee with the given Id was not found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [HttpPatch]
    public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDTO upEmployee) {

        var existingEmployee = _context.Employees.Find(upEmployee.Id);
        if (existingEmployee==null) {
            return BadRequest("Employee does not Exist!");
        }

        existingEmployee = (Employee)upEmployee;
        
        await _context.SaveChangesAsync();

        var response = JsonConvert.SerializeObject((EmployeeDTO)existingEmployee);

        return Ok(response);
    }

    /// <summary>
    /// Deletes the Employee with the given Id
    /// </summary>
    /// <returns>NoContent if successfull</returns>
    /// <response code="200">Employee was found, and thus deleted</response>
    /// <response code="400">Employee not found</response>
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Employee))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(string id) {

        var Employee = _context.Employees.Find(id);
        if(Employee == null){
            return BadRequest("Employee does not Exist!");
        }
        
        _context.Employees.Remove(Employee);

        await _context.SaveChangesAsync();

        return NoContent();
    }    
}