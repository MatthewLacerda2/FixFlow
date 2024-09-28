using Server.Models.Appointments;

namespace Server.Models.Erros;

public struct ValidatorErrors {

	public static readonly string DateMustBe2024orForward = "Date must be from 2024 and earlier.";
	public static readonly string DateIsTooFarInFuture = "Date is too far in the future.";
	public static readonly string DateHasntPassedYet = "Date hasn't even passed yet!";
	public static readonly string MinDateMustBeOlder = "MinDate must be older than MaxDate";

	public static readonly string ClientIdRequired = "ClientId is required.";
	public static readonly string BusinessIdRequired = "BusinessId is required.";
	public static readonly string AptLogRequired = nameof(AptLog) + " is required.";

	public static readonly string PriceMustBeNaturalNumber = "Price must be Greater Than or Equal to Zero.";

	public static readonly string CPFisInvalid = "CPF is invalid.";
	public static readonly string serviceLength = "Service name must have at most 63 characters.";
	public static readonly string businessDayLength = "There must be exactly a start and a finish Time for the 7 days of the week.";
	public static readonly string CNPJisInvalid = "CNPJ is invalid.";
	public static readonly string UsernameIsEmpty = "UserName is empty!";
	public static readonly string UsernameHasWhitespaces = "Username cannot contain whitespaces.";
	public static readonly string ShortPassword = "Password must have at least 8 characters.";
	public static readonly string BadPassword = "Password must contain an upper case, lower case, number and special character.";
	public static readonly string ConfirmPassword = "ConfirmPassword must be identical to Password.";
	public static readonly string FullName = "FullName is invalid.";
	public static readonly string NewPasswordSameAsOldOne = "New password can not be the same as old one.";

}
