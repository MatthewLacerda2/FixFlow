namespace Server.Models.Filters;

public struct AptContactFilter
{
    public string? clientId;
    public string? businessId;
    public DateOnly minDateTime;
    public DateOnly maxDateTime;

    public ContactSort sort = ContactSort.Date;
    public bool descending = false;
    public int offset = 0;
    public int limit = 10;

    public AptContactFilter(string? _clientId, string? _businessId, string? _aptLogId, DateOnly _minDate, DateOnly _maxDate) {
        clientId = _clientId;
        businessId = _businessId;
        minDateTime = _minDate;
        maxDateTime = _maxDate;
    }
}

public enum ContactSort
{
    ClientId, BusinessId, Date
}