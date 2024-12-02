using Server.Models.Appointments;
using Server.Models.DTO;

namespace Server.Models.Erros;

public struct NotExistErrors {

	private const string doesNotExist = " não existe.";

	public static readonly string aptContact = nameof(AptContact) + doesNotExist;
	public static readonly string aptSchedule = nameof(AptSchedule) + doesNotExist;
	public static readonly string aptLog = nameof(AptLog) + doesNotExist;
	public static readonly string customer = nameof(Customer) + doesNotExist;
	public static readonly string idlePeriod = nameof(IdlePeriod) + doesNotExist;
	public static readonly string business = nameof(Business) + doesNotExist;
	public static readonly string businessWeek = nameof(BusinessWeek) + doesNotExist;
	public static readonly string businessTimeSpan = "One or more TimeSpan ID's are not valid";


}
