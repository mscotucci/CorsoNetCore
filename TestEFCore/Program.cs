using EsempioADO;

namespace TestEFCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\App_Data\\BookStore.mdf;Integrated Security=True;Connect Timeout=30";
            IDataBaseManager databaseManager = new EFCoreDataBaseManager();
            var books = databaseManager.GetBooks();
            IBookPrinter printer = new BookPrinter();
            char userCommand;
            do
            {
                Book? book;
                var lastBook = books.OrderByDescending(book => book.Id).FirstOrDefault();
                if (lastBook is null)
                {
                    Console.WriteLine("Non sono presenti elementi nel db");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Last Book Id={lastBook.Id}");
                    Console.WriteLine("");
                    Console.ResetColor();
                }
                StampaOpzioni();
                userCommand = Console.ReadKey(true).KeyChar;
                Console.Clear();
                switch (userCommand)
                {
                    case 'i':
                        databaseManager.InitDb();
                        books = LeggiAndStampa(databaseManager, printer);
                        break;
                    case 'c':
                        //Creiamo un nuovo book
                        book = new Book();
                        var authors = databaseManager.GetAuthors();
                        book.AuthorId = authors.FirstOrDefault().Id;
                        book.Genre = Genre.Computer;
                        book.Description = "Description";
                        book.Title = "Title";
                        book.PublishDate = DateTime.Now;
                        book.Price = 12.25m;
                        databaseManager.CreateBook(book);
                        books = LeggiAndStampa(databaseManager, printer);
                        break;
                    case 'r':
                        if (lastBook is not null)
                        {
                            book = databaseManager.ReadBookWithAuthor(lastBook.Id);
                            printer.PrintToConsole(book);
                        }
                        break;
                    case 'u':
                        if (lastBook is not null)
                        {
                            lastBook.Description = "Descrizione modificata";
                            databaseManager.UpdateBook(lastBook);
                            books = LeggiAndStampa(databaseManager, printer);
                        }
                        break;
                    case 'd':
                        if (lastBook is not null)
                        {
                            databaseManager.DeleteBook(lastBook.Id);
                            books = LeggiAndStampa(databaseManager, printer);
                        }
                        break;
                    case 'l':
                        books = LeggiAndStampa(databaseManager, printer);
                        break;
                    case 's':
                        Console.WriteLine("Insertisci il titolo da cercare");
                        string title = Console.ReadLine();
                        Console.Clear();
                        //var searchResults = await databaseManager.SearchBooksAsync(title);
                        BooksSearchCriteria booksSearchCriteria = new BooksSearchCriteria(title, 1, 2);
                        var searchResults = databaseManager.SearchBooks(booksSearchCriteria);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Risultati trovati '{searchResults.Count}'");
                        StampaBooks(searchResults.Results, printer);
                        Console.ResetColor();
                        break;
                    default:
                        break;
                }
            } while (userCommand != 'q');
        }
        private static List<Book> LeggiAndStampa(IDataBaseManager databaseManager, IBookPrinter bookPrinter)
        {
            var books = databaseManager.GetBooks();
            if (books.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                StampaBooks(books, bookPrinter);
                Console.ResetColor();
                Console.WriteLine("");
            }
            return books;
        }
        private static void StampaBooks(List<Book> books, IBookPrinter bookPrinter)
        {
            foreach (var book in books)
            {
                bookPrinter.PrintToConsole(book);
            }
        }
        private static void StampaOpzioni()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("'i' => Inizializza db");
            Console.WriteLine("'c' => Create Book");
            Console.WriteLine("'r' => Read Last Book");
            Console.WriteLine("'u' => Update Last Book");
            Console.WriteLine("'d' => Delete Last Book");
            Console.WriteLine("'l' => Lista Book");
            Console.WriteLine("'s' => Search Book");
            Console.WriteLine("'q' => Esci");
            Console.ResetColor();
        }
    }
}