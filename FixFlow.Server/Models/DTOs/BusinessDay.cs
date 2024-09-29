namespace Server.Models.DTO;

public class BusinessDay {

	public int dayOfTheWeek { get; set; }

	public DateTime start { get; set; }
	public DateTime end { get; set; }

	public BusinessDay() {
		dayOfTheWeek = 0;
		start = new DateTime(DateTime.Now.Year, 1, 1, 8, 0, 0);
		end = new DateTime(DateTime.Now.Year, 1, 1, 18, 0, 0);
	}

	public BusinessDay(int dayOfTheWeek, DateTime start, DateTime end) {
		this.dayOfTheWeek = dayOfTheWeek;
		this.start = start;
		this.end = end;
	}
}
