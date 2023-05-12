using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TestWebApi.Responses;

public class PagedResultsResponse<T>
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (decimal)PageSize);
    public List<T> Results { get; private set; } = new List<T>();

    private PagedResultsResponse(int page, int pageSize, int totalCount)
    {
        Page = Math.Max(1, page);
        PageSize = Math.Max(1, pageSize);
        TotalCount = totalCount;
    }

    public static async Task<PagedResultsResponse<T>> CreateAsync(int page, int pageSize, int totalCount, IQueryable<T> query)
    {
        var instance = new PagedResultsResponse<T>(page, pageSize, totalCount);
        instance.Results = await query
        .Skip((instance.Page - 1) * instance.PageSize)
        .Take(instance.PageSize)
        .ToListAsync();
        return instance;
    }

    public static PagedResultsResponse<T> Create(int page, int pageSize, int totalCount, IQueryable<T> query)
    {
        var instance = new PagedResultsResponse<T>(page, pageSize, totalCount);
        instance.Results = query
        .Skip((instance.Page - 1) * instance.PageSize)
        .Take(instance.PageSize)
        .ToList();
        return instance;
    }
}