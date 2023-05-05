namespace EsempioADO
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\App_Data\\BookStore.mdf;Integrated Security=True;Connect Timeout=30";
            IDataBaseManager databaseManager = new ADODataBaseManager(connectionString);
            var books = await databaseManager.GetBooksAsync();
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
                        await databaseManager.InitDbAsync();
                        books = await LeggiAndStampaAsync(databaseManager, printer);
                        break;
                    case 'c':
                        //Creiamo un nuovo book
                        book = new Book();
                        var authors = await databaseManager.GetAuthorsAsync();
                        book.AuthorId = authors.FirstOrDefault().Id;
                        book.Genre = Genre.Computer;
                        book.Description = "Description";
                        book.Title = "Title";
                        book.PublishDate = DateTime.Now;
                        book.Price = 12.25m;
                        await databaseManager.CreateBookAsync(book);
                        books = await LeggiAndStampaAsync(databaseManager, printer);
                        break;
                    case 'r':
                        if (lastBook is not null)
                        {
                            book = await databaseManager.ReadBookWithAuthorAsync(lastBook.Id);
                            printer.PrintToConsole(book);
                        }
                        break;
                    case 'u':
                        if (lastBook is not null)
                        {
                            lastBook.Description = "Descrizione modificata";
                            await databaseManager.UpdateBookAsync(lastBook);
                            books = await LeggiAndStampaAsync(databaseManager, printer);
                        }
                        break;
                    case 'd':
                        if (lastBook is not null)
                        {
                            await databaseManager.DeleteBookAsync(lastBook.Id);
                            books = await LeggiAndStampaAsync(databaseManager, printer);
                        }
                        break;
                    case 'l':
                        books = await LeggiAndStampaAsync(databaseManager, printer);
                        break;
                    case 's':
                        Console.WriteLine("Insertisci il titolo da cercare");
                        string title = Console.ReadLine();
                        Console.Clear();
                        //var searchResults = await databaseManager.SearchBooksAsync(title);
                        BooksSearchCriteria booksSearchCriteria = new BooksSearchCriteria(title, 1, 2);
                        booksSearchCriteria.SetPublishDateStart(new DateTime(2001,1,1));
                        booksSearchCriteria.SetPublishDateEnd(new DateTime(2001, 12, 31));
                        var searchResults = await databaseManager.SearchBooksAsync(booksSearchCriteria);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        //Console.WriteLine($"Risultati trovati '{searchResults.Count}'"); 
                        var totalPages = (int)Math.Ceiling(searchResults.Count / (decimal)booksSearchCriteria.Limit);
                        Console.WriteLine($"Pagina {booksSearchCriteria.Page} di {totalPages} Risultati: {searchResults.Results.Count} di '{searchResults.Count}'");
                        StampaBooks(searchResults.Results, printer);
                        Console.ResetColor();
                        break;
                    case 'x':
                        List<Author> autori = await databaseManager.GetAuthorsHaveMoreBooks();
                        foreach (Author author in autori)
                        {
                            Console.WriteLine(author.Name);
                        }
                        break;
                    default:
                        break;
                }
            } while (userCommand != 'q');
        }
        private static async Task<List<Book>> LeggiAndStampaAsync(IDataBaseManager databaseManager, IBookPrinter bookPrinter)
        {
            var books = await databaseManager.GetBooksAsync();
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
            Console.WriteLine("'x' => Autori con più libri");
            Console.WriteLine("'q' => Esci");
            Console.ResetColor();
        }
    }
}