using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsempioADO
{
    public class BookPrinter : IBookPrinter
    {
        public void PrintToConsole(Book book)
        {
            Console.WriteLine($"Book({book.Id}) - {book.Title}");
            Console.WriteLine($"Author: {book.Author}");
            Console.WriteLine($"Genre:{book.Genre} Price: {book.Price:C2} PublishDate:{book.PublishDate:d} Description:{book.Description}");
            Console.WriteLine();
        }
    }
}
