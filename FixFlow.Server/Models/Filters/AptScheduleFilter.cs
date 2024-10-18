namespace Server.Models.Filters;

public class AptScheduleFilter {

	public string businessId { get; set; } = string.Empty;

	public string? client { get; set; }
	public string? service { get; set; }

	public float minPrice { get; set; }
	public float maxPrice { get; set; }

	public DateTime minDateTime { get; set; }
	public DateTime maxDateTime { get; set; }

	public ScheduleSort sort { get; set; }

	public bool descending { get; set; }
	public int offset { get; set; }
	public int limit { get; set; }

	public AptScheduleFilter() {
		minPrice = 0;
		maxPrice = 1000;
		descending = true;
	}

	public AptScheduleFilter(string businessId, ScheduleSort sort, DateTime minDateTime, DateTime maxDateTime) {
		this.businessId = businessId;
		this.sort = sort;
		this.minDateTime = minDateTime;
		this.maxDateTime = maxDateTime;

		minPrice = 0;
		maxPrice = 1000;
		descending = true;
	}
}

public enum ScheduleSort {
	Customer, Price, Date
}
