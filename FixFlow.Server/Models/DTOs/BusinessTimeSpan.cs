namespace Server.Models.DTO;

public class BusinessTimeSpan {

	public string Id { get; set; }

	/// <summary>
	/// Whether or not we are open that day at all
	/// </summary>
	/// <remarks>
	/// If this is false we ignore the 'Start' and 'Finish'.
	/// They are still saved because if we re-open that day, we get the last commercial hours available right away
	/// </remarks>
	public bool IsActive { get; set; }

	public TimeSpan Start { get; set; }
	public TimeSpan Finish { get; set; }

	public BusinessTimeSpan() : this(new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0)) { }

	public BusinessTimeSpan(TimeSpan start, TimeSpan finish) {
		Id = Guid.NewGuid().ToString();
		IsActive = true;
		Start = start;
		Finish = finish;
	}
}
