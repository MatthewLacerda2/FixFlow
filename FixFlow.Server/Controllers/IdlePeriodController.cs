using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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
[Authorize]
[Produces("application/json")]
public class IdlePeriodController : ControllerBase {

	private readonly ServerContext _context;

	public IdlePeriodController(ServerContext context) {
		_context = context;
	}

	/// <summary>
	/// Creates an Idle period
	/// </summary>
	/// <remarks>
	/// Idle Periods are allowed to overlap
	/// </remarks>
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdlePeriod))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPost]
	public async Task<IActionResult> CreateIdlePeriod([FromBody] IdlePeriod idlePeriod) {

		idlePeriod.Id = Guid.NewGuid().ToString();
		_context.IdlePeriods.Add(idlePeriod);

		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateIdlePeriod), idlePeriod);
	}

	/// <summary>
	/// Removes Idle days
	/// </summary>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpDelete]
	public async Task<IActionResult> RemoveIdlePeriod([FromBody] string idlePeriodId) {

		var idlePeriod = _context.IdlePeriods.Find(idlePeriodId);
		if (idlePeriod == null) {
			return BadRequest(NotExistErrors.IdlePeriod);
		}

		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;
		if (idlePeriod.BusinessId != businessId) {
			return Unauthorized(ValidatorErrors.BadIdlePeriodOwnership);
		}

		_context.IdlePeriods.Remove(idlePeriod);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
