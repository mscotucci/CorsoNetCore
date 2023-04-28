using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsempioADO
{
    public class DataBaseManager
    {
        private readonly string _connectionString;

        public DataBaseManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InitDb(bool createAuthors=false)
        {
            var books = XMLDataSource.GetBooks();
            using SqlConnection conn = GetOpenedConnection();
            using SqlCommand cmd = conn.CreateCommand();
            int affectedRows = 0;
            if (createAuthors)
            {
                var authors = books.GroupBy(x => x.Author).Select(x => x.Key).ToList();
                foreach (var author in authors)
                {
                    string insertAuthorCommand = "INSERT INTO Authors(Name) VALUES(@Name)";
                    cmd.CommandText = insertAuthorCommand;
                    cmd.Parameters.AddWithValue("@Name", author);
                    affectedRows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    if (affectedRows != 1)
                    {
                        throw new Exception($"Si è verificato un problema in inserimento author {author}");
                    }
                }
            }
            var authorsFromDb = GetAuthors();

            foreach (var book in books)
            {
                var authorId = authorsFromDb.SingleOrDefault(x => x.Name == book.Author).Id;
                string insertCommand = "INSERT INTO Books(AuthorId, Title,Genre,Price,PublishDate,Description) VALUES(@AuthorId,@Title,@Genre,@Price,@PublishDate,@Description);";
                cmd.CommandText = insertCommand;
                cmd.Parameters.Add(new SqlParameter("@Title", book.Title));
                cmd.Parameters.Add(new SqlParameter("@AuthorId", authorId));
                cmd.Parameters.Add(new SqlParameter("@Genre", Enum.Parse<Genre>(book.Genre).ToString()));
                cmd.Parameters.Add(new SqlParameter("@Price", book.Price));
                cmd.Parameters.Add(new SqlParameter("@PublishDate", book.PublishDate));
                cmd.Parameters.Add(new SqlParameter("@Description", book.Description));
                try
                {
                    affectedRows = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex) when (ex.ErrorCode == 7978987987987987)//ErrorCode inventato!!!!!!!!!!
                {
                    //operazioni in caso di questa tipologia di errori
                }
                catch (Exception)
                {
                    throw;
                }
                cmd.Parameters.Clear();
                if (affectedRows != 1)
                {
                    throw new Exception($"Si è verificato un errore in inserimento book {book.Title}");
                }
            }
        }
        public List<Book> GetBooks()
        {
            var books = new List<Book>();
            string command = "SELECT * FROM Books";
            using SqlConnection conn = GetOpenedConnection();
            using SqlCommand cmd = GetSqlCommand(conn, command);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Book book = new Book();
                book.Id = reader.GetInt32("Id");
                book.Title = reader.GetString("Title");
                book.PublishDate = reader.GetDateTime("PublishDate");
                book.Description = reader.GetString("Description");
                book.AuthorId = reader.GetInt32("AuthorId");
                book.Price = reader.GetDecimal("Price");
                book.Genre = Enum.Parse<Genre>(reader.GetString("Genre"));
                books.Add(book);
            }
            return books;
        }

        public List<Book> GetBooksDisconnesso()
        {
            var books = new List<Book>();
            string command = "SELECT * FROM Books";
            using SqlConnection conn = GetOpenedConnection();
            using SqlCommand cmd = GetSqlCommand(conn, command);
            using SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            dt.AsEnumerable()
                .ToList()
                .ForEach(x => books.Add(new Book
                {
                    Id = x.Field<int>("Id"),
                    Title = x.Field<string>("Title"),
                    AuthorId = x.Field<int>("AuthorId"),
                    Genre = Enum.Parse<Genre>(x.Field<string>("Genre")),
                    Price = x.Field<decimal>("Price"),
                    PublishDate = x.Field<DateTime>("PublishDate"),
                    Description = x.Field<string>("Description")
                }));

            return books;
        }

        public void CreateBook(Book book)
        {
            string insertCommand = "INSERT INTO Books(AuthorId, Title,Genre,Price,PublishDate,Description) VALUES(@AuthorId,@Title,@Genre,@Price,@PublishDate,@Description);SELECT CAST(SCOPE_IDENTITY() AS INT) AS LAST_IDENTITY";
            using SqlConnection conn = GetOpenedConnection();
            using SqlCommand cmd = GetSqlCommand(conn, insertCommand);
            cmd.Parameters.AddWithValue("@AuthorId", book.AuthorId);
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@Genre", book.Genre);
            cmd.Parameters.AddWithValue("@Price", book.Price);
            cmd.Parameters.AddWithValue("@PublishDate", book.PublishDate);
            cmd.Parameters.AddWithValue("@Description", book.Description);
            int bookId = (int)cmd.ExecuteScalar();
            book.Id = bookId;
        }

        public void UpdateBook(Book book)
        {
            string updateCommand = @$"UPDATE Books
                                                                SET AuthorId=@AuthorId,
                                                                       Title=@Title,
                                                                       Genre=@Genre,
                                                                       Price=@Price,
                                                                       PublishDate=@PublishDate,
                                                                       Description=@Description
                                                                WHERE Id=@Id;";

            using SqlConnection conn = GetOpenedConnection();
            using SqlCommand cmd = GetSqlCommand(conn, updateCommand);
            cmd.Parameters.AddWithValue("@Id", book.Id);
            cmd.Parameters.AddWithValue("@AuthorId", book.AuthorId);
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@Genre", book.Genre);
            cmd.Parameters.AddWithValue("@Price", book.Price);
            cmd.Parameters.AddWithValue("@PublishDate", book.PublishDate);
            cmd.Parameters.AddWithValue("@Description", book.Description);
            int affected = cmd.ExecuteNonQuery();
            if (affected != 1)
            {
                throw new Exception($"Si è verificato un errore in aggiornamento book {book.Title}");
            }
        }

        public void DeleteBook(int bookId)
        {
            string deleteCommand = "DELETE Books WHERE Id=@Id";
            using (SqlConnection conn = GetOpenedConnection())
            {
                using SqlCommand cmd = GetSqlCommand(conn, deleteCommand);
                cmd.Parameters.AddWithValue("@Id", bookId);
                int affected = cmd.ExecuteNonQuery();
                if (affected != 1)
                {
                    throw new Exception($"Si è verificato un errore in eliminazione book {bookId}");
                }
            }
        }

        public void UpdateBookTitle(int bookId, string title)
        {
            string updateCommand = @$"UPDATE Books
                                                                SET Title=@Title
                                                                WHERE Id=@Id;";

            using SqlConnection conn = GetOpenedConnection();
            using SqlCommand cmd = GetSqlCommand(conn, updateCommand);
            cmd.Parameters.AddWithValue("@Id", bookId);
            cmd.Parameters.AddWithValue("@Title", title);
            int affected = cmd.ExecuteNonQuery();
            if (affected != 1)
            {
                throw new Exception($"Si è verificato un errore in aggiornamento book {title}");
            }
        }

        internal Book? ReadBook(int id)
        {
            string readCommand = "SELECT * FROM Books WHERE Id=@Id";
            using SqlConnection conn = GetOpenedConnection();
            using SqlCommand cmd = GetSqlCommand(conn, readCommand);
            cmd.CommandText = readCommand;
            cmd.Parameters.Add(new SqlParameter("@Id", id));
            using SqlDataReader reader = cmd.ExecuteReader();
            int readerCount = 0;
            Book? book = null;
            while (reader.Read())
            {
                if (readerCount > 1)
                {
                    throw new Exception($"Sono presenti più elementi con id {id}");
                }
                book = new Book();
                book.Id = reader.GetInt32("Id");
                book.Title = reader.GetString("Title");
                book.AuthorId = reader.GetInt32("AuthorId");
                book.Genre = Enum.Parse<Genre>(reader.GetString("Genre"));
                book.Price = reader.GetDecimal("Price");
                book.PublishDate = reader.GetDateTime("PublishDate");
                book.Description = reader.GetString("Description");
                readerCount++;
            }
            return book;
        }

        public SearchResults<Book> SearchBooks(string title)
        {
            List<Book> books = new List<Book>();
            string command = "SELECT * FROM Books WHERE  Title LIKE @Title; SELECT COUNT(*) FROM Books WHERE  Title LIKE @Title";
            using SqlConnection conn = GetOpenedConnection();
            using SqlCommand cmd = GetSqlCommand(conn, command);
            cmd.Parameters.AddWithValue("@Title",$"%{title}%");
            using SqlDataReader reader = cmd.ExecuteReader();
            DataSet dataSet = new DataSet();
            do
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataSet.Tables.Add(dt);
            } while (!reader.IsClosed);

            dataSet.Tables[0].AsEnumerable()
                .ToList()
                .ForEach(x => books.Add(new Book
                {
                    Id = x.Field<int>("Id"),
                    Title = x.Field<string>("Title"),
                    AuthorId = x.Field<int>("AuthorId"),
                    Genre = Enum.Parse<Genre>(x.Field<string>("Genre")),
                    Price = x.Field<decimal>("Price"),
                    PublishDate = x.Field<DateTime>("PublishDate"),
                    Description = x.Field<string>("Description")
                }));
            int booksCount = Convert.ToInt32(dataSet.Tables[1].Rows[0][0]);
            return new SearchResults<Book>
            {
                Count = booksCount,
                Results = books
            };

        }

        public List<Author> GetAuthors()
        {
            List<Author> authors = new List<Author>();
            string command = "SELECT * FROM Authors";
            using SqlConnection conn = GetOpenedConnection();
            using SqlCommand cmd = GetSqlCommand(conn, command);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Author author = new Author
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                };
                authors.Add(author);
            }
            return authors;
        }
        private SqlConnection GetOpenedConnection()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        private SqlCommand GetSqlCommand(SqlConnection conn, string command)
        {
            return new SqlCommand(command, conn);
        }
    }
}
