namespace Server.Models.Filters;

public struct AptLogFilter
{
    public string? ClientId;
    public float minPrice;
    public float maxPrice;
    public DateTime minDateTime;
    public DateTime maxDateTime;

    public LogSort sort;
    public bool descending;
    public int offset;
    public int limit;
}

public enum LogSort
{
    ClientId, price, date
}