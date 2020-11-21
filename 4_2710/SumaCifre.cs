using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Probleme cu numere/cifrele unui numar.
/// </summary>
namespace _4_2710
{
    class SumaCifrelor
    {
         public static void Main(string[] args)
        {
            int n;

            

            n = Util.GetIntFromConsole();


            // Calculez suma cifrelor numarului:
            Console.WriteLine($"Suma cifrelor numarului {n} este {SumaCifre(n)}");
            Console.WriteLine($"Suma cifrelor numarului {n} este {SumaCifreRecursiv(n)}");


        }
        /// <summary>
        /// Calculeaza suma cifrelor unui numar in mod recursiv
        /// </summary>
        /// <param name="n">asd</param>
        /// <returns></returns>
        public static int SumaCifreRecursiv(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            else
                return n % 10 + SumaCifreRecursiv(n / 10);
        }

        /// <summary>
        /// Calculeaza suma cifrelor unui numar
        /// </summary> 
        /// <param name="n">numarul pentru care calculam suma cifrelor</param>
        /// <returns>suma cifrelor parametrului n</returns>



        public static int SumaCifre(int n)
        {
            int suma = 0; // acumulator
            int cifra;
            while ( n > 0)
            {
                cifra = n % 10;
                suma += cifra; // echivalent cu suma = suma + cifra;
                n /= 10; // echivalent cu n = n / 10; 
            }
            return suma;
        }

      
    }
}
