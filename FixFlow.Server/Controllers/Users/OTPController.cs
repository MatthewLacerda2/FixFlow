using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;
using Server.Models.Utils;

namespace FixFlow.Server.Controllers.Users;

/// <summary>
/// Controller class for OTP usage
/// </summary>
[ApiController]
[Route(Common.api_v1 + nameof(OTP))]
[Produces("application/json")]
public class OTPController : ControllerBase {

	private readonly ServerContext _context;

	public OTPController(ServerContext context) {
		_context = context;
	}

	/// <summary>
	/// Creates an OTP for when creating a Business
	/// </summary>
	/// <param name="phoneNumber"></param>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Business))]
	public IActionResult CreateBusinessOTP(string phoneNumber) {

		OTP otp = new OTP(phoneNumber, OTP_use_purpose.create_business);

		_context.Add(otp);
		_context.SaveChanges();

		return Ok();
	}
}
