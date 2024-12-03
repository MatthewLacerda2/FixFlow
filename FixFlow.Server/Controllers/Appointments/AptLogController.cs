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
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptLog[]))]
	[HttpGet]
	public async Task<IActionResult> ReadLogs(string? clientName, string? service, float minPrice, float? maxPrice,
												DateTime minDateTime, DateTime maxDateTime, int offset, int limit) {

		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;

		var logsQuery = _context.Logs.AsQueryable();

		logsQuery = logsQuery.Where(x => x.BusinessId == businessId);

		if (!string.IsNullOrWhiteSpace(clientName)) {
			logsQuery = logsQuery.Where(x => x.Customer.FullName.Contains(clientName!));
		}

		if (!string.IsNullOrWhiteSpace(service)) {
			logsQuery = logsQuery.Where(x => x.Service != null && x.Service.Contains(service!));
		}

		logsQuery = logsQuery.Where(x => x.Price >= minPrice);
		if (maxPrice.HasValue) {
			logsQuery = logsQuery.Where(x => x.Price <= maxPrice);
		}

		logsQuery = logsQuery.Where(x => x.dateTime.Date >= minDateTime.Date);
		logsQuery = logsQuery.Where(x => x.dateTime.Date <= maxDateTime.Date);

		var resultsArray = await logsQuery
			.Skip(offset)
			.Take(limit)
			.OrderByDescending(x => x.dateTime).ThenBy(x => x.Price).ThenBy(x => x.CustomerId).ThenBy(x => x.Id)
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

		var existingCustomer = _context.Customers.Find(createLog.CustomerId);
		if (existingCustomer == null) {
			return BadRequest(NotExistErrors.customer);
		}

		if (!string.IsNullOrWhiteSpace(createLog.ScheduleId)) {
			var scheduleExists = _context.Schedules.Find(createLog.ScheduleId);
			if (scheduleExists == null) {
				return BadRequest(NotExistErrors.aptSchedule);
			}
		}

		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;
		if (businessId != existingCustomer.BusinessId) {
			return Unauthorized(ValidatorErrors.BadLogOwnership);
		}

		createLog.Service = StringUtils.PhraseCaseNormalizer(createLog.Service);
		createLog.Description = StringUtils.PhraseCaseNormalizer(createLog.Description);

		var existingBusiness = _context.Business.Find(businessId);
		if (existingBusiness!.AllowListedServicesOnly) {
			if (createLog.Service == null || !existingBusiness.Services.Contains(createLog.Service)) {
				return BadRequest(ValidatorErrors.UnlistedService);
			}
		}

		AptLog newLog = new AptLog(createLog, businessId);
		AptContact contact = new AptContact(newLog, createLog.DateToComeback);
		var timeOfDay = contact.dateTime.TimeOfDay;
		var early = new TimeSpan(8, 10, 0);
		var late = new TimeSpan(18, 0, 0);

		if (timeOfDay > late || timeOfDay < early) {
			var hoursToAdd = Math.Abs(8 - contact.dateTime.TimeOfDay.Hours);
			contact.dateTime.AddHours(hoursToAdd);
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
	/// Update the Appointment Log of the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptLog))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPatch]
	public async Task<IActionResult> UpdateLog([FromBody] UpdateAptLog upLog) {

		var existingLog = _context.Logs.Find(upLog.Id);
		if (existingLog == null) {
			return BadRequest(NotExistErrors.aptLog);
		}

		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;
		if (existingLog.BusinessId != businessId) {
			return Unauthorized(ValidatorErrors.BadLogOwnership);
		}

		existingLog.Service = StringUtils.PhraseCaseNormalizer(existingLog.Service);
		existingLog.Description = StringUtils.PhraseCaseNormalizer(existingLog.Description);

		var existingBusiness = _context.Business.Find(businessId);
		if (upLog.Service != null && existingBusiness!.AllowListedServicesOnly) {
			if (!existingBusiness.Services.Contains(upLog.Service)) {
				return BadRequest(ValidatorErrors.UnlistedService);
			}
		}

		existingLog.dateTime = upLog.dateTime;
		existingLog.Service = upLog.Service;
		existingLog.Price = upLog.Price;
		existingLog.Description = upLog.Description;

		await _context.SaveChangesAsync();

		return Ok(existingLog);
	}

	/// <summary>
	/// Deletes the Appointment Log of the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpDelete]
	public async Task<IActionResult> DeleteLog([FromBody] string Id) {

		var logToDelete = _context.Logs.Find(Id);
		if (logToDelete == null) {
			return BadRequest(NotExistErrors.aptLog);
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
