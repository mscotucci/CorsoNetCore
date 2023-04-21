using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione2
{
    public class DettaglioOrdine
    {
        public int Id { get; set; }
        public List<ItemDettaglio> Items { get; set; } = new List<ItemDettaglio>();
    }
}
