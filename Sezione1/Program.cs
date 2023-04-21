namespace Sezione1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("t per Tipi Dato");
            Console.WriteLine("o per operazioni"); 
            Console.WriteLine("q per uscire");
            char command;
            do
            {
                command = Console.ReadKey(true).KeyChar;
                switch (command)
                {
                    case 't':
                        TipiDato.StampaValori();
                        break;
                    case 'o':
                        Operazioni.StampaValori();
                        break;
                    default:
                        break;
                }
            } while (command != 'q');

        }
    }
}