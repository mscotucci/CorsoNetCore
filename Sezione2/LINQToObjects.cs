using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione2
{
    public class LINQToObjects
    {
        public static void StampaValori()
        {
            DatabaseSource.InitDataSource();
            var ordini = DatabaseSource.Ordini;

            //Ordini anno scorso
            var ordiniAnnoScorso = ordini.Where(x => x.DataOrdine.Year == DateTime.Now.Year - 1);
            Console.WriteLine($"Ordini anno scorso = {ordiniAnnoScorso.Count()}");
            Console.WriteLine("-----------------------------------------------");
            foreach (var ordine in ordiniAnnoScorso)
            {
                Console.WriteLine($"Ordine id = {ordine.Id}");
            }

            //Ordini raggruppati per anno
            var ordiniRaggruppatiPerAnno = ordini.GroupBy(x => x.DataOrdine.Year);
            foreach (var gr in ordiniRaggruppatiPerAnno)
            {
                var anno = gr.Key;
                Console.WriteLine("Anno {0}", anno);
                foreach (var ordine in gr)
                {
                    Console.WriteLine($"Ordine id = {ordine.Id}");
                }
            }

            //Ordini cha hanno i dettagli con id dispari
            var ordiniDettagliIdDispari= ordini.Where(ordine=>ordine.Dettagli.Any(dettaglio => dettaglio.Id % 2 !=0));
            Console.WriteLine($"Ordini con dettagli id dispari = {ordiniDettagliIdDispari.Count()}");
            Console.WriteLine("-----------------------------------------------");
            foreach (var ordine in ordiniDettagliIdDispari)
            {
                Console.WriteLine($"Ordine id = {ordine.Id}");
                foreach (var dettaglio in ordine.Dettagli)
                {
                    Console.WriteLine("Dettaglio id {0}", dettaglio.Id);
                }
            }

            var dettagliConIdDispari = ordini.SelectMany(o=>o.Dettagli.Where(d=>d.Id %2 !=0));
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Dettagli con id dispari {0}", dettagliConIdDispari.Count());
            foreach (var dettaglio in dettagliConIdDispari)
            {
                Console.WriteLine("Dettaglio id {0}", dettaglio.Id);
            }

            var items = from o in ordini
                            from d in o.Dettagli.Where(dett=>dett.Id % 2 ==0)
                            from i in d.Items
                            where i.Quantita > 20 && d.Id % 2 == 0
                            select i;

            items = ordini.SelectMany(o => o.Dettagli.Where(dett => dett.Id % 2 == 0).SelectMany(i => i.Items))
                .Where(x => x.Quantita > 20);
        }
    }
}
