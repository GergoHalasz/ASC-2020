using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Monoton
            monotonCrescator();

        }

        private static void monotonCrescator()
        {
            int n,a,b;
            bool monotonCrescator = true;
            n = int.Parse(Console.ReadLine());
            a = int.Parse(Console.ReadLine());
            for (int i = 0; i < n-1; i++)
            {
                b = int.Parse(Console.ReadLine());
                if (a>b)
                {
                    monotonCrescator = false;

                }
                a = b;

            }
            if (monotonCrescator)
            {
                Console.WriteLine("Secventa este monoton crescatoare");
            }
            else
            {
                Console.WriteLine("Secventa nu este monoton crescatoare");
            }
        }
    }
}
