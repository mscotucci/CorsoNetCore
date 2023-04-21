using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sezione2
{
    public class EsempioArray
    {
        public static void StampaValori()
        {
            int[] a = new int[] { 1, 2, 3 };

            int[][] jagged = new int[2][];
            jagged[0] = new int[] { 1, 2, 3 };
            jagged[1] = new int[] { 1, 2, 3, 4, 5, 6 };

            for (int i = 0; i < jagged.Rank; i++)
            {
                for (int j = 0; j < jagged.GetLength(i); j++)
                {
                    Console.WriteLine($"jagged dimensione {i} riga {j}");
                    for (int k = 0; k < jagged[j].Length; k++)
                    {
                        Console.WriteLine($"colonna {k} = {jagged[j][k]}");
                    }
                }
            }

            int[,] quadrato =
            {
                {1,2,3,4,5 },
                {6,7,8,9,10 },
                {11,12,13,14,15 },
                {16,17,18,19,20 },
                {21,22,23,24,25 },
            };

            int[,] quadratoNew = new int[15, 5];

            for (int riga = 0; riga < quadratoNew.GetLength(0); riga++)
            {
                for (int colonna = 0; colonna < quadratoNew.GetLength(1); colonna++)
                {
                    quadratoNew[riga,colonna]=(colonna+1)*(riga+1);
                }
            }
            StampaMatrice(quadratoNew);

        }

        private static void StampaMatrice(int[,] matrice)
        {
            Console.WriteLine($"Stampo matrice di dimensione {matrice.Rank}");

            for (int riga = 0; riga < matrice.GetLength(0); riga++)
            {
                for (int colonna = 0; colonna < matrice.GetLength(1); colonna++)
                {
                    Console.Write("{0,-5}", matrice[riga, colonna]);
                }
                Console.WriteLine();
            }
        }
    }
}
