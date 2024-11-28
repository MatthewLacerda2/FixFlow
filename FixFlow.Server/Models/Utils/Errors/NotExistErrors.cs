namespace Server.Models.Erros;

public struct NotExistErrors {

	private const string doesNotExist = " não existe.";

	public static readonly string AptLog = nameof(Appointments.AptLog) + doesNotExist;
	public static readonly string AptSchedule = nameof(Appointments.AptSchedule) + doesNotExist;
	public static readonly string Customer = nameof(Models.Customer) + doesNotExist;
	public static readonly string Business = nameof(Models.Business) + doesNotExist;
	public static readonly string IdlePeriod = nameof(Models.IdlePeriod) + doesNotExist;

}
