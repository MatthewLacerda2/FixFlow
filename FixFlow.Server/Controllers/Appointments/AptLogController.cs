using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.Erros;
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
//[Authorize]
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
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptLog[]))]
	[HttpGet]
	public async Task<IActionResult> ReadLogs(string businessId, string? clientName, string? service,
												float minPrice, float maxPrice, DateTime minDateTime, DateTime maxDateTime,
													int offset, int limit) {

		var logsQuery = _context.Logs.AsQueryable();

		logsQuery = logsQuery.Where(x => x.BusinessId == businessId);

		if (!string.IsNullOrWhiteSpace(clientName)) {
			logsQuery = logsQuery.Where(x => x.Customer.FullName.Contains(clientName!));
		}

		if (!string.IsNullOrWhiteSpace(service)) {
			logsQuery = logsQuery.Where(x => x.Service != null && x.Service.Contains(service!));
		}

		logsQuery = logsQuery.Where(x => x.Price >= minPrice);
		logsQuery = logsQuery.Where(x => x.Price <= maxPrice);

		logsQuery = logsQuery.Where(x => x.dateTime.Date >= minDateTime.Date);
		logsQuery = logsQuery.Where(x => x.dateTime.Date <= maxDateTime.Date);

		var resultsArray = await logsQuery
			.Skip(offset)
			.Take(limit)
			.ToArrayAsync();

		for (int i = 0; i < resultsArray.Length; i++) {
			Customer customer = _context.Customers.Find(resultsArray[i].CustomerId)!;
			resultsArray[i].Customer = customer;
		}

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

		var existingCustomer = _context.Customers.Find(createLog.customerId);
		if (existingCustomer == null) {
			return BadRequest(NotExistErrors.Customer);
		}

		if (!string.IsNullOrWhiteSpace(createLog.ScheduleId)) {
			var scheduleExists = _context.Schedules.Find(createLog.ScheduleId);
			if (scheduleExists == null) {
				return BadRequest(NotExistErrors.AptSchedule);
			}
		}

		var existingBusiness = _context.Business.Find(existingCustomer.BusinessId);
		createLog.BusinessId = existingCustomer.BusinessId;

		if (existingBusiness!.allowListedServicesOnly) {
			if (createLog.Service == null || !existingBusiness.services.Contains(createLog.Service)) {
				return BadRequest(ValidatorErrors.UnlistedService);
			}
		}

		AptLog newLog = new AptLog(createLog);

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

		var contact = _context.Contacts.Where(x => x.aptLogId == Id).FirstOrDefault();
		if (contact != null) {
			_context.Contacts.Remove(contact);
		}

		_context.Logs.Remove(logToDelete);

		await _context.SaveChangesAsync();
		return NoContent();
	}
}
