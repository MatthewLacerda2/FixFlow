namespace Server.Models.Filters;

public struct AptScheduleFilter
{
    public string ClientId;
    public float minPrice;
    public float maxPrice;
    public DateTime minDateTime;
    public DateTime maxDateTime;

    public ScheduleSort sort;
    public bool descending;
    public int offset;
    public int limit;
}

public enum ScheduleSort
{
    ClientId, price, date
}