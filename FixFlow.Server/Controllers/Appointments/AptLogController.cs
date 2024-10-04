using Microsoft.AspNetCore.Authorization;
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
[Authorize]
[Produces("application/json")]
public class AptLogController : ControllerBase {

	private readonly ServerContext _context;
	private readonly UserManager<Business> _userManager;
	private readonly UserManager<Customer> _clientManager;

	public AptLogController(ServerContext context, UserManager<Business> userManager, UserManager<Customer> clientManager) {
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
			logsQuery = logsQuery.Where(x => x.Customer.FullName.Contains(filter.client!));
		}

		if (!string.IsNullOrWhiteSpace(filter.service)) {
			logsQuery = logsQuery.Where(x => x.Service != null && x.Service.Contains(filter.service!));
		}

		logsQuery = logsQuery.Where(x => x.Price >= filter.minPrice);
		logsQuery = logsQuery.Where(x => x.Price <= filter.maxPrice);

		logsQuery = logsQuery.Where(x => x.dateTime.Date >= filter.minDateTime.Date);
		logsQuery = logsQuery.Where(x => x.dateTime.Date <= filter.maxDateTime.Date);

		switch (filter.sort) {
			case LogSort.Customer:
				logsQuery = logsQuery.OrderBy(s => s.Customer.FullName).ThenByDescending(s => s.dateTime).ThenByDescending(s => s.Price).ThenBy(s => s.Id);
				break;
			case LogSort.Date:
				logsQuery = logsQuery.OrderByDescending(s => s.dateTime).ThenBy(s => s.Price).ThenBy(s => s.Id);
				break;
			case LogSort.Price:
				logsQuery = logsQuery.OrderByDescending(s => s.Price).ThenBy(s => s.dateTime).ThenBy(s => s.Id);
				break;
		}

		if (filter.descending) {
			switch (filter.sort) {
				case LogSort.Customer:
					logsQuery = logsQuery.OrderByDescending(s => s.Customer.FullName).ThenByDescending(s => s.dateTime).ThenBy(s => s.Id);
					break;
				case LogSort.Date:
					logsQuery = logsQuery.OrderBy(s => s.dateTime).ThenBy(s => s.Price).ThenBy(s => s.Id);
					break;
				case LogSort.Price:
					logsQuery = logsQuery.OrderBy(s => s.Price).ThenBy(s => s.Customer.FullName).ThenByDescending(s => s.dateTime).ThenBy(s => s.Id);
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

		var existingCustomer = _context.Customers.Find(createLog.CustomerId);
		if (existingCustomer == null) {
			return BadRequest(NotExistErrors.Customer);
		}

		var existingBusiness = _context.Business.Find(existingCustomer.BusinessId);

		if (existingBusiness!.allowListedServicesOnly) {
			if (createLog.Service == null || !existingBusiness.services.Contains(createLog.Service)) {
				return BadRequest(ValidatorErrors.UnlistedService);
			}
		}

		AptLog newLog = new AptLog(createLog);

		AptSchedule aptSchedule = _context.Schedules.Where(x => x.CustomerId == createLog.CustomerId)
								.Where(x => x.dateTime.Date == DateTime.UtcNow.Date)
								.OrderByDescending(x => x.dateTime).FirstOrDefault()!;

		newLog.ScheduleId = aptSchedule.Id;

		AptContact contact = new AptContact(newLog, createLog.whenShouldCustomerComeBack);
		if (contact.dateTime.TimeOfDay > new TimeSpan(18, 0, 0)) {
			contact.dateTime = contact.dateTime.Date.AddHours(12);
		}
		if (contact.dateTime.DayOfWeek == DayOfWeek.Saturday) {
			contact.dateTime = contact.dateTime.AddDays(2);
		}
		else if (contact.dateTime.DayOfWeek == DayOfWeek.Sunday) {
			contact.dateTime = contact.dateTime.AddDays(1);
		}

		_context.Logs.Add(newLog);
		_context.Contacts.Add(contact);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateLog), newLog);
	}

	/// <summary>
	/// Update the Appointment Log with the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptLog))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPatch]
	public async Task<IActionResult> UpdateLog([FromBody] UpdateAptLog upLog) {

		var existingLog = _context.Logs.Find(upLog.Id);
		if (existingLog == null) {
			return BadRequest(NotExistErrors.AptLog);
		}

		var existingBusiness = _context.Business.Find(existingLog.BusinessId);
		if (upLog.Service != null && existingBusiness!.allowListedServicesOnly) {
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

		existingLog.ScheduleId = upLog.ScheduleId;
		existingLog.dateTime = upLog.dateTime;
		existingLog.Service = upLog.Service;
		existingLog.Price = upLog.Price;
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
