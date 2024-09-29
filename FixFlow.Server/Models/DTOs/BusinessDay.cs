namespace Server.Models.DTO;

public class BusinessDay {

	public DateTime Start { get; set; }
	public DateTime Finish { get; set; }

	public BusinessDay() {
		Start = new DateTime(DateTime.Now.Year, 1, 1, 8, 0, 0);
		Finish = new DateTime(DateTime.Now.Year, 1, 1, 18, 0, 0);
	}

	public BusinessDay(int dayOfTheWeek, DateTime start, DateTime end) {
		this.Start = start;
		this.Finish = end;
	}
}
