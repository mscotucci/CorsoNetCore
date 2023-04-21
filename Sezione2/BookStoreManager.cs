using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione2
{
    public class BookStoreManager
    {
        private readonly IBookPrinter _bookPrinter;
        private readonly IRiciclaBook _riciclaBook;

        public BookStoreManager(IBookPrinter bookPrinter, IRiciclaBook riciclaBook)
        {
            _bookPrinter = bookPrinter;
            _riciclaBook = riciclaBook;
        }

        public void Stampa(IBook book)
        {
            _bookPrinter.PrintToConsole(book);
        }

        public void Ricicla(IBookRiciclabile book)
        {
            _riciclaBook.Ricicla(book);
        }
    }
}
