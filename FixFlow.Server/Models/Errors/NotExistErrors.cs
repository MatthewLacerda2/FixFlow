using Server.Models.Appointments;

namespace Server.Models.Erros;

public struct NotExistErrors {

	private const string doesNotExist = " does not exist.";

	public static readonly string AptContact = nameof(Appointments.AptContact) + doesNotExist;
	public static readonly string AptLog = nameof(Appointments.AptLog) + doesNotExist;
	public static readonly string AptSchedule = nameof(Appointments.AptSchedule) + doesNotExist;
	public static readonly string Client = nameof(Models.Client) + doesNotExist;
	public static readonly string Business = nameof(Models.Business) + doesNotExist;

}
