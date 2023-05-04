using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsempioADO
{
    public class AuthorSearchCriteria : BaseSearchCriteria
    {
        public AuthorSearchCriteria(string name, int page, int limit) : base(page, limit)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
