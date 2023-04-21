namespace Sezione3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Figura> figure = new List<Figura>();
            Cerchio cerchio = new Cerchio(3);
            Quadrato quadrato = new Quadrato(5);
            figure.Add(cerchio);
            figure.Add(quadrato);

            foreach (var figura in figure)
            {
                Console.WriteLine($"figura = {figura}");
            }
        }
    }
}