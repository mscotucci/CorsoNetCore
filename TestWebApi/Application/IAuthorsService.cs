using TestWebApi.Requests.Authors;
using TestWebApi.Responses.Authors;
using TestWebApi.Responses;

namespace TestWebApi.Application
{
    public interface IAuthorsService
    {
        AuthorModelResponse CreateAuthor(CreateAuthorRequest createAuthorRequest);
        Task<AuthorModelResponse> CreateAuthorAsync(CreateAuthorRequest createAuthorRequest);
        void DeleteAuthor(int id);
        Task DeleteAuthorAsync(int id);
        AuthorModelResponse? ReadAuthor(int id);
        Task<AuthorModelResponse?> ReadAuthorAsync(int id);
        PagedResultsResponse<AuthorModelResponse> SearchAuthors(SearchAuthorRequest searchAuthorRequest);
        Task<PagedResultsResponse<AuthorModelResponse>> SearchAuthorsAsync(SearchAuthorRequest searchAuthorRequest);
        void UpdateAuthor(UpdateAuthorRequest updateAuthorRequest);
        Task UpdateAuthorAsync(UpdateAuthorRequest updateAuthorRequest);
    }
}
