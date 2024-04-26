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

    public EmployeeController(ServerContext context, UserManager<Employee> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    /// <summary>
    /// Get the Employee with the given Id
    /// </summary>
    /// <param name="Id">The Client's Id</param>
    /// <returns>EmployeeDTO</returns>/// 
    /// <response code="200">The Employee's DTO</response>
    /// <response code="404">There was no Employee with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{Id}")]
    public async Task<IActionResult> ReadEmployee(string Id)
    {

        var employee = await _userManager.FindByIdAsync(Id);
        if (employee == null)
        {
            employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == Id);
        }
        if (employee == null)
        {
            return NotFound();
        }

        return Ok((EmployeeDTO)employee);
    }

    /// <summary>
    /// Gets a number of Employees, with optional filters
    /// </summary>
    /// <remarks>
    /// Does not return Not Found, but an Array of size 0 instead
    /// </remarks>
    /// <param name="username">Filters results to only Users whose username contains this string</param>
    /// <param name="offset">Offsets the result by a given amount</param>
    /// <param name="limit">Limits the number of results</param>
    /// <param name="sort">Orders the result by a given field. Does not order if the field does not exist</param>
    /// <returns>EmployeeDTO[]</returns>
    /// <response code="200">Returns an array of EmployeeDTO</response>
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

        if (!string.IsNullOrWhiteSpace(sort) && sort.Contains("desc"))
        {
            employeesQuery.Reverse();
        }

        offset = offset.HasValue ? offset : 0;
        limit = limit.HasValue ? limit : 10;

        employeesQuery = employeesQuery.Skip((int)offset).Take((int)limit);

        var resultsArray = await employeesQuery.Select(c => (EmployeeDTO)c).ToArrayAsync();

        return Ok(resultsArray);
    }

    /// <summary>
    /// Creates a Employee User
    /// </summary>
    /// <returns>The created Employee's Data</returns>
    /// <response code="200">EmployeeDTO</response>
    /// <response code="400">The Client's (PhoneNumber || CPF || Email) does not exist</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRegister EmployeeDto)
    {

        var existingEmail = await _userManager.FindByEmailAsync(EmployeeDto.Email);
        if (existingEmail == null)
        {
            existingEmail = await _context.Employees.FirstOrDefaultAsync(x => x.Email == EmployeeDto.Email);
        }
        if (existingEmail != null)
        {
            return BadRequest("Email already registered!");
        }

        var existingCPF = _context.Employees.Where(c => c.CPF == EmployeeDto.CPF);
        if (existingCPF != null)
        {
            return BadRequest("CPF already registered!");
        }

        var existingPhone = _context.Employees.Where(c => c.PhoneNumber == EmployeeDto.PhoneNumber);
        if (existingPhone != null)
        {
            return BadRequest("PhoneNumber already registered!");
        }

        Employee Employee = new Employee(EmployeeDto.FullName, EmployeeDto.CPF, EmployeeDto.salary, EmployeeDto.Email, EmployeeDto.PhoneNumber);

        var userCreationResult = await _userManager.CreateAsync(Employee, EmployeeDto.newPassword);

        if (!userCreationResult.Succeeded)
        {
            return StatusCode(500, "Internal Server Error: Register Employee Unsuccessful");
        }

        var userRoleAddResult = await _userManager.AddToRoleAsync(Employee, Common.Employee_Role);

        if (!userRoleAddResult.Succeeded)
        {
            return StatusCode(500, "Internal Server Error: Add Employee Role Unsuccessful");
        }

        _context.SaveChanges();

        return CreatedAtAction(nameof(CreateEmployee), (EmployeeDTO)Employee);
    }

    /// <summary>
    /// Updates the Employee with the given Id
    /// </summary>
    /// <returns>EmployeeDTO</returns>
    /// <response code="200">Updated Employee's DTO</response>
    /// <response code="400">There was no Employee with the given Id</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPatch]
    public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeRegister upEmployee)
    {

        var existingEmployee = await _userManager.FindByIdAsync(upEmployee.Id);
        if (existingEmployee == null)
        {
            existingEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == upEmployee.Id);
        }
        if (existingEmployee == null)
        {
            return BadRequest("Employee does not Exist!");
        }

        if (existingEmployee.CPF != upEmployee.CPF)
        {
            var existingCPF = _context.Employees.Where(x => x.CPF == upEmployee.CPF);
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
            var existingUsername = _context.Employees.Where(x => x.UserName == upEmployee.UserName);
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
            var existingPhonenumber = _context.Employees.Where(x => x.PhoneNumber == upEmployee.PhoneNumber);
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
    /// <param name="Id">The Id of the Employee to be deleted</param>
    /// <returns>NoContentResult</returns>
    /// <response code="200">Employee was found, and thus deleted</response>
    /// <response code="400">There was no Employee with the given Id</response>
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(EmployeeDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteEmployee(string Id)
    {

        var employee = await _userManager.FindByIdAsync(Id);
        if (employee == null)
        {
            return BadRequest("Employee does not Exist!");
        }

        await _userManager.DeleteAsync(employee);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}