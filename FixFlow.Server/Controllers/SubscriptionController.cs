using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Models.Utils;

namespace FixFlow.Server.Controllers;

/// <summary>
/// Controller class Subscription stuff
/// </summary>
[ApiController]
[Route(Common.api_v1 + nameof(Subscription))]
[Authorize]
[Produces("application/json")]
public class SubscriptionController : ControllerBase {

	private readonly ServerContext _context;

	public SubscriptionController(ServerContext context) {
		_context = context;
	}

	/// <summary>
	/// Gets Idle Periods owned by the Company that start and end within a given time-period
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Subscription[]))]
	[HttpGet]
	public async Task<IActionResult> GetSubscriptions(int startMonth, int startYear, int endMonth, int endYear) {

		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;

		Subscription[] subs = await _context.Subscriptions.Where(s => s.BusinessId == businessId)
		.Where(s => s.dateTime.Year >= startYear && s.dateTime.Month >= startMonth)
		.Where(s => s.dateTime.Year <= endYear && s.dateTime.Month <= endMonth)
		.ToArrayAsync();

		return Ok(subs);
	}

	/// <summary>
	/// Deletes an Idle Period
	/// </summary>
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Subscription[]))]
	[HttpGet("unpayed")]
	public async Task<IActionResult> GetUnpayedSubscriptions() {
		string businessId = User.Claims.First(c => c.Type == "businessId")?.Value!;
		var subs = await _context.Subscriptions.Where(s => s.Payed == false).ToArrayAsync();
		return Ok(subs);
	}
}
