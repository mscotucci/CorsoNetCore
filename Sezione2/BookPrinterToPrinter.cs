using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione2
{
    public class BookPrinterToPrinter : IBookPrinter
    {
        public void PrintToConsole(IBook book)
        {
            //stampa su una stampante di rete
        }
    }
}
