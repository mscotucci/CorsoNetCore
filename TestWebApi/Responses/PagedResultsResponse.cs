namespace TestWebApi.Responses;

public class PagedResultsResponse<T>
{
    public int Page { get; private set; }
    public decimal PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / PageSize);
    public List<T> Results { get; private set; } = new List<T>();

    public PagedResultsResponse(int page, int pageSize, int totalCount, IEnumerable<T> results)
    {
        Page = Math.Max(1, page);
        PageSize = Math.Max(1, pageSize);
        TotalCount = totalCount;
        Results = results.ToList();
    }
}