﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsempioADO
{
    public class SearchResults<T> where T : class
    {
        public int Count { get; set; }
        public List<T> Results { get; set; } = new List<T>();
    }
}
