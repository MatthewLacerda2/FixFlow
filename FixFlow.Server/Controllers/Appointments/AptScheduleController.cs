using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Filters;
using Server.Models.Utils;

namespace Server.Controllers;

/// <summary>
/// Controller class for Scheduled Appointment CRUD requests
/// </summary>
/// <remarks>
/// Schedules are simply the setup of an Appointment, not the Appointment itself
/// </remarks>
[ApiController]
[Route(Common.api_v1 + "schedules")]
[Produces("application/json")]
public class AptScheduleController : ControllerBase {

	private readonly ServerContext _context;

	public AptScheduleController(ServerContext context) {
		_context = context;
	}

	/// <summary>
	/// Gets a number of filtered Schedules
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AptSchedule[]>))]
	[HttpGet]
	public async Task<IActionResult> ReadSchedules([FromBody] AptScheduleFilter filter) {
		var schedulesQuery = _context.Schedules.AsQueryable();

		schedulesQuery = _context.Schedules.Where(x => x.BusinessId == filter.businessId);

		if (!string.IsNullOrWhiteSpace(filter.client)) {
			schedulesQuery = schedulesQuery.Where(x => x.Client.FullName.Contains(filter.client));
		}

		if (!string.IsNullOrWhiteSpace(filter.service)) {
			schedulesQuery = schedulesQuery.Where(x => x.Service != null && x.Service.Contains(filter.service));
		}

		schedulesQuery = schedulesQuery.Where(x => x.dateTime >= filter.minDateTime);
		schedulesQuery = schedulesQuery.Where(x => x.dateTime <= filter.maxDateTime);

		schedulesQuery = schedulesQuery.Where(x => x.Price >= filter.minPrice);
		schedulesQuery = schedulesQuery.Where(x => x.Price <= filter.maxPrice);

		switch (filter.sort) {
			case ScheduleSort.Client:
				schedulesQuery = schedulesQuery.OrderBy(s => s.Client.FullName).ThenByDescending(s => s.dateTime).ThenBy(s => s.Price).ThenBy(s => s.Id);
				break;
			case ScheduleSort.Price:
				schedulesQuery = schedulesQuery.OrderBy(s => s.Price).ThenByDescending(s => s.dateTime).ThenBy(s => s.Client.FullName).ThenBy(s => s.Id);
				break;
			case ScheduleSort.Date:
				schedulesQuery = schedulesQuery.OrderByDescending(s => s.dateTime).ThenBy(s => s.Price).ThenBy(s => s.Id);
				break;
		}

		if (filter.descending) {
			switch (filter.sort) {
				case ScheduleSort.Client:
					schedulesQuery = schedulesQuery.OrderByDescending(s => s.Client.FullName).ThenByDescending(s => s.dateTime).ThenBy(s => s.Price).ThenBy(s => s.Id);
					break;
				case ScheduleSort.Price:
					schedulesQuery = schedulesQuery.OrderByDescending(s => s.Price).ThenByDescending(s => s.dateTime).ThenBy(s => s.Client.FullName).ThenBy(s => s.Id);
					break;
				case ScheduleSort.Date:
					schedulesQuery = schedulesQuery.OrderBy(s => s.dateTime).ThenBy(s => s.Client.FullName).ThenBy(s => s.Price).ThenBy(s => s.Id);
					break;
			}
		}

		var resultsArray = await schedulesQuery
			.Skip(filter.offset)
			.Take(filter.limit)
			.ToArrayAsync();

		return Ok(resultsArray);
	}

	/// <summary>
	/// Creates an Appointment Schedule
	/// </summary>
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AptSchedule))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPost]
	public async Task<IActionResult> CreateSchedule([FromBody] CreateAptSchedule newAppointment) {

		var existingClient = _context.Clients.Find(newAppointment.ClientId);
		if (existingClient == null) {
			return BadRequest(NotExistErrors.Client);
		}

		var existingBusiness = _context.Business.Find(existingClient.BusinessId);

		if (existingBusiness!.allowListedServicesOnly) {
			if (newAppointment.Service == null || !existingBusiness.services.Contains(newAppointment.Service)) {
				return BadRequest(ValidatorErrors.UnlistedService);
			}
		}

		BusinessDay businessDay = existingBusiness.BusinessDays[(int)newAppointment.dateTime.DayOfWeek];
		if (businessDay == null) {
			return BadRequest(ValidatorErrors.TimeNotWithinBusinessHours);
		}
		else if (businessDay.Start.TimeOfDay < newAppointment.dateTime.TimeOfDay || businessDay.Finish.TimeOfDay > newAppointment.dateTime.TimeOfDay) {
			return BadRequest(ValidatorErrors.TimeNotWithinBusinessHours);
		}

		IdlePeriod[] idps = _context.IdlePeriods.Where(x => x.BusinessId == existingBusiness.Id).ToArray();
		foreach (IdlePeriod idp in idps) {
			if (idp.start <= newAppointment.dateTime && idp.finish >= newAppointment.dateTime) {
				return BadRequest(ValidatorErrors.DateWithinIdlePeriod);
			}
		}

		AptContact contact = _context.Contacts.Where(x => x.ClientId == newAppointment.ClientId)
								.Where(x => x.dateTime <= DateTime.Now).Where(x => x.dateTime >= DateTime.Now.AddDays(-1))
								.OrderByDescending(x => x.dateTime).FirstOrDefault()!;

		_context.Schedules.Add(new AptSchedule(newAppointment, existingBusiness.Id, contact != null));
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateSchedule), newAppointment);
	}

	/// <summary>
	/// Update the Appointment Schedule with the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptSchedule))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPut]
	public async Task<IActionResult> UpdateSchedule([FromBody] AptSchedule upSchedule) {

		var existingAppointment = _context.Schedules.Find(upSchedule.Id);
		if (existingAppointment == null) {
			return BadRequest(NotExistErrors.AptSchedule);
		}

		var existingBusiness = _context.Business.Find(existingAppointment.BusinessId);
		if (upSchedule.Service != null && existingBusiness!.allowListedServicesOnly) {
			if (!existingBusiness.services.Contains(upSchedule.Service)) {
				return BadRequest(ValidatorErrors.UnlistedService);
			}
		}

		BusinessDay businessDay = existingBusiness!.BusinessDays[(int)upSchedule.dateTime.DayOfWeek];
		if (businessDay == null) {
			return BadRequest(ValidatorErrors.TimeNotWithinBusinessHours);
		}
		else if (businessDay.Start.TimeOfDay < upSchedule.dateTime.TimeOfDay || businessDay.Finish.TimeOfDay > upSchedule.dateTime.TimeOfDay) {
			return BadRequest(ValidatorErrors.TimeNotWithinBusinessHours);
		}

		IdlePeriod[] idps = _context.IdlePeriods.Where(x => x.BusinessId == existingAppointment.BusinessId).ToArray();
		foreach (IdlePeriod idp in idps) {
			if (idp.start <= upSchedule.dateTime && idp.finish >= upSchedule.dateTime) {
				return BadRequest(ValidatorErrors.DateWithinIdlePeriod);
			}
		}

		AptContact contact = _context.Contacts.Where(x => x.ClientId == upSchedule.ClientId)
								.Where(x => x.dateTime <= DateTime.Now).Where(x => x.dateTime >= DateTime.Now.AddDays(-1))
								.OrderByDescending(x => x.dateTime).FirstOrDefault()!;

		upSchedule.WasContacted = contact != null;

		_context.Schedules.Update(upSchedule);
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
			return BadRequest(NotExistErrors.AptSchedule);
		}

		_context.Schedules.Remove(scheduleToDelete);

		AptLog[] logs = _context.Logs.Where(x => x.scheduleId == Id).ToArray();
		foreach (AptLog log in logs) {
			log.scheduleId = null;
		}
		_context.Logs.UpdateRange(logs);

		await _context.SaveChangesAsync();
		return NoContent();
	}
}
