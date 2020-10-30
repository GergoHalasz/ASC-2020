using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion
{
    class Program
    {
        static void Main(string[] args)
        {
            string numar;
            int b1, b2;
            Console.WriteLine("Conversii:va rog sa scrieti numarul,baza lui si baza tinta");
            numar = Console.ReadLine();
            b1 = int.Parse(Console.ReadLine());
            b2 = int.Parse(Console.ReadLine());

            while (b1 < 2 || b1 > 16 || b2 > 16 || b2 < 2)
            {
                Console.WriteLine("Bazele nu sunt corecte.Scrie iarasi corect");
                b1 = int.Parse(Console.ReadLine());
                b2 = int.Parse(Console.ReadLine());
            }

            string parteIntreaga, parteFract;
            string[] split = numar.Split('.');

            parteIntreaga = split[0];

            parteFract = split[1];
            int parteInt, fractInt;
            parteInt = Convert.ToInt32(parteIntreaga);
            fractInt = Convert.ToInt32(parteFract);
            int numarInt = Convert.ToInt32(numar);
            if (b1 == 10)
            {
                int db = parteIntreaga.Length;
                int db1 = parteIntreaga.Length;
                int c, d;
                int[] v = new int[db + 1];
                while (db != 0)
                {
                    c = numarInt % b2;
                    v[db] = c;
                    numarInt = numarInt / b2;
                    db--;
                }
                int dbFract = parteFract.Length;







            }







        }
    }
}
