using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione2
{
    public class LINQ
    {
        public static void StampaValori()
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //query syntax
            
            var query = from n in array
                                where n % 2 == 0
                                select n * n;
            foreach ( var item in query )
            {
                Console.WriteLine( item );
            }

            //method syntax

            query = array.Where(i => i % 2 == 0).Select(n => n * n);
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            Func<int, bool> espressioneLambda = n => n % 2 == 0;
            Func<int,int,bool> espr=(n1,n2) => n1== n2;
            Func<int, Utente, bool> espr2 = (n1, utente) => n1 == utente.Age;

            query = array.Where(QueryMioDb.GetNumeriPariAlQuadrato());

            int numeroRisultati = (from n in array
                                   where n % 2 == 0
                                   select n * n).Count();
            Console.WriteLine(numeroRisultati);

            array[1] = 100;
            array[2] = 150;
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            query = query.Where(x => x > 20);

        }
        bool EspressioneLamba(int n)
        {
            return n % 2 == 0;
        }
    }

    static class QueryMioDb
    {
        public static Func<int,bool> GetNumeriPariAlQuadrato()
        {
            return (n) =>
            {
                return n % 2 == 0;
            };
        }

        public static Func<Utente, bool> GetUtentiMaggiorenni() => utente => utente.Age >= 18;

    }

    class Utente
    {
        public int Age { get; set; }
    }
}
