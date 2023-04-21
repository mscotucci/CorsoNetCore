using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione2
{
    public class Book : IBookRiciclabile
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
        public bool IsCheap()
        {
            return Price < 10;
        }

        public void Ricicla()
        {
            Console.WriteLine("Riciclato");
        }
    }
}
