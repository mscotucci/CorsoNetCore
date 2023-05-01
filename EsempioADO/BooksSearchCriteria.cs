using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsempioADO
{
    public class BooksSearchCriteria
    {
        public BooksSearchCriteria(string search, int page, int limit)
        {
            Search = search ?? "";
            Page = Math.Max(1, page);
            Limit = Math.Max(1, limit);

            Offset = (Page - 1) * Limit;
        }
        public string Search { get; }
        public int Page { get; }

        public int Limit { get; }
        public int Offset { get; }
    }
}
