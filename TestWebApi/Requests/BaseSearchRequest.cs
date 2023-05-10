namespace TestWebApi.Requests;

public class BaseSearchRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
}
