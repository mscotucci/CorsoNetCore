using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione2
{
    public class RiciclaBook : IRiciclaBook
    {
        public void Ricicla(IBookRiciclabile book)
        {
            book.Ricicla();
        }
    }
}
