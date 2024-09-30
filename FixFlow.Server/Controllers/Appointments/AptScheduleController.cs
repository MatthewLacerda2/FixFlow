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
			schedulesQuery = schedulesQuery.Where(x => x.service != null && x.service.Contains(filter.service));
		}

		schedulesQuery = schedulesQuery.Where(x => x.dateTime >= filter.minDateTime);
		schedulesQuery = schedulesQuery.Where(x => x.dateTime <= filter.maxDateTime);

		schedulesQuery = schedulesQuery.Where(x => x.price >= filter.minPrice);
		schedulesQuery = schedulesQuery.Where(x => x.price <= filter.maxPrice);

		switch (filter.sort) {
			case ScheduleSort.Client:
				schedulesQuery = schedulesQuery.OrderBy(s => s.Client.FullName).ThenByDescending(s => s.dateTime).ThenBy(s => s.price).ThenBy(s => s.Id);
				break;
			case ScheduleSort.Price:
				schedulesQuery = schedulesQuery.OrderBy(s => s.price).ThenByDescending(s => s.dateTime).ThenBy(s => s.Client.FullName).ThenBy(s => s.Id);
				break;
			case ScheduleSort.Date:
				schedulesQuery = schedulesQuery.OrderByDescending(s => s.dateTime).ThenBy(s => s.price).ThenBy(s => s.Id);
				break;
		}

		if (filter.descending) {
			switch (filter.sort) {
				case ScheduleSort.Client:
					schedulesQuery = schedulesQuery.OrderByDescending(s => s.Client.FullName).ThenByDescending(s => s.dateTime).ThenBy(s => s.price).ThenBy(s => s.Id);
					break;
				case ScheduleSort.Price:
					schedulesQuery = schedulesQuery.OrderByDescending(s => s.price).ThenByDescending(s => s.dateTime).ThenBy(s => s.Client.FullName).ThenBy(s => s.Id);
					break;
				case ScheduleSort.Date:
					schedulesQuery = schedulesQuery.OrderBy(s => s.dateTime).ThenBy(s => s.Client.FullName).ThenBy(s => s.price).ThenBy(s => s.Id);
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
	public async Task<IActionResult> CreateSchedule([FromBody] AptSchedule newAppointment) {

		var existingClient = _context.Clients.Find(newAppointment.ClientId);
		if (existingClient == null) {
			return BadRequest(NotExistErrors.Client);
		}

		var existingBusiness = _context.Business.Find(newAppointment.BusinessId);
		if (existingBusiness == null) {
			return BadRequest(NotExistErrors.Business);
		}

		if (existingBusiness.allowListedServicesOnly) {
			if (!existingBusiness.services.Contains(newAppointment.service)) {
				return BadRequest(ValidatorErrors.UnlistedService);
			}
		}

		AptContact contact = _context.Contacts.Where(x => x.ClientId == newAppointment.ClientId)
								.Where(x => x.dateTime <= DateTime.Now).Where(x => x.dateTime >= DateTime.Now.AddDays(-1))
								.OrderByDescending(x => x.dateTime).FirstOrDefault()!;

		newAppointment.wasContacted = contact != null;

		IdlePeriod[] idps = _context.IdlePeriods.Where(x => x.BusinessId == newAppointment.BusinessId).ToArray();
		foreach (IdlePeriod idp in idps) {
			if (idp.start <= newAppointment.dateTime && idp.finish >= newAppointment.dateTime) {
				return BadRequest(ValidatorErrors.DateWithinIdlePeriod);
			}
		}

		_context.Schedules.Add(newAppointment);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateSchedule), newAppointment);
	}

	/// <summary>
	/// Update the Appointment Schedule with the given Id
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AptSchedule))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPut]
	public async Task<IActionResult> UpdateSchedule([FromBody] AptSchedule upAppointment) {

		var existingAppointment = _context.Schedules.Find(upAppointment.Id);
		if (existingAppointment == null) {
			return BadRequest(NotExistErrors.AptSchedule);
		}

		IdlePeriod[] idps = _context.IdlePeriods.Where(x => x.BusinessId == upAppointment.BusinessId).ToArray();
		foreach (IdlePeriod idp in idps) {
			if (idp.start <= upAppointment.dateTime && idp.finish >= upAppointment.dateTime) {
				return BadRequest(ValidatorErrors.DateWithinIdlePeriod);
			}
		}

		_context.Schedules.Update(upAppointment);
		await _context.SaveChangesAsync();

		return Ok(upAppointment);
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
		await _context.SaveChangesAsync();
		return NoContent();
	}
}