using BookStoreApi.Requests.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Requests.Books;
using TestWebApi.Responses;
using TestWebApi.Responses.Books;

namespace TestWebApi.Application
{
    public interface IBooksService
    {
        BookModelResponse CreateBook(CreateBookRequest createBookRequest);
        Task<BookModelResponse> CreateBookAsync(CreateBookRequest createBookRequest);
        void DeleteBook(int bookId);
        Task DeleteBookAsync(int bookId);
        BookModelResponse? ReadBook(int id);
        Task<BookModelResponse?> ReadBookAsync(int id);
        PagedResultsResponse<BookModelResponse> SearchBooks(SearchBookRequest searchBookRequest);
        Task<PagedResultsResponse<BookModelResponse>> SearchBooksAsync(SearchBookRequest searchBookRequest);
        void UpdateBook(UpdateBookRequest updateBookRequest);
        Task UpdateBookAsync(UpdateBookRequest updateBookRequest);
    }
}
