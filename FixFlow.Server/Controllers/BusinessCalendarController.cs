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
	/// <returns>Business</returns>
	/// <response code="200">Successfull login</response>
	/// <response code="401">Unauthorized login</response>
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

		//TODO: insert idle periods
		//TODO: insert appointments

		return Ok(businessCalendarDays);
	}
}
