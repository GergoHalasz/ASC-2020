using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moore
{
    class Program
    {
        static void Main(string[] args)
        {

            // rezolva problema
            
            int n;
            Console.WriteLine("Scrieti numarul n:");
            n = int.Parse(Console.ReadLine());
            int luni = Convert.ToInt32(Math.Floor((Math.Log(n, 2)) * 18));
            int ani = luni / 12;
            luni = luni % 12;

     
            Console.WriteLine($"Dupa {ani} ani si {luni} luni vom avea o puterea de calcul de {n} ori mai mare, la acelasi pret.");

        }
    }
}
