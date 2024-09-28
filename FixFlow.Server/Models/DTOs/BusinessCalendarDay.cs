using Server.Models.Appointments;

namespace Server.Models.DTO;

public class BusinessCalendarDay {

	public bool isIdlePeriod;
	public string[] holiday = Array.Empty<string>();

	public AptSchedule[] schedules = Array.Empty<AptSchedule>();
	public AptLog[] logs = Array.Empty<AptLog>();

}
