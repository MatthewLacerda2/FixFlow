namespace Server.Models.DTO;

public class BusinessDay {

	public string Id { get; set; }

	public DateTime Start { get; set; } = DateTime.UtcNow;
	public DateTime Finish { get; set; } = DateTime.UtcNow.AddHours(10);

	public bool IsOpen { get; set; } = true;

	public BusinessDay() : this(DateTime.UtcNow, DateTime.UtcNow.AddHours(10)) { }

	public BusinessDay(DateTime start, DateTime end) {
		Id = Guid.NewGuid().ToString();
		Start = DateTime.SpecifyKind(start, DateTimeKind.Utc);
		Finish = DateTime.SpecifyKind(end, DateTimeKind.Utc);
		IsOpen = true;
	}
}
