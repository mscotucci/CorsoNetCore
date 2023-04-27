using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsempioADO
{
    public class Book
    {
        public int Id { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
    }
}
