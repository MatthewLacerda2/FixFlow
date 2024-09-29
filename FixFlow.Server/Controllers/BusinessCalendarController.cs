using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Controllers;

[ApiController]
[Route(Common.api_v1 + nameof(BusinessCalendarDay))]
[Produces("application/json")]
public class BusinessCalendarController : ControllerBase {

	private readonly ServerContext _context;

	public BusinessCalendarController(ServerContext context) {
		_context = context;
	}

	/// <summary>
	/// Login with an email and password
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusinessCalendarDay[]))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpGet]
	public async Task<IActionResult> GetBusinessCalendar([FromBody] string businessId) {

		bool businessExists = await _context.Business.FindAsync(businessId) != null;
		if (!businessExists) {
			return NotFound(NotExistErrors.Business);
		}

		int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
		BusinessCalendarDay[] businessCalendarDays = new BusinessCalendarDay[daysInMonth];

		int i = 0;
		foreach (BusinessCalendarDay day in businessCalendarDays) {
			day.date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
			i++;

			day.schedules = _context.Schedules.Where(x => x.BusinessId == businessId)
							.Where(x => x.dateTime == day.date)
							.ToArray();

			day.logs = _context.Logs.Where(x => x.BusinessId == businessId)
							.Where(x => x.dateTime == day.date)
							.ToArray();

			day.idlePeriods = _context.IdlePeriods.Where(x => x.businessId == businessId)
								.Where(x => x.isDateWithinIdlePeriod(day.date)).ToArray();
		}

		return Ok(businessCalendarDays);
	}
}
