using System.ComponentModel.DataAnnotations;
using Server.Models.Utils;

namespace Server.Models;

public class OTP {

	public string Id { get; set; }

	[Length(6, 6)]
	public string Code { get; set; }

	[Phone]
	public string PhoneNumber { get; set; }

	public DateTime ExpiryTime { get; set; }
	public bool IsUsed { get; set; }

	public OTP() {
		PhoneNumber = "9888263255";

		Id = Guid.NewGuid().ToString();
		Code = new Random().Next(0, 1000000).ToString("D6");
		ExpiryTime = DateTime.UtcNow.AddMinutes(Common.otpExpirationTimeInMinutes);
		IsUsed = false;
	}

	public OTP(string phoneNumber) {
		PhoneNumber = phoneNumber;

		Id = Guid.NewGuid().ToString();
		Code = new Random().Next(0, 1000000).ToString("D6");
		ExpiryTime = DateTime.UtcNow.AddMinutes(Common.otpExpirationTimeInMinutes);
		IsUsed = false;
	}
}
