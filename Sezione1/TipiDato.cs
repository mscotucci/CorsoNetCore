using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione1
{
    internal class TipiDato
    {
        public static void StampaValori()
        {
            //Tipi interi
            //byte b = 300; //errore, il valore 300 è troppo grande per il tipo byte
            //byte b = 255;
            //Console.WriteLine($"byte b = " + b);
            //byte bMax=byte.MaxValue;
            //byte bMin=byte.MinValue;
            //Console.WriteLine("bMin = {0} bMax={1}",bMin,bMax);

            //sbyte sb = -1;
            //Console.WriteLine($"sbyte sb = " + sb);
            //sbyte sbMax = sbyte.MaxValue;
            //sbyte sbMin = sbyte.MinValue;
            //Console.WriteLine("sbyte Min:{0} Max:{1}", sbMin, sbMax);

            //short sh = -1;
            //Console.WriteLine($"short sb = " + sh);
            //short shMax = short.MaxValue;
            //short shMin = short.MinValue;
            //Console.WriteLine("short Min:{0} Max:{1}", shMin, shMax);

            //ushort us = 1;
            //Console.WriteLine($"ushort sb = " + us);
            //ushort usMax = ushort.MaxValue;
            //ushort usMin = ushort.MinValue;
            //Console.WriteLine("ushort Min:{0} Max:{1}", usMin, usMax);

            //int i = -1;
            //Console.WriteLine($"int i = {i} Min:{int.MinValue} Max:{int.MaxValue}");
            //uint ui = 1;
            //Console.WriteLine($"uint ui  ={ui} Min:{uint.MinValue} Max:{uint.MaxValue}");
            //long lo = -1;
            //Console.WriteLine($"long lo = {lo} Min:{long.MinValue} Max:{long.MaxValue}");
            //ulong ul = 1;
            //Console.WriteLine($"ulong ui  = {ul} Min:{ulong.MinValue} Max:{ulong.MaxValue}");

            //float f = 0.1F; //ok
            //Console.WriteLine($"float  f  = " + f);
            //f = 0.1F * 9999999;
            //Console.WriteLine($"float  f  = " + f);
            //double d = 0.1D;
            //Console.WriteLine($"double  f  = " + d);

            bool bo = false;

            char char1 = 'X';
            Console.WriteLine($"char1={char1}");
            char char2 = '\x0058';
            Console.WriteLine($"char2={char2}");
            char char3 = '\u0058';
            Console.WriteLine($"char3={char3}");
            char char4 = (char)88;
            Console.WriteLine($"char4={char4}");

            string str1 = "ciao";
            string str2 = "mondo";
            str1=str1+str2;
            Console.WriteLine($"str1={str1}");
            string str3 = new string(new char[] { 'c','i','a','o' });
            Console.WriteLine($"str3 = {str3}");
        }
    }
}
