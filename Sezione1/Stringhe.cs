using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione1
{
    internal class Stringhe
    {
        public static void StampaValori()
        {
            string charArray = new string(new char[] { 'a', 'b' });
            Console.WriteLine(charArray);

            string nome = "Mario";
            string cognome = "Rossi";
            string nomeCompleto = nome + " " + cognome;//concatenazione
            Console.WriteLine(nomeCompleto);

            string nomeCompletoInterpolazione = $"{nome} {cognome}"; //interpolazione
            Console.WriteLine(nomeCompletoInterpolazione);

            string nomeCompletoFormat = string.Format("{0} {1}", nome, cognome);//format
            Console.WriteLine(nomeCompletoFormat);

            float num = 1.2F;
            Console.WriteLine("|{0, 5}|", num); //allinea a destra
            Console.WriteLine("|{0, -5}|", num); //allinea a sinistra
            num = 1.23456789F;
            Console.WriteLine("|{0, 5}|", num);

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            double val = 123.456789D;
            Console.WriteLine("currency:      {0:C2}", val);
            Console.WriteLine("currency:      {0:C2}", val);
            Console.WriteLine("decimal:       {0:D5}", 123);
            Console.WriteLine("esponenziale:  {0:E2}", val);
            Console.WriteLine("virgola fissa: {0:F3}", val);
            Console.WriteLine("generale:      {0:G}", val);
            Console.WriteLine("numerico:      {0:N4}", 123456789.123456789);
            Console.WriteLine("percent:       {0:P2}", 0.123);
            Console.WriteLine("round trip:    {0:R}", val);
            Console.WriteLine("esadecimale:   {0:X}", 123);

            Console.WriteLine("{0:#.###}", 1.23456);
            Console.WriteLine("{0:##,##}", 123.456);
            Console.WriteLine("{0:(+##)###-###-###}", 39123456789);
        }
    }
}
