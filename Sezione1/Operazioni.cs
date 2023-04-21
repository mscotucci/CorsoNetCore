using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione1
{
    internal class Operazioni
    {
        public static void StampaValori()
        {
            int numero1 = 10;
            int numero2 = 5;
            Console.WriteLine($"numero1 = {numero1}");
            Console.WriteLine($"numero2 = {numero2}");
            int somma = numero1 + numero2;
            int differenza = numero1 - numero2;
            int prodotto = numero1 * numero2;
            int quoziente = numero1 / numero2;
            int resto = numero1 % numero2;

            //Console.WriteLine("Somma: " + somma);
            //Console.WriteLine("Differenza: " + differenza);
            //Console.WriteLine("Prodotto: " + prodotto);
            //Console.WriteLine("Quoziente: " + quoziente);
            //Console.WriteLine("Resto: " + resto);

            int incremento = ++numero1;
            Console.WriteLine($"numero1 = {numero1}");
            Console.WriteLine($"incremento =  {incremento}");

            int decremento = --numero2;
            Console.WriteLine($"numero2 = {numero2}");
            Console.WriteLine($"decremento =  {decremento}");
            numero1 += numero1;//numero1 = numero1 + numero1;

            bool maggiore = numero1 > numero2;
            bool minore = numero1 < numero2;
            bool uguale = numero1 == numero2;
            bool diverso = numero1 != numero2;

            Console.WriteLine($"Maggiore = {maggiore}");
            Console.WriteLine($"MInore = {minore}");
            Console.WriteLine($"Uguale = {uguale}");
            Console.WriteLine($"Diverso = {diverso}");

            bool vero = true;
            bool falso = false;

            bool and = vero && falso;
            bool or = vero || falso;
            bool not = !vero;

            if(vero || falso)
            {

            }

            double d = 5.5;
            double s = 5 + d;

            int a = 256;
            byte b=(byte)a;
            Console.WriteLine($"byte = {b}");
        }
    }
}
