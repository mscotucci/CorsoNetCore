using TestWebApi.Requests;

namespace BookStoreApi.Requests.Books;

public class SearchBookRequest : BaseSearchRequest
{
    public string? Title { get; set; }
    public DateTime? PublishDateStart { get; set; }
    public DateTime? PublishDateEnd { get; set; }
}
