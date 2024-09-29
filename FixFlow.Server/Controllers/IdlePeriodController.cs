using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.Erros;
using Server.Models.Utils;

namespace FixFlow.Server.Controllers.Users;

/// <summary>
/// Controller class for Creating, Extending, Shrinking and Deleting Idle Periods
/// </summary>
[ApiController]
[Route(Common.api_v1 + nameof(IdlePeriod))]
[Produces("application/json")]
public class IdlePeriodController : ControllerBase {

	private readonly ServerContext _context;

	public IdlePeriodController(ServerContext context) {
		_context = context;
	}

	/// <summary>
	/// Returns all Idle Periods that contain the given date
	/// </summary>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdlePeriod[]))]
	public async Task<IActionResult> GetIdlePeriodsAtDate([FromBody] BusinessIdlePeriodsRequest period) {

		var business = _context.Business.Find(period.BusinessId);
		if (business == null) {
			return BadRequest(NotExistErrors.Business);
		}

		var idlePeriods = await _context.IdlePeriods.Where(x => x.BusinessId == period.BusinessId)
								.Where(x => x.start <= period.Date && x.finish >= period.Date)
								.ToArrayAsync();

		return Ok(idlePeriods);
	}

	/// <summary>
	/// Creates an Idle period
	/// </summary>
	/// <remarks>
	/// Idle Periods are allowed to overlap
	/// </remarks>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdlePeriod))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	public async Task<IActionResult> CreateIdlePeriod([FromBody] IdlePeriod idlePeriod) {

		if (_context.Business.Find(idlePeriod.BusinessId) == null) {
			return BadRequest(NotExistErrors.Business);
		}

		if (idlePeriod.finish <= DateTime.Now) {
			return BadRequest(ValidatorErrors.IdlePeriodHasPassed);
		}

		idlePeriod.Id = Guid.NewGuid().ToString();
		_context.IdlePeriods.Add(idlePeriod);

		await _context.SaveChangesAsync();

		return Ok(idlePeriod);
	}

	/// <summary>
	/// Removes Idle days
	/// </summary>
	[HttpDelete]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	public async Task<IActionResult> RemoveIdlePeriod([FromBody] string idlePeriodId) {

		var idlePeriod = _context.IdlePeriods.Find(idlePeriodId);
		if (idlePeriod == null) {
			return BadRequest(NotExistErrors.IdlePeriod);
		}

		_context.IdlePeriods.Remove(idlePeriod);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
