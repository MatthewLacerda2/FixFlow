using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;
using Server.Models.Appointments;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Controllers;

[ApiController]
[Route(Common.api_v1 + nameof(BusinessCalendarDay))]
[Authorize]
[Produces("application/json")]
public class BusinessCalendarDayController : ControllerBase {

	private readonly ServerContext _context;

	public BusinessCalendarDayController(ServerContext context) {
		_context = context;
	}

	/// <summary>
	/// Gets all the events for this month
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusinessCalendarDay[]))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpGet]
	public async Task<IActionResult> GetBusinessMonthCalendar(string businessId, int year, int month) {

		bool businessExists = await _context.Business.FindAsync(businessId) != null;
		if (!businessExists) {
			return NotFound(NotExistErrors.Business);
		}

		int daysInMonth = DateTime.DaysInMonth(year, month);
		BusinessCalendarDay[] businessCalendarDays = new BusinessCalendarDay[daysInMonth];

		for (int i = 0; i < daysInMonth; i++) {
			BusinessCalendarDay aux = new BusinessCalendarDay();
			aux.date = new DateTime(year, month, i + 1).ToUniversalTime();

			aux.schedules = _context.Schedules.Where(x => x.BusinessId == businessId)
								.Where(x => x.dateTime.ToUniversalTime().Date == aux.date.ToUniversalTime().Date)
								.ToArray();

			aux.logs = _context.Logs.Where(x => x.BusinessId == businessId)
								.Where(x => x.dateTime.ToUniversalTime().Date == aux.date.ToUniversalTime().Date)
								.ToArray();

			aux.idlePeriods = _context.IdlePeriods.Where(x => x.BusinessId == businessId)
								.Where(x => x.start <= aux.date && x.finish >= aux.date)
								.ToArray();

			foreach (AptSchedule sched in aux.schedules) {
				sched.Customer = _context.Customers.Find(sched.CustomerId)!;
			}
			foreach (AptLog log in aux.logs) {
				log.Customer = _context.Customers.Find(log.CustomerId)!;
			}

			businessCalendarDays[i] = aux;
		}

		return Ok(businessCalendarDays);
	}
}
