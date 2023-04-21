using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione1
{
    internal class ControlloFlusso
    {
        public static void StampaValori()
        {
            int x = 10;
            int y = 20;

            if (x > y)
            {
                Console.WriteLine("x è maggiore di y");
            }
            else if (x < y)
            {
                Console.WriteLine("x è minore di y");
            }
            else
            {
                Console.WriteLine("x è uguale ad y");
            }

            for (int i = 0; i < 10; i++)
            {
                
                Console.WriteLine("ciclo i = {0}", i);
            }

            int[] numeri = { 1, 2, 3, 4, 5 };
            int index = 0;
            foreach (int i in numeri)
            {
                Console.WriteLine("ciclo foreach i = {0}", i);
                index++;
            }

            string parola = "ciao";
            for (int i = 0; i < parola.Length; i++)
            {
                Console.WriteLine("carattere {0} = {1}",i, parola[i]);
            }

            foreach (var c in parola)
            {
                Console.WriteLine("valore carattere = {0}", (char)c);
            }

            int j = 0;
            do
            {
                Console.WriteLine("i={0}", j);
                j++;
            } while (j<5);

            while (j < 4)
            {
                Console.WriteLine("j={0}", j);
                j++;
            }

            //Console.WriteLine("Inserisci un carattere");
            //char ch = Console.ReadKey(true).KeyChar;
            //switch (ch)
            //{
            //    case 'a':
            //        Console.WriteLine("Hai inserito a");
            //        break;
            //    case 'b':
            //        Console.WriteLine("Hai inserito b");
            //        break;
            //    case 'c':
            //        Console.WriteLine("Hai inserito a");
            //        break;
            //    default:
            //        Console.WriteLine("Non hai inserito un valore valido");
            //        break;
            //}

            DayOfWeek day = DateTime.Now.DayOfWeek;
            switch (day)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    Console.WriteLine("riposo");
                    break;
                case DayOfWeek.Monday:
                    Console.WriteLine("8:00 - 12:00");
                    break;
                case DayOfWeek.Tuesday:
                    Console.WriteLine("8:00 - 12:00 e 15:00 - 18:00");
                    break;
                case DayOfWeek.Wednesday:
                    Console.WriteLine("8:00 - 12:00");
                    break;
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    Console.WriteLine("8:00 - 12:00");
                    break;
            }
        }
    }
}
