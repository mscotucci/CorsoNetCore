﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsempioADO
{
    public class BookXml
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
    }
}
