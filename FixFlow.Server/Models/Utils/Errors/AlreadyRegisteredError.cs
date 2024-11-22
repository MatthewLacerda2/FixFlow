namespace Server.Models.Erros;

public struct AlreadyRegisteredErrors {

	private const string alreadyRegistered = " já existe.";

	public static readonly string Email = "Email" + alreadyRegistered;
	public static readonly string CPF = "CPF" + alreadyRegistered;
	public static readonly string PhoneNumber = "PhoneNumber" + alreadyRegistered;
	public static readonly string CNPJ = "CNPJ" + alreadyRegistered;
	public static readonly string UserName = "UserName" + alreadyRegistered;

}
