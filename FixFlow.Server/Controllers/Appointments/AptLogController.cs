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
	/// Gets a number of filtered Logs
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AptLog[]>))]
	[HttpGet]
	public async Task<IActionResult> ReadLogs([FromBody] AptLogFilter filter) {

		var logsQuery = _context.Logs.AsQueryable();

		logsQuery = logsQuery.Where(x => x.BusinessId == filter.businessId);

		if (!string.IsNullOrWhiteSpace(filter.client)) {
			logsQuery = logsQuery.Where(x => x.Client.FullName.Contains(filter.client!));
		}

		if (!string.IsNullOrWhiteSpace(filter.service)) {
			logsQuery = logsQuery.Where(x => x.service != null && x.service.Contains(filter.service!));
		}

		logsQuery = logsQuery.Where(x => x.price >= filter.minPrice);
		logsQuery = logsQuery.Where(x => x.price <= filter.maxPrice);

		logsQuery = logsQuery.Where(x => x.dateTime.Date >= filter.minDateTime.Date);
		logsQuery = logsQuery.Where(x => x.dateTime.Date <= filter.maxDateTime.Date);

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
	/// Creates a Log
	/// </summary>
	/// <remarks>
	/// Generates a Contact
	/// </remarks>
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AptLog))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPost]
	public async Task<IActionResult> CreateLog([FromBody] CreateAptLog createLog) {

		var existingClient = _context.Clients.Find(createLog.ClientId);
		if (existingClient == null) {
			return BadRequest(NotExistErrors.Client);
		}

		var existingBusiness = _context.Business.Find(createLog.BusinessId);
		if (existingBusiness == null) {
			return BadRequest(NotExistErrors.Business);
		}

		if (existingBusiness.allowListedServicesOnly) {
			if (!existingBusiness.services.Contains(createLog.service)) {
				return BadRequest(ValidatorErrors.UnlistedService);
			}
		}

		AptLog newLog = new AptLog(createLog);

		AptSchedule aptSchedule = _context.Schedules.Where(x => x.ClientId == createLog.ClientId)
								.Where(x => x.dateTime <= DateTime.Now).Where(x => x.dateTime >= DateTime.Now.AddDays(-1))
								.OrderByDescending(x => x.dateTime).FirstOrDefault()!;

		if (aptSchedule != null) {
			newLog.scheduleId = aptSchedule.Id;
		}

		_context.Logs.Add(newLog);

		AptContact contact = new AptContact(newLog, createLog.whenShouldClientComeBack);
		_context.Contacts.Add(contact);

		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateLog), newLog);
	}

	/// <summary>
	/// Update the Appointment Log with the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptLog))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPut]
	public async Task<IActionResult> UpdateLog([FromBody] UpdateAptLog upLog) {

		var existingLog = _context.Logs.Find(upLog.Id);
		if (existingLog == null) {
			return BadRequest(NotExistErrors.AptLog);
		}

		var existingBusiness = _context.Business.Find(existingLog.BusinessId);
		if (existingBusiness!.allowListedServicesOnly) {
			if (!existingBusiness.services.Contains(upLog.Service)) {
				return BadRequest(ValidatorErrors.UnlistedService);
			}
		}

		if (!string.IsNullOrWhiteSpace(upLog.ScheduleId)) {
			var existingSchedule = _context.Schedules.Find(upLog.ScheduleId);
			if (existingSchedule == null) {
				return BadRequest(NotExistErrors.AptSchedule);
			}
		}

		existingLog.scheduleId = upLog.ScheduleId;
		existingLog.dateTime = upLog.dateTime;
		existingLog.service = upLog.Service;
		existingLog.price = upLog.Price;
		existingLog.description = upLog.Description;

		await _context.SaveChangesAsync();

		return Ok(existingLog);
	}

	/// <summary>
	/// Deletes the Appointment Log with the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpDelete]
	public async Task<IActionResult> DeleteLog([FromBody] string Id) {

		var logToDelete = _context.Logs.Find(Id);
		if (logToDelete == null) {
			return BadRequest(NotExistErrors.AptLog);
		}

		_context.Logs.Remove(logToDelete);

		var contact = _context.Contacts.Where(x => x.aptLogId == Id).FirstOrDefault();

		if (contact != null) {
			_context.Contacts.Remove(contact);
		}

		await _context.SaveChangesAsync();
		return NoContent();
	}
}