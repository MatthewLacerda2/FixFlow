namespace Server.Models.DTO;

public class BusinessTimeSpan {

	public string Id { get; set; }

	public bool IsActive { get; set; }

	public TimeSpan Start { get; set; }
	public TimeSpan Finish { get; set; }

	public BusinessTimeSpan() : this(new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0)) { }

	public BusinessTimeSpan(TimeSpan start, TimeSpan finish) {
		Id = Guid.NewGuid().ToString();
		Start = start;
		Finish = finish;
	}
}
