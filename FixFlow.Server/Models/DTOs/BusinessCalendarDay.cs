using Newtonsoft.Json;
using Server.Models.Appointments;

namespace Server.Models.DTO;

public class BusinessCalendarDay {

	[JsonIgnore]
	public DateTime date;

	public string description = string.Empty;

	public IdlePeriod[] idlePeriods = Array.Empty<IdlePeriod>();
	public string[] holiday = Array.Empty<string>();

	public AptSchedule[] schedules = Array.Empty<AptSchedule>();
	public AptLog[] logs = Array.Empty<AptLog>();

}
