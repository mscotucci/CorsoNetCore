using TestWebApi.Entities;
using TestWebApi.Infrastructure.SearchCriteria;

namespace TestWebApi.Infrastructure;

public interface IDataBaseManager
{
    void CreateBook(Book book);
    Task CreateBookAsync(Book book);
    void DeleteBook(int bookId);
    Task DeleteBookAsync(int bookId);
    List<Author> GetAuthors();
    Task<List<Author>> GetAuthorsAsync();
    List<Book> GetBooks();
    Task<List<Book>> GetBooksAsync();
    Book? ReadBook(int id);
    Task<Book?> ReadBookAsync(int id);
    SearchResults<Book> SearchBooks(BooksSearchCriteria booksSearchCriteria);
    Task<SearchResults<Book>> SearchBooksAsync(BooksSearchCriteria booksSearchCriteria);
    SearchResults<Author> SearchAuthor(AuthorSearchCriteria authorSearchCriteria);
    void UpdateBook(Book book);
    Task UpdateBookAsync(Book book);
}
