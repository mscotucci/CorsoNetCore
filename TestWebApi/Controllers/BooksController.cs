using Microsoft.AspNetCore.Mvc;
using TestWebApi.Entities;
using TestWebApi.Infrastructure;
using TestWebApi.Requests.Books;
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
        public async Task<IActionResult> GetBooks()
        {
            var books = await _dataBaseManager.GetBooksAsync();
            return Ok(books);
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
