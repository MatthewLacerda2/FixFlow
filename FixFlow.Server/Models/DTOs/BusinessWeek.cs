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
}
