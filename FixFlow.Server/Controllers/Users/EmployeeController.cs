using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Utils;

namespace Server.Controllers;

/// <summary>
/// Controller class for Employee CRUD requests
/// </summary>
[ApiController]
[Route(Common.api_route + "employee")]
[Produces("application/json")]
public class EmployeeController : ControllerBase
{

    private readonly ServerContext _context;
    private readonly UserManager<Employee> _userManager;

    /// <summary>
    /// Controller class for Employee CRUD requests
    /// </summary>
    public EmployeeController(ServerContext context, UserManager<Employee> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    /// <summary>
    /// Get the Employee with the given Id
    /// </summary>
    /// <returns>Employee with the given Id. NotFoundResult if there is none</returns>
    /// <response code="200">Returns the Employee's DTO</response>
    /// <response code="404">If there is none with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<IActionResult> ReadEmployee(string id)
    {

        var employee = await _userManager.FindByIdAsync(id);
        if (employee == null)
        {
            employee = await _context.Employees.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == id);
        }
        if (employee == null)
        {
            return NotFound();
        }

        return Ok((EmployeeDTO)employee);
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeDTO[]>))]
    [HttpGet]
    public async Task<IActionResult> ReadEmployees(string? username, int? offset, int? limit, string? sort)
    {

        var employeesQuery = _context.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(username))
        {
            employeesQuery = employeesQuery.Where(Employee => Employee.UserName!.Contains(username, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(sort))
        {
            sort = sort.ToLower();
            if (sort.Contains("name"))
            {
                employeesQuery.OrderBy(s => s.FullName).ThenBy(s => s.UserName);
            }
        }

        offset = offset.HasValue ? offset : 0;
        limit = limit.HasValue ? limit : 10;

        employeesQuery = employeesQuery.Skip((int)offset).Take((int)limit);

        var resultsArray = await employeesQuery.Select(c => (EmployeeDTO)c).ToArrayAsync();

        if (!string.IsNullOrWhiteSpace(sort) && sort.Contains("desc"))
        {
            resultsArray.Reverse();
        }

        return Ok(resultsArray);
    }

    /// <summary>
    /// Creates a Employee User
    /// </summary>
    /// <returns>The created Employee's Data</returns>
    /// <response code="200">EmployeeDTO</response>
    /// <response code="400">Returns a string with the requirements that were not filled</response>
    /// <response code="400">In case the Employee's data is already Registered (it will tell which data)</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRegister EmployeeDto)
    {

        DeleteInactiveEmployees();

        var existingEmail = await _userManager.FindByEmailAsync(EmployeeDto.Email);
        if (existingEmail == null)
        {
            existingEmail = await _context.Employees.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Email == EmployeeDto.Email);
        }
        if (existingEmail != null)
        {
            return BadRequest("Email already registered!");
        }

        var existingCPF = _context.Employees.IgnoreQueryFilters().Where(c => c.CPF == EmployeeDto.CPF);
        if (existingCPF != null)
        {
            return BadRequest("CPF already registered!");
        }

        var existingPhone = _context.Employees.IgnoreQueryFilters().Where(c => c.PhoneNumber == EmployeeDto.PhoneNumber);
        if (existingPhone != null)
        {
            return BadRequest("PhoneNumber already registered!");
        }

        Employee Employee = new Employee(EmployeeDto.FullName, EmployeeDto.CPF, EmployeeDto.salary, EmployeeDto.Email, EmployeeDto.PhoneNumber);

        var result = await _userManager.CreateAsync(Employee, EmployeeDto.newPassword);

        if (!result.Succeeded)
        {
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPatch]
    public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeRegister upEmployee)
    {

        DeleteInactiveEmployees();

        var existingEmployee = await _userManager.FindByIdAsync(upEmployee.Id);
        if (existingEmployee == null)
        {
            existingEmployee = await _context.Employees.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == upEmployee.Id);
        }
        if (existingEmployee == null)
        {
            return BadRequest("Employee does not Exist!");
        }

        if (existingEmployee.CPF != upEmployee.CPF)
        {
            var existingCPF = _context.Clients.IgnoreQueryFilters().Where(x => x.CPF == upEmployee.CPF);
            if (existingCPF.Any())
            {
                return BadRequest("CPF taken");
            }
            else
            {
                await _userManager.SetUserNameAsync(existingEmployee, upEmployee.UserName);
            }
        }

        if (existingEmployee.UserName != upEmployee.UserName)
        {
            var existingUsername = _context.Clients.IgnoreQueryFilters().Where(x => x.UserName == upEmployee.UserName);
            if (existingUsername.Any())
            {
                return BadRequest("Username already exists");
            }
            else
            {
                await _userManager.SetUserNameAsync(existingEmployee, upEmployee.UserName);
            }
        }

        if (existingEmployee.PhoneNumber != upEmployee.PhoneNumber)
        {
            var existingPhonenumber = _context.Clients.IgnoreQueryFilters().Where(x => x.PhoneNumber == upEmployee.PhoneNumber);
            if (existingPhonenumber.Any())
            {
                return BadRequest("PhoneNumber taken");
            }
            else
            {
                await _userManager.SetPhoneNumberAsync(existingEmployee, upEmployee.PhoneNumber);
            }
        }

        existingEmployee = (Employee)upEmployee;

        if (!string.IsNullOrWhiteSpace(upEmployee.currentPassword) && !string.IsNullOrWhiteSpace(upEmployee.newPassword))
        {
            await _userManager.ChangePasswordAsync(existingEmployee, upEmployee.currentPassword, upEmployee.newPassword);
        }

        await _context.SaveChangesAsync();

        return Ok((EmployeeDTO)existingEmployee);
    }

    /// <summary>
    /// Deletes the Employee with the given Id
    /// </summary>
    /// <returns>NoContent if successfull</returns>
    /// <response code="200">Employee was found, and thus deleted</response>
    /// <response code="400">Employee not found</response>
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(EmployeeDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(string id)
    {

        var employee = await _userManager.FindByIdAsync(id);
        if (employee == null)
        {
            return BadRequest("Employee does not Exist!");
        }

        employee.isDeleted = true;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    public void DeleteInactiveEmployees()
    {

        var cutoffDate = DateTime.UtcNow.AddDays(-90);
        var usersToDelete = _userManager.Users.Where(u => u.LastLogin < cutoffDate);

        foreach (var user in usersToDelete)
        {
            user.isDeleted = true;
        }

    }
}