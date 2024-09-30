using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models.DTO;
using Server.Models.Erros;
using Server.Models.Utils;

namespace Server.Controllers;

[ApiController]
[Route(Common.api_v1 + nameof(BusinessCalendarDay))]
[Produces("application/json")]
public class BusinessCalendarDayController : ControllerBase {

	private readonly ServerContext _context;

	public BusinessCalendarDayController(ServerContext context) {
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

		for (int i = 0; i < daysInMonth; i++) {
			BusinessCalendarDay aux = new BusinessCalendarDay();
			aux.date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i + 1);

			aux.schedules = _context.Schedules.Where(x => x.BusinessId == businessId)
							.Where(x => x.dateTime == aux.date)
							.ToArray();

			aux.logs = _context.Logs.Where(x => x.BusinessId == businessId)
							.Where(x => x.dateTime == aux.date)
							.ToArray();

			aux.idlePeriods = _context.IdlePeriods.Where(x => x.BusinessId == businessId)
							.Where(x => x.start <= aux.date && x.finish >= aux.date).ToArray();

			businessCalendarDays[i] = aux;
		}

		return Ok(businessCalendarDays);
	}
}