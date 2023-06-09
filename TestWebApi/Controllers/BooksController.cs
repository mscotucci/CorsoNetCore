﻿using BookStoreApi.Requests.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Application;
using TestWebApi.Exceptions;
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
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] SearchBookRequest searchBookRequest)
        {
            PagedResultsResponse<BookModelResponse> booksSearchResults = await _booksService.SearchBooksAsync(searchBookRequest);
            
            return Ok(booksSearchResults);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleBook(int id)
        {
            BookModelResponse? bookResponse = await _booksService.ReadBookAsync(id);
            if (bookResponse == null) 
            {
                return NotFound();
            }
            return Ok(bookResponse);
        }
        

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest createBookRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _booksService.CreateBookAsync(createBookRequest);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest updateBookRequest)
        {
            try
            {
                await _booksService.UpdateBookAsync(updateBookRequest);
                return NoContent();
            }
            catch (BookNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _booksService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
