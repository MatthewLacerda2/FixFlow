using Server.Models.Appointments;

namespace Server.Models.Erros;

public struct NotExistErrors {

	private const string doesNotExist = " não existe.";

	public static readonly string aptContact = nameof(AptContact) + doesNotExist;
	public static readonly string aptSchedule = nameof(AptSchedule) + doesNotExist;
	public static readonly string aptLog = nameof(AptLog) + doesNotExist;
	public static readonly string customer = nameof(Customer) + doesNotExist;
	public static readonly string idlePeriod = nameof(IdlePeriod) + doesNotExist;
	public static readonly string business = nameof(Business) + doesNotExist;

}
