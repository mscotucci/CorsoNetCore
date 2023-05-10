using BookStoreApi.Requests.Books;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestWebApi.Entities;
using TestWebApi.Infrastructure;
using TestWebApi.Infrastructure.SearchCriteria;
using TestWebApi.Requests.Books;
using TestWebApi.Responses;
using TestWebApi.Responses.Books;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IDataBaseManager _dataBaseManager;

        public BooksController(IDataBaseManager dataBaseManager)
        {
            _dataBaseManager = dataBaseManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] SearchBookRequest searchBookRequest)
        {
            BooksSearchCriteria booksSearchCriteria=new BooksSearchCriteria(searchBookRequest.Title,searchBookRequest.Page,searchBookRequest.PageSize);
            booksSearchCriteria.SetPublishDateStart(searchBookRequest.PublishDateStart);
            booksSearchCriteria.SetPublishDateEnd(searchBookRequest.PublishDateEnd);
            booksSearchCriteria.SortBy=searchBookRequest.SortBy;
            booksSearchCriteria.SortOrder=searchBookRequest.SortOrder;
            SearchResults<Book> booksSearchResults = await _dataBaseManager.SearchBooksAsync(booksSearchCriteria);

            PagedResultsResponse<BookModelResponse> response = new(
                searchBookRequest.Page, 
                searchBookRequest.PageSize,
                booksSearchResults.Count, 
                booksSearchResults.Results.Select(x => new BookModelResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    AuthorId = x.AuthorId,
                    Genre = x.Genre.ToString(),
                    Price = x.Price,
                    PublishDate = x.PublishDate
                }));
            
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleBook(int id)
        {
            Book? book = await _dataBaseManager.ReadBookAsync(id);
            if (book == null) 
            {
                return NotFound();
            }

            BookModelResponse bookResponse = new BookModelResponse();

            
            bookResponse.Id = book.Id;
            bookResponse.Title = book.Title;    
            bookResponse.Description = book.Description;
            bookResponse.AuthorId = book.AuthorId;  
            bookResponse.PublishDate = book.PublishDate;
            bookResponse.Price = book.Price;
            bookResponse.Genre = book.Genre.ToString();

            return Ok(bookResponse);
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest createBookRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Book book = new Book
            {
                AuthorId = createBookRequest.AuthorId,
                Description = createBookRequest.Description,
                Genre = Enum.Parse<Genre>(createBookRequest.Genre),
                Price = createBookRequest.Price,
                PublishDate = createBookRequest.PublishDate,
                Title = createBookRequest.Title,
            };
            await _dataBaseManager.CreateBookAsync(book);
            var response = new BookModelResponse
            {
                Id = book.Id,
                AuthorId = book.AuthorId,
                Description = book.Description,
                Price = book.Price,
                Title = book.Title,
                PublishDate = book.PublishDate,
                Genre = book.Genre.ToString()
            };
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest updateBookRequest)
        {
            var book = await _dataBaseManager.ReadBookAsync(updateBookRequest.Id);
            if (book == null)
            {
                return NotFound();
            }
            book.AuthorId = updateBookRequest.AuthorId ?? book.AuthorId;
            book.PublishDate = updateBookRequest.PublishDate ?? book.PublishDate;
            book.Description = updateBookRequest.Description ?? book.Description;
            book.Price = updateBookRequest.Price ?? book.Price;
            if (Enum.TryParse<Genre>(updateBookRequest.Genre, out Genre genre))
            {
                book.Genre = genre;
            }
            book.Title = updateBookRequest.Title ?? book.Title;
            await _dataBaseManager.UpdateBookAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _dataBaseManager.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
