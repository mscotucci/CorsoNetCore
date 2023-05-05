namespace TestEFCoreReverse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new BookStoreContext())
            {
                var books = context.Books.ToList();
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title);
                }
            }
        }
    }
}