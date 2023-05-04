using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsempioADO
{
    public class BooksSearchCriteria : BaseSearchCriteria
    {
        public BooksSearchCriteria(string search, int page, int limit) : base(page, limit)
        {
            Search = search ?? "";
        }
        public string Search { get; }

    }
}
