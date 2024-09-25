namespace Server.Models.Filters;

public class AptLogFilter {

	public string businessId = string.Empty;

	public string? client;
	public string? service;

	public float minPrice;
	public float maxPrice;

	public DateOnly minDateTime;
	public DateOnly maxDateTime;

	public LogSort sort = LogSort.Date;

	public bool descending = false;
	public int offset = 0;
	public int limit = 10;

}

public enum LogSort {
	Client, Price, Date
}
