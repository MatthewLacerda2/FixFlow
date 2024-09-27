using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

		if (idlePeriod.finish <= DateTime.Now) {
			return BadRequest("Idle Period end has passed");
		}

		foreach (IdlePeriod idp in _context.IdlePeriods.Where(idp => idp.businessId == idlePeriod.businessId)) {
			if (idp.start >= idlePeriod.start) {
				if (idp.start <= idlePeriod.finish) {
					return BadRequest("Idle Period already exists");
				}
				else {
					idp.finish = idlePeriod.start;
					await _context.SaveChangesAsync();
					return Ok();
				}
			}
			else if (idlePeriod.finish <= idp.start) {
				idp.start = idlePeriod.finish;
				await _context.SaveChangesAsync();
				return Ok();
			}
		}

		_context.IdlePeriods.Add(idlePeriod);
		await _context.SaveChangesAsync();

		return Ok();
	}

	/// <summary>
	/// Removes Idle days
	/// </summary>
	[HttpDelete]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	public async Task<IActionResult> RemoveIdlePeriod([FromBody] string idlePeriodId) {

		var idlePeriod = _context.IdlePeriods.Find(idlePeriodId);
		if (idlePeriod == null) {
			return BadRequest("Idle Period does not exist");
		}

		_context.IdlePeriods.Remove(idlePeriod);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
