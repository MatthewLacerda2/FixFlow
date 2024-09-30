namespace Server.Models.DTO;

public class BusinessDay {

	public DateTime Start { get; set; }
	public DateTime Finish { get; set; }

	public BusinessDay() : this(new DateTime(DateTime.Now.Year, 1, 1, 8, 0, 0), new DateTime(DateTime.Now.Year, 1, 1, 18, 0, 0)) { }

	public BusinessDay(DateTime start, DateTime end) {
		Start = start;
		Finish = end;
	}
}
