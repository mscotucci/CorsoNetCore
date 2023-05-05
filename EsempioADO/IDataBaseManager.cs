namespace EsempioADO
{
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
        List<Book> GetBooksDisconnesso();
        void InitDb(bool createAuthors = false);
        Task InitDbAsync(bool createAuthors = false);
        Book? ReadBook(int id);
        Task<Book?> ReadBookAsync(int id);
        Task<Book?> ReadBookWithAuthorAsync(int id);
        Book? ReadBookWithAuthor(int id);
        SearchResults<Book> SearchBooks(string title);
        SearchResults<Book> SearchBooks(BooksSearchCriteria booksSearchCriteria);
        Task<SearchResults<Book>> SearchBooksAsync(BooksSearchCriteria booksSearchCriteria);
        Task<SearchResults<Book>> SearchBooksAsync(string title);
        SearchResults<Author> SearchAuthor(AuthorSearchCriteria authorSearchCriteria);
        Task<List<Author>> GetAuthorsHaveMoreBooks();
        void UpdateBook(Book book);
        Task UpdateBookAsync(Book book);
        void UpdateBookTitle(int bookId, string title);
        Task UpdateBookTitleAsync(int bookId, string title);
    }
}