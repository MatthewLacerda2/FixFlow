namespace Server.Models.Filters;
//TODO: It's validator
public struct AptLogFilter {

    public string? clientId;
    public string? businessId;
    //TODO: Add 'hasSchedule' and 'hasContact'. Those are important to calculate customer retention
    public float minPrice;
    public float maxPrice;
    public DateOnly minDateTime;
    public DateOnly maxDateTime;

    public LogSort sort = LogSort.Date;
    public bool descending = false;
    public int offset = 0;
    public int limit = 10;

    public AptLogFilter(string _clientId, string _businessId, float _minPrice, float _maxPrice, DateOnly _minDate, DateOnly _maxDate){
        clientId = _clientId;
        businessId = _businessId;
        minPrice = _minPrice;
        maxPrice = _maxPrice;
        minDateTime = _minDate;
        maxDateTime = _maxDate;
    }
}

public enum LogSort
{
    ClientId, BusinessId, Price, Date
}