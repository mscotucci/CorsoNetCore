using BookStoreApi.Requests.Books;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Entities;
using TestWebApi.Exceptions;
using TestWebApi.Infrastructure;
using TestWebApi.Requests.Authors;
using TestWebApi.Responses;
using TestWebApi.Responses.Authors;

namespace TestWebApi.Application.Impl
{
    public class AuthorsService : IAuthorsService
    {
        private readonly BookStoreDbContext _dbContext;

        public AuthorsService(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public AuthorModelResponse CreateAuthor(CreateAuthorRequest createAuthorRequest)
        {
            var author = new Author
            {
                Name = createAuthorRequest.Nome
            };
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
            var response = new AuthorModelResponse
            {
                Id = author.Id,
                Nome = author.Name,
            };
            return response;
        }

        public async Task<AuthorModelResponse> CreateAuthorAsync(CreateAuthorRequest createAuthorRequest)
        {
            var author = new Author
            {
                Name = createAuthorRequest.Nome
            };
            _dbContext.Authors.Add(author);
            await _dbContext.SaveChangesAsync();
            var response = new AuthorModelResponse
            {
                Id = author.Id,
                Nome = author.Name,
            };
            return response;
        }

        public void DeleteAuthor(int id)
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == id);
            if (author != null)
            {
                _dbContext.Authors.Remove(author);
            }
            _dbContext.SaveChanges();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _dbContext.Authors.SingleOrDefaultAsync(x => x.Id == id);
            if (author != null)
            {
                _dbContext.Authors.Remove(author);
            }
            await _dbContext.SaveChangesAsync();
        }

        public AuthorModelResponse? ReadAuthor(int id)
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == id);
            if(author != null)
            {
                var response = new AuthorModelResponse
                {
                    Id = author.Id,
                    Nome = author.Name,
                };
                return response;
            }
            return null;
        }

        public async Task<AuthorModelResponse?> ReadAuthorAsync(int id)
        {
            var author = await _dbContext.Authors.SingleOrDefaultAsync(x => x.Id == id);
            if (author != null)
            {
                var response = new AuthorModelResponse
                {
                    Id = author.Id,
                    Nome = author.Name,
                };
                return response;
            }
            return null; 
        }

        public PagedResultsResponse<AuthorModelResponse> SearchAuthors(SearchAuthorRequest searchAuthorRequest)
        {
            var query = _dbContext.Authors.AsQueryable<Author>();
            if (searchAuthorRequest.Nome != null)
            {
                query = query.Where(x => x.Name.Contains(searchAuthorRequest.Nome));
            }
            int totalCount = query.Count();

            var results = query.Select(author => new AuthorModelResponse
            {
                Id = author.Id,
                Nome = author.Name,
            });

            PagedResultsResponse<AuthorModelResponse> searchResults = PagedResultsResponse<AuthorModelResponse>.Create(
                searchAuthorRequest.Page,
                searchAuthorRequest.PageSize,
                totalCount,
                results
            );
            return searchResults;
        }

        public async Task<PagedResultsResponse<AuthorModelResponse>> SearchAuthorsAsync(SearchAuthorRequest searchAuthorRequest)
        {
            var query = _dbContext.Authors.AsQueryable<Author>();
            if (searchAuthorRequest.Nome != null)
            {
                query = query.Where(x => x.Name.Contains(searchAuthorRequest.Nome));
            }
            int totalCount = await query.CountAsync();

            var results = query.Select(author => new AuthorModelResponse
            {
                Id = author.Id,
                Nome = author.Name,
            });

            PagedResultsResponse<AuthorModelResponse> searchResults = await PagedResultsResponse<AuthorModelResponse>.CreateAsync(
                searchAuthorRequest.Page,
                searchAuthorRequest.PageSize,
                totalCount,
                results
            );
            return searchResults;
        }

        public void UpdateAuthor(UpdateAuthorRequest updateAuthorRequest)
        {
            var author = _dbContext.Authors.SingleOrDefault(x =>x.Id == updateAuthorRequest.Id);
            if (author ==null)
            {
                throw new AuthorNotFoundException($"author null con id={updateAuthorRequest.Id}");
            }
                author.Name = updateAuthorRequest.Nome ?? author.Name;
                _dbContext.SaveChanges();
        }

        public async Task UpdateAuthorAsync(UpdateAuthorRequest updateAuthorRequest)
        {
            var author = await _dbContext.Authors.SingleOrDefaultAsync(x => x.Id == updateAuthorRequest.Id);
            if (author == null)
            {
                throw new AuthorNotFoundException($"author null con id={updateAuthorRequest.Id}");
            }
            author.Name = updateAuthorRequest.Nome ?? author.Name;
            await _dbContext.SaveChangesAsync();
        }
    }
}
