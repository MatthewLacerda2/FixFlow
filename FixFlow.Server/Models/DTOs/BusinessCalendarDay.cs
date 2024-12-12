using Newtonsoft.Json;
using Server.Models.Appointments;

namespace Server.Models.DTO;

public class BusinessCalendarDay {

	[JsonIgnore]
	public DateTime date { get; set; }

	public IdlePeriod[] idlePeriods { get; set; } = Array.Empty<IdlePeriod>();
	public string[] holiday { get; set; } = Array.Empty<string>();

	public AptSchedule[] schedules { get; set; } = Array.Empty<AptSchedule>();
	public AptLog[] logs { get; set; } = Array.Empty<AptLog>();

	public BusinessCalendarDay() {
		idlePeriods = Array.Empty<IdlePeriod>();
		holiday = Array.Empty<string>();

		schedules = Array.Empty<AptSchedule>();
		logs = Array.Empty<AptLog>();
	}
}
