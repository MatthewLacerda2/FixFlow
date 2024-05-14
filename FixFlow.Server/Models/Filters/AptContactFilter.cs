namespace Server.Models.Filters;

public struct AptContactFilter
{
    public string? ClientId;
    public DateTime minDateTime;
    public DateTime maxDateTime;

    public ContactSort sort;
    public bool descending;
    public int offset;
    public int limit;
}

public enum ContactSort
{
    ClientId, date
}