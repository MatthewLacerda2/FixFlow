namespace Server.Models.DTO;

public class BusinessDay {

	public string Id { get; set; }

	public DateTime Start { get; set; }
	public DateTime Finish { get; set; }

	public bool IsOpen { get; set; } = true;

	public BusinessDay() : this(new DateTime(DateTime.Now.Year, 1, 1, 8, 0, 0), new DateTime(DateTime.Now.Year, 1, 1, 18, 0, 0)) { }

	public BusinessDay(DateTime start, DateTime end) {
		Id = Guid.NewGuid().ToString();
		Start = start;
		Finish = end;
		IsOpen = true;
	}
}
