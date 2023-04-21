using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione2
{
    public class DatabaseSource
    {
        static List<Ordine> ordini= new List<Ordine>();
        public static List<Ordine> Ordini => ordini;

        public static void InitDataSource()
        {
            for (int i = 0; i < 10; i++)
            {
                var dataOrdine = i % 2 == 0 ? DateTime.Now.AddYears(-1) : DateTime.Now;
                Ordine ordine = new Ordine();
                ordine.Id= i;
                ordine.DataOrdine = dataOrdine;
                ordini.Add(ordine);
                for (int j = 0; j < 3; j++)
                {
                    DettaglioOrdine dettaglio = new DettaglioOrdine();
                    dettaglio.Id = j*i;
                    ordine.Dettagli.Add(dettaglio);
                    for (int k = 0; k < 2; k++)
                    {
                        ItemDettaglio item = new ItemDettaglio();
                        item.Id = k*i*j;
                        item.Nome = $"Item {k * i * j}";
                        item.Quantita = k * j * i;
                        dettaglio.Items.Add(item);
                    }
                }
            }
        }
    }
}
