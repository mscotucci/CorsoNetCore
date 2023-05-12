using BookStoreApi.Requests.Books;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Entities;
using TestWebApi.Exceptions;
using TestWebApi.Infrastructure;
using TestWebApi.Requests.Books;
using TestWebApi.Responses;
using TestWebApi.Responses.Books;

namespace TestWebApi.Application.Impl
{
    public class BooksService : IBooksService
    {
        private readonly BookStoreDbContext _context;

        public BooksService(BookStoreDbContext dbContext)
        {
            _context = dbContext;
        }

        public BookModelResponse CreateBook(CreateBookRequest createBookRequest)
        {
            var book = new Book
            {
                AuthorId = createBookRequest.AuthorId,
                Description = createBookRequest.Description,
                Genre = Enum.Parse<Genre>(createBookRequest.Genre),
                Price = createBookRequest.Price,
                PublishDate = createBookRequest.PublishDate,
                Title = createBookRequest.Title,
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            var response = new BookModelResponse
            {
                Id = book.Id,
                AuthorId = book.AuthorId,
                Description = book.Description,
                Title = book.Title,
                Genre = book.Genre.ToString(),
                Price = book.Price,
                PublishDate = book.PublishDate,
            };

            return response;
        }

        public async Task<BookModelResponse> CreateBookAsync(CreateBookRequest createBookRequest)
        {
            var book = new Book
            {
                AuthorId = createBookRequest.AuthorId,
                Description = createBookRequest.Description,
                Genre = Enum.Parse<Genre>(createBookRequest.Genre),
                Price = createBookRequest.Price,
                PublishDate = createBookRequest.PublishDate,
                Title = createBookRequest.Title,
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var response = new BookModelResponse
            {
                Id = book.Id,
                AuthorId = book.AuthorId,
                Description = book.Description,
                Title = book.Title,
                Genre = book.Genre.ToString(),
                Price = book.Price,
                PublishDate = book.PublishDate,
            };

            return response;
        }

        public void DeleteBook(int bookId)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            _context.SaveChanges();
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            await _context.SaveChangesAsync();
        }

        public BookModelResponse? ReadBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book != null)
            {
                var response = new BookModelResponse
                {
                    Id = book.Id,
                    AuthorId = book.AuthorId,
                    Description = book.Description,
                    Genre = book.Genre.ToString(),
                    Price = book.Price,
                    PublishDate = book.PublishDate,
                    Title = book.Title,
                };
                return response;
            }
            return null;
        }

        public async Task<BookModelResponse?> ReadBookAsync(int id)
        {
            var book = await _context.Books.SingleOrDefaultAsync(x => x.Id == id);
            if (book != null)
            {
                var response = new BookModelResponse
                {
                    Id = book.Id,
                    AuthorId = book.AuthorId,
                    Description = book.Description,
                    Genre = book.Genre.ToString(),
                    Price = book.Price,
                    PublishDate = book.PublishDate,
                    Title = book.Title,
                };
                return response;
            }
            return null;
        }

        public PagedResultsResponse<BookModelResponse> SearchBooks(SearchBookRequest searchBookRequest)
        {
            IQueryable<Book> query = _context.Books;
            if (searchBookRequest.Title != null)
            {
                query = query.Where(x => x.Title.Contains(searchBookRequest.Title));
            }
            if (searchBookRequest.PublishDateStart != null)
            {
                query = query.Where(x => x.PublishDate >= searchBookRequest.PublishDateStart);
            }
            if (searchBookRequest.PublishDateEnd != null)
            {
                query = query.Where(x => x.PublishDate <= searchBookRequest.PublishDateEnd);
            }
            if (searchBookRequest.SortBy != null)
            {
                bool asc = searchBookRequest.SortOrder == null || searchBookRequest.SortOrder == "asc" || false;
                switch (searchBookRequest.SortBy)
                {
                    case "title":
                        query = asc ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title);
                        break;
                    case "publishDate":
                        query = asc ? query.OrderBy(x => x.PublishDate) : query.OrderByDescending(x => x.PublishDate);
                        break;
                    case "price":
                        query = asc ? query.OrderBy(x => x.Price) : query.OrderByDescending(x => x.Price);
                        break;
                    default:
                        query = asc ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title);
                        break;
                }
            }
            int totalCount = query.Count();

            var pagedQuery = query.Select(book => new BookModelResponse
            {
                Id = book.Id,
                AuthorId = book.AuthorId,
                Description = book.Description,
                Genre = book.Genre.ToString(),
                Price = book.Price,
                PublishDate = book.PublishDate,
                Title = book.Title,
            });

            PagedResultsResponse<BookModelResponse> searchResults =PagedResultsResponse<BookModelResponse>.Create(
                searchBookRequest.Page,
                searchBookRequest.PageSize,
                 totalCount,
                 pagedQuery);

            return searchResults;
        }

        public async Task<PagedResultsResponse<BookModelResponse>> SearchBooksAsync(SearchBookRequest searchBookRequest)
        {
           var query = _context.Books.AsQueryable();
            if (searchBookRequest.Title != null)
            {
                query = query.Where(x => x.Title.Contains(searchBookRequest.Title));
            }
            if (searchBookRequest.PublishDateStart != null)
            {
                query = query.Where(x => x.PublishDate >= searchBookRequest.PublishDateStart);
            }
            if (searchBookRequest.PublishDateEnd != null)
            {
                query = query.Where(x => x.PublishDate <= searchBookRequest.PublishDateEnd);
            }
            if (searchBookRequest.SortBy != null)
            {
                bool asc = searchBookRequest.SortOrder == null || searchBookRequest.SortOrder == "asc" || false;
                switch (searchBookRequest.SortBy)
                {
                    case "title":
                        query = asc ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title);
                        break;
                    case "publishDate":
                        query = asc ? query.OrderBy(x => x.PublishDate) : query.OrderByDescending(x => x.PublishDate);
                        break;
                    case "price":
                        query = asc ? query.OrderBy(x => x.Price) : query.OrderByDescending(x => x.Price);
                        break;
                    default:
                        query = asc ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title);
                        break;
                }
            }
            
            int totalCount = await query.CountAsync();

            var pagedQuery = query.Select(book => new BookModelResponse
            {
                Id = book.Id,
                AuthorId = book.AuthorId,
                Description = book.Description,
                Genre = book.Genre.ToString(),
                Price = book.Price,
                PublishDate = book.PublishDate,
                Title = book.Title,
            });

            PagedResultsResponse<BookModelResponse> searchResults = await PagedResultsResponse<BookModelResponse>.CreateAsync(
                searchBookRequest.Page,
                searchBookRequest.PageSize,
                 totalCount,
                 pagedQuery);

            return searchResults;
        }

        public void UpdateBook(UpdateBookRequest updateBookRequest)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == updateBookRequest.Id);
            if (book == null)
            {
                throw new BookNotFoundException($"book null con id={updateBookRequest.Id}");
            }

            book.AuthorId = updateBookRequest.AuthorId ?? book.AuthorId;
            book.PublishDate = updateBookRequest.PublishDate ?? book.PublishDate;
            book.Description = updateBookRequest.Description ?? book.Description;
            book.Price = updateBookRequest.Price ?? book.Price;
            book.Genre = Enum.TryParse<Genre>(updateBookRequest.Genre, out Genre genre) ? genre : book.Genre;
            book.Title = updateBookRequest.Title ?? book.Title;

            _context.SaveChanges();
        }

        public async Task UpdateBookAsync(UpdateBookRequest updateBookRequest)
        {
            var book = await _context.Books.SingleOrDefaultAsync(x => x.Id == updateBookRequest.Id);
            if (book == null)
            {
                throw new BookNotFoundException($"book null con id={updateBookRequest.Id}");
            }

            book.AuthorId = updateBookRequest.AuthorId ?? book.AuthorId;
            book.PublishDate = updateBookRequest.PublishDate ?? book.PublishDate;
            book.Description = updateBookRequest.Description ?? book.Description;
            book.Price = updateBookRequest.Price ?? book.Price;
            book.Genre = Enum.TryParse<Genre>(updateBookRequest.Genre, out Genre genre) ? genre : book.Genre;
            book.Title = updateBookRequest.Title ?? book.Title;

            await _context.SaveChangesAsync();
        }
    }
}
