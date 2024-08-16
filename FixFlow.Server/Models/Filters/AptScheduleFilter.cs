namespace Server.Models.Filters;

public struct AptScheduleFilter
{
    public string? clientId;
    public string? businessId;
    public bool hasContact;

    public float minPrice;
    public float maxPrice;
    public DateOnly minDateTime;
    public DateOnly maxDateTime;

    public ScheduleSort sort = ScheduleSort.Date;
    public bool descending = false;
    public int offset = 0;
    public int limit = 10;

    public AptScheduleFilter(string? _clientId, string? _businessId, DateOnly _minDate, DateOnly _maxDate){
        clientId = _clientId;
        businessId = _businessId;
        minDateTime = _minDate;
        maxDateTime = _maxDate;
    }
}

public enum ScheduleSort
{
    ClientId, BusinessId, Price, Date
}