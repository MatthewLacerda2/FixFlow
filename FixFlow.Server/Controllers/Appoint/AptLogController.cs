using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.Erros;
using Server.Models.Filters;
using Server.Models.Utils;

namespace Server.Controllers;

/// <summary>
/// Controller class for Appointment Log CRUD requests
/// </summary>
/// <remarks>
/// Logs are simply registration that the Appointment was done
/// </remarks>
[ApiController]
[Route(Common.api_v1 + "logs")]
[Produces("application/json")]
public class AptLogController : ControllerBase {

	private readonly ServerContext _context;
	private readonly UserManager<Business> _userManager;
	private readonly UserManager<Client> _clientManager;

	public AptLogController(ServerContext context, UserManager<Business> userManager, UserManager<Client> clientManager) {
		_context = context;
		_userManager = userManager;
		_clientManager = clientManager;
	}

	/// <summary>
	/// Gets a number of Appointment Logs, with optional filters
	/// </summary>
	/// <remarks>
	/// Does not return Not Found, but an Array of size 0 instead
	/// </remarks>
	/// <param name="filter">The Filter Properties of the Query</param>
	/// <returns>AptLog[]</returns>
	/// <response code="200">Returns an array of AppointmentLog</response>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AptLog[]>))]
	[HttpGet]
	public async Task<IActionResult> ReadLogs([FromBody] AptLogFilter filter) {

		var logsQuery = _context.Logs.AsQueryable();

		logsQuery = logsQuery.Where(x => x.businessId == filter.businessId);

		if (!string.IsNullOrWhiteSpace(filter.client)) {
			logsQuery = logsQuery.Where(x => x.Client.FullName.Contains(filter.client!));
		}

		logsQuery = logsQuery.Where(x => x.price >= filter.minPrice);
		logsQuery = logsQuery.Where(x => x.price <= filter.maxPrice);

		logsQuery = logsQuery.Where(x => x.dateTime.Date >= filter.minDateTime.ToDateTime(TimeOnly.MinValue).Date);
		logsQuery = logsQuery.Where(x => x.dateTime.Date <= filter.maxDateTime.ToDateTime(TimeOnly.MaxValue).Date);

		switch (filter.sort) {
			case LogSort.Client:
				logsQuery = logsQuery.OrderBy(s => s.Client.FullName).ThenByDescending(s => s.dateTime).ThenByDescending(s => s.price).ThenBy(s => s.Id);
				break;
			case LogSort.Date:
				logsQuery = logsQuery.OrderByDescending(s => s.dateTime).ThenBy(s => s.price).ThenBy(s => s.Id);
				break;
			case LogSort.Price:
				logsQuery = logsQuery.OrderByDescending(s => s.price).ThenBy(s => s.dateTime).ThenBy(s => s.Id);
				break;
		}

		if (filter.descending) {
			switch (filter.sort) {
				case LogSort.Client:
					logsQuery = logsQuery.OrderByDescending(s => s.Client.FullName).ThenByDescending(s => s.dateTime).ThenBy(s => s.Id);
					break;
				case LogSort.Date:
					logsQuery = logsQuery.OrderBy(s => s.dateTime).ThenBy(s => s.price).ThenBy(s => s.Id);
					break;
				case LogSort.Price:
					logsQuery = logsQuery.OrderBy(s => s.price).ThenBy(s => s.Client.FullName).ThenByDescending(s => s.dateTime).ThenBy(s => s.Id);
					break;
			}
		}

		var resultsArray = await logsQuery
			.Skip(filter.offset)
			.Take(filter.limit)
			.ToArrayAsync();

		return Ok(resultsArray);
	}

	/// <summary>
	/// Create an Appointment Log
	/// </summary>
	/// <returns>AptLog</returns>
	/// <response code="200">The created Appointment Log</response>
	/// <response code="400">The given (ClientId || ScheduleId) does not exist</response>
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AptLog))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPost]
	public async Task<IActionResult> CreateLog([FromBody] AptLog newLog) {

		var existingClient = _context.Clients.Find(newLog.ClientId);
		if (existingClient == null) {
			return BadRequest(NotExistErrors.Client);
		}

		var existingBusiness = _context.Business.Find(newLog.businessId);
		if (existingBusiness == null) {
			return BadRequest(NotExistErrors.Business);
		}

		if (!string.IsNullOrWhiteSpace(newLog.scheduleId)) {
			var existingSchedule = _context.Schedules.Find(newLog.scheduleId);

			if (existingSchedule == null) {
				return BadRequest(NotExistErrors.AptSchedule);
			}
		}

		newLog.Id = new Guid().ToString();
		_context.Logs.Add(newLog);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateLog), newLog);
	}

	/// <summary>
	/// Update the Appointment Log with the given Id
	/// </summary>
	/// <returns>AptLog</returns>
	/// <response code="200">The updated Appointment Log</response>
	/// <response code="400">There was no AptLog with the given Id</response>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptLog))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPatch]
	public async Task<IActionResult> UpdateLog([FromBody] AptLog upLog) {

		var existingClient = _context.Clients.Find(upLog.ClientId);
		if (existingClient == null) {
			return BadRequest(NotExistErrors.Client);
		}

		var existingBusiness = _context.Business.Find(upLog.businessId);
		if (existingBusiness == null) {
			return BadRequest(NotExistErrors.Business);
		}

		if (!string.IsNullOrWhiteSpace(upLog.scheduleId)) {
			var existingSchedule = _context.Schedules.Find(upLog.scheduleId);

			if (existingSchedule == null) {
				return BadRequest(NotExistErrors.AptSchedule);
			}
		}

		var existingLog = _context.Logs.Find(upLog.Id);
		if (existingLog == null) {
			return BadRequest(NotExistErrors.AptLog);
		}

		//TODO: check for idle period and business hours
		_context.Logs.Update(upLog);
		await _context.SaveChangesAsync();

		return Ok(upLog);
	}

	/// <summary>
	/// Deletes the Appointment Log with the given Id
	/// </summary>
	/// <param name="Id">The Id of the AptLog to be deleted</param>
	/// <returns>NoContentResult</returns>
	/// <response code="204">No Content</response>
	/// <response code="400">There was no Log with the given Id</response>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpDelete]
	public async Task<IActionResult> DeleteLog([FromBody] string Id) {

		var logToDelete = _context.Logs.Find(Id);
		if (logToDelete == null) {
			return BadRequest(NotExistErrors.AptLog);
		}

		_context.Logs.Remove(logToDelete);
		await _context.SaveChangesAsync();
		return NoContent();
	}
}
