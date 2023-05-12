using Microsoft.EntityFrameworkCore;

namespace TestWebApi.Responses;

public class PagedResultsResponse<T>
{
    public int Page { get; private set; }
    public decimal PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / PageSize);
    public List<T> Results { get; set; } = new List<T>();

    private PagedResultsResponse(int page, int pageSize, int totalCount)
    {
        if (pageSize == 0) throw new ArgumentException("page size must be greater then zero");
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public static async Task<PagedResultsResponse<T>> CreateAsync(int page, int pageSize, int totalCount, IQueryable<T> query)
    {
        var instance = new PagedResultsResponse<T>(page, pageSize, totalCount);
        instance.Results = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
        return instance;
    }

    public static PagedResultsResponse<T> Create(int page, int pageSize, int totalCount, IQueryable<T> query)
    {
        var instance = new PagedResultsResponse<T>(page, pageSize, totalCount);
        instance.Results = query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();
        return instance;
    }
}