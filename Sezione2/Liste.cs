using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sezione2
{
    public class Liste
    {
        public static void StampaValori()
        {
            //List<string> lista = new List<string>();
            //lista.Add("ciao");
            //lista.Add("mondo");

            //Console.WriteLine($"count {lista.Count} {lista.Capacity}");

            //lista.Insert(0, "pippo");
            //lista.TrimExcess();
            //var indiceCiao = lista.IndexOf("ciao");
            //Console.WriteLine($"indice ciao = {indiceCiao}");
            //lista.LastIndexOf("ciao");
            //bool contains = lista.Contains("ciao");
            //Console.WriteLine($"la lista contiene il valore 'ciao'={contains}");

            //lista = new List<string>() { "Ciao", "Mondo" };


            //Queue<int> coda = new Queue<int>();
            //coda.Enqueue(1);
            //coda.Enqueue(2);
            //coda.Enqueue(3);

            //foreach (var elemento in coda)
            //{
            //    Console.WriteLine("Elemento in coda = {0}",elemento.ToString());
            //}

            //var prossimo = coda.Peek();
            //Console.WriteLine("Prossimo = {0}", prossimo);
            //prossimo = coda.Dequeue();
            //Console.WriteLine("Prossimo dec = {0}", prossimo);

            //foreach (var elemento in coda)
            //{
            //    Console.WriteLine("Elemento in coda = {0}", elemento.ToString());
            //}

            //Stack<int> stack = new Stack<int>();
            //stack.Push(1);
            //stack.Push(2);

            //int top = stack.Peek();
            //Console.WriteLine("top stack = {0}", top);

            HashSet<int> set = new HashSet<int>();
            set.Add(1);
            set.Add(2);

            HashSet<int> set2 = new HashSet<int>();
            set2.Add(3);
            set2.Add(2);

            set.UnionWith(set2);
            PrintEnumerable(set);
            var intersectResult = set.Intersect(set2);
            PrintEnumerable(intersectResult);

            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(1, "aa");
            dict.Add(2, "aa");
            dict.Add(3, "333333");

            bool esiste = dict.TryGetValue(3, out var val);
            if (esiste)
            {
                Console.WriteLine("esiste val = {0}",val);
            }
            foreach (KeyValuePair<int,string> elemento in dict)
            {
                Console.WriteLine("chiave {0} valore {1}", elemento.Key, elemento.Value);
            }
        }
        public static void PrintEnumerable(IEnumerable enumerable)
        {
            Console.WriteLine("-----------");
            foreach (var item in enumerable)
            {
                Console.WriteLine(item);
            }
        }
    }
}
