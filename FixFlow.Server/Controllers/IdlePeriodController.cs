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
	private readonly UserManager<Business> _userManager;

	public IdlePeriodController(ServerContext context, UserManager<Business> userManager) {
		_context = context;
		_userManager = userManager;
	}

	/// <summary>
	/// Returns all Idle Periods that contain the given date
	/// </summary>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdlePeriod[]))]
	public async Task<IActionResult> GetIdlePeriodsAtDate([FromBody] DateTime date) {
		return Ok(await _context.IdlePeriods.Where(ip => ip.isDateWithinIdlePeriod(date)).ToArrayAsync());
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

		if (_userManager.FindByIdAsync(idlePeriod.businessId) == null) {
			return BadRequest(NotExistErrors.Business);
		}

		if (idlePeriod.finish <= DateTime.Now) {
			return BadRequest(ValidatorErrors.IdlePeriodHasPassed);
		}

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
