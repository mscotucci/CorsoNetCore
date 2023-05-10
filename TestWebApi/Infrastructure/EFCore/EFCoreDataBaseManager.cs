using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Entities;
using TestWebApi.Infrastructure.SearchCriteria;

namespace TestWebApi.Infrastructure.EFCore;

public class EFCoreDataBaseManager : IDataBaseManager
{
    private readonly BookStoreDbContext _context;

    public EFCoreDataBaseManager(BookStoreDbContext context)
    {
        _context = context;
    }

    public void CreateBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public async Task CreateBookAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
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

    public List<Author> GetAuthors()
    {
        return _context.Authors.ToList();
    }

    public async Task<List<Author>> GetAuthorsAsync()
    {
        return await _context.Authors.ToListAsync();
    }

    public List<Book> GetBooks()
    {
        return _context.Books.ToList();
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }
    public Book? ReadBook(int id)
    {
        return _context.Books.SingleOrDefault(x => x.Id == id);
    }

    public async Task<Book?> ReadBookAsync(int id)
    {
        return await _context.Books.SingleOrDefaultAsync(x => x.Id == id);
    }

    public SearchResults<Book> SearchBooks(BooksSearchCriteria booksSearchCriteria)
    {
        var query = _context.Books.Where(x => x.Title.Contains(booksSearchCriteria.Search));
        if (booksSearchCriteria.PublishDateStart != null)
        {
            query = query.Where(x => x.PublishDate >= booksSearchCriteria.PublishDateStart);
        }
        if (booksSearchCriteria.PublishDateEnd != null)
        {
            query = query.Where(x => x.PublishDate <= booksSearchCriteria.PublishDateEnd);
        }
        if (booksSearchCriteria.SortBy != null)
        {
            bool asc = booksSearchCriteria.SortOrder == "asc" || false;
            switch (booksSearchCriteria.SortBy)
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
        var pagedQuery = query
            .Skip(booksSearchCriteria.Offset)
            .Take(booksSearchCriteria.Limit);
        SearchResults<Book> searchResults = new SearchResults<Book>();
        searchResults.Count = query.Count();
        searchResults.Results = pagedQuery.ToList();
        return searchResults;
    }

    public async Task<SearchResults<Book>> SearchBooksAsync(BooksSearchCriteria booksSearchCriteria)
    {
        var query = _context.Books.Where(x => x.Title.Contains(booksSearchCriteria.Search));
        if (booksSearchCriteria.PublishDateStart != null)
        {
            query = query.Where(x => x.PublishDate >= booksSearchCriteria.PublishDateStart);
        }
        if (booksSearchCriteria.PublishDateEnd != null)
        {
            query = query.Where(x => x.PublishDate <= booksSearchCriteria.PublishDateEnd);
        }
        if (booksSearchCriteria.SortBy != null)
        {
            bool asc = booksSearchCriteria.SortOrder == null || booksSearchCriteria.SortOrder == "asc" || false;
            switch (booksSearchCriteria.SortBy)
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
        var pagedQuery = query
            .Skip(booksSearchCriteria.Offset)
            .Take(booksSearchCriteria.Limit);
        SearchResults<Book> searchResults = new SearchResults<Book>();
        searchResults.Count = await query.CountAsync();
        searchResults.Results = await pagedQuery.ToListAsync();
        return searchResults;
    }

    public SearchResults<Author> SearchAuthor(AuthorSearchCriteria criteria)
    {
        var query = _context.Authors.Where(x => x.Name.Contains(criteria.Name));
        SearchResults<Author> searchResults = new SearchResults<Author>();
        searchResults.Count = query.Count();
        searchResults.Results = query.ToList();
        return searchResults;
    }

    public void UpdateBook(Book book)
    {
        var bookFromDb = _context.Books.SingleOrDefault(x => x.Id == book.Id);
        if (bookFromDb == null)
        {
            throw new Exception($"book null con id={book.Id}");
        }
        bookFromDb.Title = book.Title;
        bookFromDb.AuthorId = book.AuthorId;
        bookFromDb.Description = book.Description;
        bookFromDb.Price = book.Price;
        bookFromDb.PublishDate = book.PublishDate;
        bookFromDb.Genre = book.Genre;
        _context.SaveChanges();
    }

    public async Task UpdateBookAsync(Book book)
    {
        var bookFromDb = await _context.Books.SingleOrDefaultAsync(x => x.Id == book.Id);
        if (bookFromDb == null)
        {
            throw new Exception($"book null con id={book.Id}");
        }
        bookFromDb.Title = book.Title;
        bookFromDb.AuthorId = book.AuthorId;
        bookFromDb.Description = book.Description;
        bookFromDb.Price = book.Price;
        bookFromDb.PublishDate = book.PublishDate;
        bookFromDb.Genre = book.Genre;
        await _context.SaveChangesAsync();
    }
}
