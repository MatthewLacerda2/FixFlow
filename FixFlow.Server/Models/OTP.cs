using System.ComponentModel.DataAnnotations;
using Server.Models.Utils;

namespace Server.Models;

public class OTP {

	public Guid Id { get; set; }

	[Length(6, 6)]
	public string Code { get; set; }

	[Phone]
	public string PhoneNumber { get; set; }

	public DateTime ExpiryTime { get; set; }
	public bool IsUsed { get; set; }
	public OTP_use_purpose Purpose { get; set; }

	public OTP() {
		PhoneNumber = "9888263255";
		Purpose = OTP_use_purpose.create_business;

		Id = new Guid();
		Code = new Random().Next(0, 1000000).ToString("D6");		
		ExpiryTime = DateTime.UtcNow.AddMinutes(Common.otpExpirationTimeInMinutes);
		IsUsed = false;
	}

	public OTP(string phoneNumber, OTP_use_purpose purpose){
		PhoneNumber = phoneNumber;
		Purpose = purpose;

		Id = new Guid();
		Code = new Random().Next(0, 1000000).ToString("D6");		
		ExpiryTime = DateTime.UtcNow.AddMinutes(Common.otpExpirationTimeInMinutes);
		IsUsed = false;
		Purpose = purpose;
	}
}

public enum OTP_use_purpose {
	//TODO: are we gonna send an OTP just for the new phone? or for the old phone and ask for confirmation?
	create_business, delete_business, change_phone
}
