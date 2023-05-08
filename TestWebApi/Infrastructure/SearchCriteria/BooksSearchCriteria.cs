namespace TestWebApi.Infrastructure.SearchCriteria;

public class BooksSearchCriteria : BaseSearchCriteria
{

    public BooksSearchCriteria(string search, int page, int limit) : base(page, limit)
    {
        Search = search ?? "";
    }
    public string Search { get; }

    public DateTime? PublishDateStart { get; private set; }

    public DateTime? PublishDateEnd { get; private set; }

    public void SetPublishDateStart(DateTime? publishDateStart)
    {
        if (PublishDateEnd != null && publishDateStart != null && publishDateStart > PublishDateEnd)
        {
            throw new ArgumentException($"{nameof(publishDateStart)} deve essere maggiore di {nameof(PublishDateEnd)}");
        }
        PublishDateStart = publishDateStart;
    }

    public void SetPublishDateEnd(DateTime? publishDateEnd)
    {
        if (PublishDateEnd != null && publishDateEnd != null && publishDateEnd < PublishDateStart)
        {
            throw new ArgumentException($"{nameof(publishDateEnd)} deve essere maggiore di {nameof(PublishDateStart)}");
        }
        PublishDateEnd = publishDateEnd;
    }

}
