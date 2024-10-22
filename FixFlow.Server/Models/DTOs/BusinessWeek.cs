namespace Server.Models.DTO;

public class BusinessWeek {

	public string Id { get; set; }

	public BusinessTimeSpan Sunday { get; set; }

	public BusinessTimeSpan Monday { get; set; }
	public BusinessTimeSpan Tuesday { get; set; }
	public BusinessTimeSpan Wednesday { get; set; }
	public BusinessTimeSpan Thursday { get; set; }
	public BusinessTimeSpan Friday { get; set; }

	public BusinessTimeSpan Saturday { get; set; }

	public BusinessWeek() {

		Id = Guid.NewGuid().ToString();

		Sunday = new BusinessTimeSpan();
		Sunday.IsActive = false;

		Monday = new BusinessTimeSpan();
		Tuesday = new BusinessTimeSpan();
		Wednesday = new BusinessTimeSpan();
		Thursday = new BusinessTimeSpan();
		Friday = new BusinessTimeSpan();

		Saturday = new BusinessTimeSpan();
		Saturday.Finish = new TimeSpan(12, 0, 0);

	}

	public BusinessTimeSpan GetTimeForDayOfWeek(DayOfWeek dayOfWeek) {
		switch (dayOfWeek) {
			case DayOfWeek.Sunday:
				return Sunday;
			case DayOfWeek.Monday:
				return Monday;
			case DayOfWeek.Tuesday:
				return Tuesday;
			case DayOfWeek.Wednesday:
				return Wednesday;
			case DayOfWeek.Thursday:
				return Thursday;
			case DayOfWeek.Friday:
				return Friday;
			case DayOfWeek.Saturday:
				return Saturday;
		}
		return Sunday;
	}
}
