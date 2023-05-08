namespace TestWebApi.Infrastructure.SearchCriteria;

public abstract class BaseSearchCriteria
{
    public BaseSearchCriteria(int page, int limit)
    {
        Page = Math.Max(1, page);
        Limit = Math.Max(1, limit);

        Offset = (Page - 1) * Limit;
    }
    public int Page { get; }

    public int Limit { get; }
    public int Offset { get; }
}
