namespace Server.Models.Filters;

public class AptScheduleFilter {

	public string businessId = string.Empty;

	public string? client;
	public string? service;

	public float minPrice;
	public float maxPrice;

	public DateTime minDateTime;
	public DateTime maxDateTime;

	public ScheduleSort sort = ScheduleSort.Date;

	public bool descending = false;
	public int offset = 0;
	public int limit = 10;
}

public enum ScheduleSort {
	Client, Price, Date
}
