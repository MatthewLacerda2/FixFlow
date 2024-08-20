namespace Server.Models.Filters;

public struct AptLogFilter {

	public string? clientId;
	public string? businessId;
	public bool? hasSchedule;
	//TODO: implementar hasContact;

	public float minPrice;
	public float maxPrice;
	public DateOnly minDateTime;
	public DateOnly maxDateTime;

	public LogSort sort = LogSort.Date;
	public bool descending = false;
	public int offset = 0;
	public int limit = 10;

	public AptLogFilter(string _clientId, string _businessId, bool? _hasSchedule, float _minPrice, float _maxPrice, DateOnly _minDate, DateOnly _maxDate) {
		clientId = _clientId;
		businessId = _businessId;
		hasSchedule = _hasSchedule;
		minPrice = _minPrice;
		maxPrice = _maxPrice;
		minDateTime = _minDate;
		maxDateTime = _maxDate;
	}
}

public enum LogSort {
	ClientId, BusinessId, Price, Date
}
