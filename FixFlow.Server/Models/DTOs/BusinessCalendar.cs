namespace Server.Models.DTO;

public class BusinessCalendar {
	public BusinessCalendarDay[] days = Array.Empty<BusinessCalendarDay>();
}

public class BusinessCalendarDay {
	public string[] events = Array.Empty<string>();
	public bool isIdlePeriod;
	public bool isHoliday;
}
