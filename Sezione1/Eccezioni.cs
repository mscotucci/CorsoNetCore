using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione1
{
    internal class Eccezioni
    {
        public static void StampaValori()
        {
			try
			{
				int numero = 10;
				var n = numero / 0;
			}
			catch(DivideByZeroException divEx)
			{
				throw new MyException(divEx);
			}
			catch (Exception ex) when (ex is MyException) 
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
            }
        }

		public class MyException : Exception
		{
            public MyException():base("Messaggio personalizzato")
            {
                
            }
            public MyException(Exception ex):base("Messaggio personalizzato",ex)
            {
                
            }
        }
    }
}
