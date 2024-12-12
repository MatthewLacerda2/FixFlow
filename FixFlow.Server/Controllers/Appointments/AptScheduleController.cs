using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Controllers;

[ApiController]
[Route(Common.api_v1 + "schedules")]
[Authorize]
[Produces("application/json")]
public class AptScheduleController : ControllerBase {

	private readonly ServerContext _context;

	public AptScheduleController(ServerContext context) {
		_context = context;
	}

	/// <summary>
	/// Gets a number of filtered Schedules
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptSchedule[]))]
	[HttpGet]
	public async Task<IActionResult> ReadSchedules(string? client, string? service, float minPrice, float? maxPrice,
													DateTime minDateTime, DateTime maxDateTime, int offset, int limit) {

		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;

		var schedulesQuery = _context.Schedules.AsQueryable();

		schedulesQuery = _context.Schedules.Where(x => x.BusinessId == businessId);

		if (!string.IsNullOrWhiteSpace(client)) {
			schedulesQuery = schedulesQuery.Where(x => x.Customer.FullName.Contains(client));
		}

		if (!string.IsNullOrWhiteSpace(service)) {
			schedulesQuery = schedulesQuery.Where(x => x.Service != null && x.Service.Contains(service));
		}

		schedulesQuery = schedulesQuery.Where(x => x.price >= minPrice);

		if (maxPrice.HasValue) {
			schedulesQuery = schedulesQuery.Where(x => x.price <= maxPrice);
		}

		schedulesQuery = schedulesQuery.Where(x => x.dateTime >= minDateTime);
		schedulesQuery = schedulesQuery.Where(x => x.dateTime <= maxDateTime);

		var resultsArray = await schedulesQuery
			.Skip(offset)
			.Take(limit)
			.OrderByDescending(x => x.dateTime).ThenBy(x => x.price).ThenBy(x => x.CustomerId).ThenBy(x => x.Id)
			.ToArrayAsync();

		for (int i = 0; i < resultsArray.Length; i++) {
			Customer customer = _context.Customers.Find(resultsArray[i].CustomerId)!;
			resultsArray[i].Customer = customer;
		}

		return Ok(resultsArray);
	}

	/// <summary>
	/// Creates an Appointment Schedule
	/// </summary>
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AptSchedule))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPost]
	public async Task<IActionResult> CreateSchedule([FromBody] CreateAptSchedule newAppointment) {

		var existingCustomer = _context.Customers.Find(newAppointment.CustomerId);
		if (existingCustomer == null) {
			return BadRequest(NotExistErrors.customer);
		}

		var existingBusiness = _context.Business.Find(existingCustomer.BusinessId);

		newAppointment.Service = StringUtils.PhraseCaseNormalizer(newAppointment.Service);
		newAppointment.Description = StringUtils.PhraseCaseNormalizer(newAppointment.Description);

		if (existingBusiness!.AllowListedServicesOnly) {
			if (newAppointment.Service == null || !existingBusiness.Services.Contains(newAppointment.Service)) {
				return BadRequest(ValidatorErrors.UnlistedService);
			}
		}

		IdlePeriod[] idps = _context.IdlePeriods.Where(x => x.BusinessId == existingCustomer.BusinessId).ToArray();
		foreach (IdlePeriod idp in idps) {
			if (idp.Start <= newAppointment.dateTime && idp.Finish >= newAppointment.dateTime) {
				return BadRequest(ValidatorErrors.DateWithinIdlePeriod);
			}
		}

		AptContact contact = _context.Contacts.Where(x => x.CustomerId == newAppointment.CustomerId)
								.Where(x => x.dateTime <= DateTime.UtcNow).Where(x => x.dateTime >= DateTime.UtcNow.AddDays(-1))
								.OrderByDescending(x => x.dateTime).FirstOrDefault()!;

		AptSchedule schedule = new AptSchedule(newAppointment, existingBusiness.Id, contact != null);
		_context.Schedules.Add(schedule);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateSchedule), schedule);
	}

	/// <summary>
	/// Update the Appointment Schedule of the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptSchedule))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPatch]
	public async Task<IActionResult> UpdateSchedule([FromBody] AptSchedule upSchedule) {

		var existingAppointment = _context.Schedules.Find(upSchedule.Id);
		if (existingAppointment == null) {
			return BadRequest(NotExistErrors.aptSchedule);
		}

		var existingBusiness = _context.Business.Find(existingAppointment.BusinessId);

		upSchedule.Service = StringUtils.PhraseCaseNormalizer(upSchedule.Service);
		upSchedule.Description = StringUtils.PhraseCaseNormalizer(upSchedule.Description);

		if (upSchedule.Service != null && existingBusiness!.AllowListedServicesOnly) {
			if (!existingBusiness.Services.Contains(upSchedule.Service)) {
				return BadRequest(ValidatorErrors.UnlistedService);
			}
		}

		//TODO: validate business hours

		IdlePeriod[] idps = _context.IdlePeriods.Where(x => x.BusinessId == existingAppointment.BusinessId).ToArray();
		foreach (IdlePeriod idp in idps) {
			if (idp.Start <= upSchedule.dateTime && idp.Finish >= upSchedule.dateTime) {
				return BadRequest(ValidatorErrors.DateWithinIdlePeriod);
			}
		}

		existingAppointment.dateTime = upSchedule.dateTime;
		existingAppointment.Service = upSchedule.Service;
		existingAppointment.Description = upSchedule.Description;
		existingAppointment.price = upSchedule.price;

		await _context.SaveChangesAsync();

		return Ok(upSchedule);
	}

	/// <summary>
	/// Deletes the Appointment Schedule with the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpDelete]
	public async Task<IActionResult> DeleteSchedule([FromBody] string Id) {

		var scheduleToDelete = _context.Schedules.Find(Id);
		if (scheduleToDelete == null) {
			return BadRequest(NotExistErrors.aptSchedule);
		}

		_context.Schedules.Remove(scheduleToDelete);

		AptLog[] logs = _context.Logs.Where(x => x.ScheduleId == Id).ToArray();
		foreach (AptLog log in logs) {
			log.ScheduleId = null;
		}
		_context.Logs.UpdateRange(logs);

		await _context.SaveChangesAsync();
		return NoContent();
	}
}
