namespace Server.Models.DTO;

public class BusinessWeek {

	public string Id { get; set; }

	public BusinessTimeSpan sunday { get; set; }

	public BusinessTimeSpan monday { get; set; }
	public BusinessTimeSpan tuesday { get; set; }
	public BusinessTimeSpan wednesday { get; set; }
	public BusinessTimeSpan thursday { get; set; }
	public BusinessTimeSpan friday { get; set; }

	public BusinessTimeSpan saturday { get; set; }

	public BusinessWeek() {

		Id = Guid.NewGuid().ToString();

		sunday = new BusinessTimeSpan();

		monday = new BusinessTimeSpan();
		tuesday = new BusinessTimeSpan();
		wednesday = new BusinessTimeSpan();
		thursday = new BusinessTimeSpan();
		friday = new BusinessTimeSpan();

		saturday = new BusinessTimeSpan();

	}

	public BusinessTimeSpan GetTimeForDayOfWeek(DayOfWeek dayOfWeek) {
		switch (dayOfWeek) {
			case DayOfWeek.Sunday:
				return sunday;
			case DayOfWeek.Monday:
				return monday;
			case DayOfWeek.Tuesday:
				return tuesday;
			case DayOfWeek.Wednesday:
				return wednesday;
			case DayOfWeek.Thursday:
				return thursday;
			case DayOfWeek.Friday:
				return friday;
			case DayOfWeek.Saturday:
				return saturday;
		}
		return sunday;
	}
}
