using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;
using Server.Models.Erros;
using Server.Models.Utils;

namespace FixFlow.Server.Controllers.Users;

/// <summary>
/// Controller class for Creating, Editing and Deleting Idle Periods
/// </summary>
[ApiController]
[Route(Common.api_v1 + "IdlePeriods")]
[Produces("application/json")]
public class IdlePeriodController : ControllerBase {

	private readonly ServerContext _context;
	private readonly UserManager<Business> _userManager;

	public IdlePeriodController(ServerContext context, UserManager<Business> userManager) {
		_context = context;
		_userManager = userManager;
	}

	/// <summary>
	/// Creates an Idle period
	/// </summary>
	/// <param name="idlePeriod"></param>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	public async Task<IActionResult> CreateIdlePeriod(IdlePeriod idlePeriod) {

		if (_userManager.FindByIdAsync(idlePeriod.businessId) == null) {
			return BadRequest(NotExistErrors.Business);
		}

		for (DateOnly day = idlePeriod.start; day < idlePeriod.finish; day.AddDays(1)) {

			if (_context.IdleDays.Any(idleDay => idleDay.businessId == idlePeriod.businessId && idleDay.day == day)) {
				continue;
			}

			_context.IdleDays.Add(new IdleDay() {
				businessId = idlePeriod.businessId,
				day = day
			});
		}

		await _context.SaveChangesAsync();

		return Ok();
	}

	/// <summary>
	/// Removes Idle days
	/// </summary>
	[HttpDelete]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	public async Task<IActionResult> RemoveIdlePeriod(IdlePeriod idlePeriod) {

		if (_userManager.FindByIdAsync(idlePeriod.businessId) == null) {
			return BadRequest(NotExistErrors.Business);
		}

		foreach (IdleDay idleDay in _context.IdleDays.Where(idleDay => idleDay.businessId == idlePeriod.businessId)) {

			if (idlePeriod.start <= idleDay.day && idleDay.day <= idlePeriod.finish) {
				_context.Remove(idleDay);
			}
		}

		await _context.SaveChangesAsync();

		return NoContent();
	}
}
