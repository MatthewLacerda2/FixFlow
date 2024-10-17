namespace Server.Models.Filters;

public class AptLogFilter {

	public string businessId = string.Empty;

	public string? client { get; set; }
	public string? service { get; set; }

	public float minPrice { get; set; }
	public float maxPrice { get; set; }

	public DateTime minDateTime { get; set; }
	public DateTime maxDateTime { get; set; }

	public LogSort sort = LogSort.Date;

	public bool descending = false;
	public int offset { get; set; }
	public int limit { get; set; }

	public AptLogFilter() {
		minPrice = 0;
		maxPrice = 1000;
		descending = true;
	}

	public AptLogFilter(string businessId, LogSort sort, DateTime minDateTime, DateTime maxDateTime) {
		this.businessId = businessId;
		this.sort = sort;
		this.minDateTime = minDateTime;
		this.maxDateTime = maxDateTime;

		minPrice = 0;
		maxPrice = 1000;
		descending = true;
	}
}

public enum LogSort {
	Customer, Price, Date
}
