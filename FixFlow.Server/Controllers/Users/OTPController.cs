using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;
using Server.Models.Utils;
//TODO: check if the phonenumber is valid
namespace FixFlow.Server.Controllers;

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
	[ProducesResponseType(StatusCodes.Status200OK)]
	[HttpPost]
	public async Task<IActionResult> CreateBusinessOTP([FromBody] string phoneNumber) {
		OTP otp = new OTP(phoneNumber);

		_context.OTPs.Add(otp);
		await _context.SaveChangesAsync();

		return Ok();
	}

}
