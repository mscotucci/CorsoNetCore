using EsempioADO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEFCore
{
    public class EFCoreDataBaseManager : IDataBaseManager
    {
        public void CreateBook(Book book)
        {
            using (var context=new BookStoreDbContext())
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
        }

        public Task CreateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(int bookId)
        {
            using (var context = new BookStoreDbContext())
            {
                var book = context.Books.FirstOrDefault(x=>x.Id == bookId);
                if(book != null)
                {
                    context.Books.Remove(book);
                }
                context.SaveChanges();
            }
        }

        public Task DeleteBookAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public List<Author> GetAuthors()
        {
            using(var context = new BookStoreDbContext())
            {
                return context.Authors.ToList();
            }
        }

        public Task<List<Author>> GetAuthorsAsync()
        {
            throw new NotImplementedException();
        }

        public List<Book> GetBooks()
        {
            using (var context = new BookStoreDbContext())
            {
                return context.Books.ToList();
            }
        }

        public Task<List<Book>> GetBooksAsync()
        {
            throw new NotImplementedException();
        }

        public List<Book> GetBooksDisconnesso()
        {
            throw new NotImplementedException();
        }

        public void InitDb(bool createAuthors = false)
        {
            var books = XMLDataSource.GetBooks();
            if (createAuthors)
            {
                using (var context = new BookStoreDbContext())
                {
                    var authors = books.GroupBy(x => x.Author).Select(x => x.Key).ToList();
                    foreach (var author in authors)
                    {
                        context.Authors.Add(new Author { Name = author });
                    }
                    context.SaveChanges();
                }
            }

            using (var context = new BookStoreDbContext())
            {
                var authors = context.Authors.ToList();
                foreach (var author in authors)
                {
                    var authorBooks = books.Where(x => x.Author == author.Name);
                    foreach (var book in authorBooks)
                    {
                        author.Books.Add(new Book
                        {
                            Title = book.Title,
                            Description = book.Description,
                            Price = book.Price,
                            Genre = Enum.Parse<Genre>(book.Genre),
                            PublishDate = book.PublishDate,
                            AuthorId = author.Id,
                        });
                    }
                }
                context.SaveChanges();
            }
        }

        public Task InitDbAsync(bool createAuthors = false)
        {
            throw new NotImplementedException();
        }

        public Book? ReadBook(int id)
        {
            using (var context = new BookStoreDbContext())
            {
                return context.Books.SingleOrDefault(x=>x.Id == id);
            }
        }

        public Task<Book?> ReadBookAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Book? ReadBookWithAuthor(int id)
        {
            using (var context = new BookStoreDbContext())
            {
                return context.Books.Include(x=>x.Author).SingleOrDefault(x=>x.Id==id);
            }
        }

        public Task<Book?> ReadBookWithAuthorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public SearchResults<Book> SearchBooks(string title)
        {
            using (var context = new BookStoreDbContext())
            {
                var query = context.Books.Where(x => x.Title.Contains(title));
                SearchResults<Book> searchResults = new SearchResults<Book>();
                searchResults.Count = query.Count();
                searchResults.Results = query.ToList();
                return searchResults;
            }
        }

        public SearchResults<Book> SearchBooks(BooksSearchCriteria booksSearchCriteria)
        {
            using (var context = new BookStoreDbContext())
            {
                var query = context.Books.Where(x => x.Title.Contains(booksSearchCriteria.Search))
                    .Skip(booksSearchCriteria.Offset)
                    .Take(booksSearchCriteria.Limit);
                SearchResults<Book> searchResults = new SearchResults<Book>();
                searchResults.Count = query.Count();
                searchResults.Results = query.ToList();
                return searchResults;
            }
        }

        public Task<SearchResults<Book>> SearchBooksAsync(BooksSearchCriteria booksSearchCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<SearchResults<Book>> SearchBooksAsync(string title)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            using (var context = new BookStoreDbContext())
            {
                var bookFromDb = context.Books.SingleOrDefault(x=>x.Id == book.Id);
                if(bookFromDb == null)
                {
                    throw new Exception($"book null con id={book.Id}");
                }
                bookFromDb.Title = book.Title;
                bookFromDb.AuthorId = book.AuthorId;
                bookFromDb.Description=book.Description;
                bookFromDb.Price=book.Price;
                bookFromDb.PublishDate=book.PublishDate;
                bookFromDb.Genre = book.Genre;
                context.SaveChanges();
            }
        }

        public Task UpdateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public void UpdateBookTitle(int bookId, string title)
        {
            using (var context = new BookStoreDbContext())
            {
                var bookFromDb = context.Books.SingleOrDefault(x => x.Id == bookId);
                if (bookFromDb == null)
                {
                    throw new Exception($"book null con id={bookId}");
                }
                bookFromDb.Title = title;
                context.SaveChanges();
            }
        }

        public Task UpdateBookTitleAsync(int bookId, string title)
        {
            throw new NotImplementedException();
        }
    }
}
