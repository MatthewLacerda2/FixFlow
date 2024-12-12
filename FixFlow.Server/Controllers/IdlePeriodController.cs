using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.Erros;
using Server.Models.Utils;

namespace FixFlow.Server.Controllers;

/// <summary>
/// Controller class for Creating and Deleting Idle Periods
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
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdlePeriod))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpPost]
	public async Task<IActionResult> CreateIdlePeriod([FromBody] IdlePeriod idlePeriod) {

		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;

		if (idlePeriod.BusinessId != businessId) {
			return BadRequest(ValidatorErrors.BadIdlePeriodOwnership);
		}

		idlePeriod.Id = Guid.NewGuid().ToString();
		idlePeriod.Name = StringUtils.PhraseCaseNormalizer(idlePeriod.Name)!;

		_context.IdlePeriods.Add(idlePeriod);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(CreateIdlePeriod), idlePeriod);
	}

	/// <summary>
	/// Gets Idle Periods owned by the Company that start and end within a given time-period
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdlePeriod))]
	[HttpGet]
	public async Task<IActionResult> GetIdlePeriod(DateTime startDate, DateTime finishDate) {

		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;

		IdlePeriod[] periods = await _context.IdlePeriods
		.Where(i => i.Start >= startDate)
		.Where(i => i.Finish <= finishDate)
		.ToArrayAsync();

		return Ok(periods);
	}

	/// <summary>
	/// Deletes an Idle Period
	/// </summary>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
	[HttpDelete]
	public async Task<IActionResult> RemoveIdlePeriod([FromBody] string idlePeriodId) {

		var idlePeriod = _context.IdlePeriods.Find(idlePeriodId);
		if (idlePeriod == null) {
			return BadRequest(NotExistErrors.idlePeriod);
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
