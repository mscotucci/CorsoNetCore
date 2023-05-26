using Microsoft.EntityFrameworkCore;
using TestWebApi.Application;
using TestWebApi.Application.Impl;
using TestWebApi.Entities;
using TestWebApi.Exceptions;
using TestWebApi.Infrastructure;
using TestWebApi.Requests.Books;

namespace ApplicatinServicesTest;

[TestClass]
public class BooksServiceTest
{
    //contesto per inizializzare i nostri dati fake
    private BookStoreDbContext _initContext;

    //contesto da utilizzare per i nostri test da passare al servizio
    private BookStoreDbContext _testContext;

    //contest che servirà pe verificare i dati
    private BookStoreDbContext _assertionContext;

    private IBooksService _entityService;

    [TestInitialize]
    public void Initialize()
    {
        var options = new DbContextOptionsBuilder<BookStoreDbContext>()
            .UseInMemoryDatabase("BookStore")
            .Options;
        _initContext = new BookStoreDbContext(options);
        _initContext.Database.EnsureCreated();

        _testContext = new BookStoreDbContext(options);

        _assertionContext = new BookStoreDbContext(options);

        _entityService = new BooksService(_testContext);

    }

    [TestMethod]
    public async Task CreateBookAsync_Should_CreateBookOnDatabase()
    {
        //Arrange
        var author = new Author { Name = "Test" };
        _initContext.Authors.Add(author);
        await _initContext.SaveChangesAsync();

        //Act
        CreateBookRequest createBookRequest = new CreateBookRequest
        {
            AuthorId = author.Id,
            Description="Description",
            Genre = Genre.ScienceFiction,
            Price=12,
            PublishDate=DateTime.Now,
            Title="Title",
        };
        var response = await _entityService.CreateBookAsync(createBookRequest);

        //Assert
        Assert.IsTrue(response.Id > 0);
        var book = await _assertionContext.Books.SingleOrDefaultAsync(x => x.Id == response.Id);
        Assert.IsNotNull(book);
    }

    [TestMethod]
    public async Task UpdateBookAsync_Should_ThrowBookNotFoundException()
    {
        //Arrange
        var author = new Author { Name = "Test" };
        author.Books.Add(new Book
        {
            AuthorId = author.Id,
            Description = "Description",
            Genre = Genre.ScienceFiction,
            Price = 12,
            PublishDate = DateTime.Now,
            Title = "Title",
        });
        _initContext.Authors.Add(author);
        await _initContext.SaveChangesAsync();

        //Act
        UpdateBookRequest updateBookRequest = new UpdateBookRequest
        {
            Id = 123
        };

        //Assertion
        await Assert.ThrowsExceptionAsync<BookNotFoundException>(async ()=>await _entityService
            .UpdateBookAsync(updateBookRequest));
    }

    [TestMethod]
    public async Task UpdateBookAsync_Should_UpdateBookOnDatabase()
    {
        //Arrange
        var author = new Author { Name = "Test" };
        author.Books.Add(new Book
        {
            AuthorId = author.Id,
            Description = "Description",
            Genre = Genre.ScienceFiction,
            Price = 12,
            PublishDate = DateTime.Now,
            Title = "Title",
        });
        _initContext.Authors.Add(author);
        await _initContext.SaveChangesAsync();
        var bookId = author.Books.FirstOrDefault().Id;

        //Act
        string description = "Descrizione modificata";
        UpdateBookRequest updateBookRequest = new UpdateBookRequest
        {
            Id = bookId,
            Description=description
        };

        await _entityService.UpdateBookAsync(updateBookRequest);

        //Assertion

        var book = await _assertionContext.Books.SingleOrDefaultAsync(x => x.Id == bookId);
        Assert.AreEqual(description, book.Description);
    }

    [TestMethod]
    public async Task DeleteBookAsync_Should_DeleteOnDatabase()
    {
        //Arrange
        var author = new Author { Name = "Autore" };
        author.Books.Add(new Book
        {
            AuthorId = author.Id,
            Description = "Description",
            Genre = Genre.ScienceFiction,
            Price = 12.9m,
            PublishDate = DateTime.Now,
            Title = "title"
        });
        _initContext.Authors.Add(author);
        await _initContext.SaveChangesAsync();
        var bookId = (await _initContext.Books.FirstOrDefaultAsync()).Id;

        //Act
        await _entityService.DeleteBookAsync(bookId);

        var book = await _assertionContext.Books.SingleOrDefaultAsync(x => x.Id == bookId);
        Assert.IsNull(book);
    }
}
