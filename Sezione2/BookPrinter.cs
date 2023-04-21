using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione2
{
    public class BookPrinter : IBookPrinter
    {
        public void PrintToConsole(IBook book)
        {
            Console.WriteLine($"Book Title:{book.Title} Author:{book.Author} Genre:{book.Genre} Price:{book.Price:C} PublishDate:{book.PublishDate:d}");
        }
    }
}
