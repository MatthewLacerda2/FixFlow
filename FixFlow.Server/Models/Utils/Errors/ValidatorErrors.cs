namespace Server.Models.Erros;

public struct ValidatorErrors {

	public static readonly string TokenNotHaveBusinessId = "Your token does not have a BusinessId.";
	public static readonly string NullBusinessId = "Your token's BusinessId value is null.";
	public static readonly string NoOpenBusinessDay = "There must be at least one open Business Day.";

	public static readonly string UnlistedService = "The Business only allows listed services.";
	public static readonly string BusinessDayCountMustBe7 = "There must be exactly 7 business days";
	public static readonly string BusinessDayStartMustBeLessThanFinish = "Business Day start time must be less than finish time.";
	public static readonly string TimeNotWithinBusinessHours = "Time is not within Business Hours";

	public static readonly string DateMustBe2024orForward = "Date must be from 2024 and earlier.";
	public static readonly string DateIsTooFarInFuture = "Date is too far in the future.";
	public static readonly string DateMustNotBeInTheFuture = "Date hasn't even passed yet!";
	public static readonly string DateMustBeInTheFuture = "Date must be in the future.";
	public static readonly string MinDateMustBeOlder = "MinDate must be older than MaxDate.";
	public static readonly string MinDateIsGreaterThanMaxDate = "MinDate must be less than MaxDate.";
	public static readonly string DateWithinIdlePeriod = "The Date is within an Idle Period";
	public static readonly string IdlePeriodHasPassed = "Idle Period has passed.";
	public static readonly string StartMustBeOlderThanFinish = "Start time must be older than Finish time.";

	public static readonly string PriceMustBeNaturalNumber = "Price must be Greater Than or Equal to 0.";
	public static readonly string MinPriceIsGreaterThanMaxPrice = "MinPrice must be less than or equal to MaxPrice.";
	public static readonly string OffsetMustBeNaturalNumber = "Offset must bea natural number Greater Than or Equal to 0.";
	public static readonly string LimitMustBeNaturalNumberGreaterThanZero = "Limit must be a natural number greater than 0.";

	public static readonly string CPFisInvalid = "CPF is invalid.";
	public static readonly string CNPJisInvalid = "CNPJ is invalid.";
	public static readonly string FullName = "FullName is invalid.";
	public static readonly string UsernameIsEmpty = "UserName is empty!";
	public static readonly string ShortPassword = "Password must have at least 8 characters.";
	public static readonly string BadPassword = "Password must contain an upper case, lower case, number and special character.";
	public static readonly string ConfirmPassword = "ConfirmPassword must be identical to Password.";
	public static readonly string NewPasswordSameAsOldOne = "New password can not be the same as old one.";
}
