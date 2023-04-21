using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione2
{
    public class Ordine
    {
        public int Id { get; set; }
        public DateTime DataOrdine { get; set; }

        public List<DettaglioOrdine> Dettagli { get; set; } = new List<DettaglioOrdine>();

    }
}
