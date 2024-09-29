namespace Server.Models.Erros;

public struct ValidatorErrors {

	public static readonly string UnlistedService = "The Business only allows listed services.";
	public static readonly string BusinessDaysInvalidMatrix = "BusinessDays must be a [2x7] matrix.";

	public static readonly string TokenNotHaveBusinessId = "Your token does not have a BusinessId.";
	public static readonly string NullBusinessId = "Your token's BusinessId value is null.";

	public static readonly string InvalidOTP = "Código OTP inválido";

	public static readonly string MinPriceIsGreaterThanMaxPrice = "MinPrice must be less than or equal to MaxPrice.";

	public static readonly string DateMustBe2024orForward = "Date must be from 2024 and earlier.";
	public static readonly string DateIsTooFarInFuture = "Date is too far in the future.";
	public static readonly string DateHasntPassedYet = "Date hasn't even passed yet!";
	public static readonly string DateMustBeInTheFuture = "Date must be in the future.";

	public static readonly string IdlePeriodHasPassed = "Idle Period has passed.";

	public static readonly string MinDateMustBeOlder = "MinDate must be older than MaxDate.";
	public static readonly string MinDateIsGreaterThanMaxDate = "MinDate must be less than MaxDate.";
	public static readonly string StartMustBeOlderThanFinish = "Start time must be older than Finish time.";

	public static readonly string DateWithinIdlePeriod = "The Date is within an Idle Period";

	public static readonly string PriceMustBeNaturalNumber = "Price must be Greater Than or Equal to 0.";
	public static readonly string OffsetMustBeNaturalNumber = "Offset must be Greater Than or Equal to 0.";
	public static readonly string CPFisInvalid = "CPF is invalid.";
	public static readonly string CNPJisInvalid = "CNPJ is invalid.";
	public static readonly string UsernameIsEmpty = "UserName is empty!";
	public static readonly string ShortPassword = "Password must have at least 8 characters.";
	public static readonly string BadPassword = "Password must contain an upper case, lower case, number and special character.";
	public static readonly string ConfirmPassword = "ConfirmPassword must be identical to Password.";
	public static readonly string FullName = "FullName is invalid.";
	public static readonly string NewPasswordSameAsOldOne = "New password can not be the same as old one.";

}
