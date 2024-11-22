namespace Server.Models.Erros;

public struct ValidatorErrors {

	public static readonly string TokenNotHaveBusinessId = "Seu token não possui um BusinessId.";
	public static readonly string NullBusinessId = "O valor do BusinessId no seu token é nulo.";
	public static readonly string NoOpenBusinessDay = "Deve haver pelo menos um dia útil aberto.";

	public static readonly string UnlistedService = "O negócio só permite serviços listados!";
	public static readonly string BusinessDayCountMustBe7 = "Devem haver exatamente 7 dias úteis!";
	public static readonly string BusinessDayStartMustBeLessThanFinish = "O horário de início do dia útil deve ser menor que o horário de término.";
	public static readonly string TimeNotWithinBusinessHours = "O horário está fora do horário de funcionamento.";

	public static readonly string DateMustBe2024orForward = "A data deve ser a partir de 2024 ou posterior.";
	public static readonly string DateIsTooFarInFuture = "A data está muito no futuro.";
	public static readonly string DateMustNotBeInTheFuture = "A data ainda não passou!";
	public static readonly string DateMustBeInTheFuture = "A data deve estar no futuro!";
	public static readonly string MinDateIsGreaterThanMaxDate = "A MinDate deve ser menor que a MaxDate!";
	public static readonly string DateWithinIdlePeriod = "A data está dentro de um período ocioso.";
	public static readonly string IdlePeriodHasPassed = "O período ocioso já passou.";
	public static readonly string StartMustBeOlderThanFinish = "O horário de início deve ser anterior ao horário de término.";

	public static readonly string PriceMustBeNaturalNumber = "O preço deve ser maior ou igual a 0!";
	public static readonly string MinPriceIsGreaterThanMaxPrice = "O MinPrice deve ser menor ou igual ao MaxPrice!";
	public static readonly string OffsetMustBeNaturalNumber = "O offset deve ser um número natural maior ou igual a 0!";
	public static readonly string LimitMustBeNaturalNumberGreaterThanZero = "O limite deve ser um número natural maior que 0.";

	public static readonly string CPFisInvalid = "O CPF é inválido!";
	public static readonly string CNPJisInvalid = "O CNPJ é inválido!";
	public static readonly string FullName = "O nome completo é inválido!";
	public static readonly string UsernameIsEmpty = "O nome de usuário está vazio!";
	public static readonly string ShortPassword = "A senha deve ter pelo menos 8 caracteres.";
	public static readonly string BadPassword = "A senha deve conter uma letra maiúscula, uma letra minúscula, um número e um caractere especial.";
	public static readonly string ConfirmPassword = "A confirmação de senha deve ser idêntica à senha!";
	public static readonly string NewPasswordSameAsOldOne = "A nova senha não pode ser igual à antiga.";
}
