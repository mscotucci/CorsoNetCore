namespace TestWebApi.Infrastructure.SearchCriteria;

public class AuthorSearchCriteria : BaseSearchCriteria
{
    public AuthorSearchCriteria(string name, int page, int limit) : base(page, limit)
    {
        Name = name;
    }

    public string Name { get; }
}
