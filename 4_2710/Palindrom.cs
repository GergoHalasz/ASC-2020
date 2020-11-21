using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_2710
{
    class Palindrome 
    {
        static void Main(string[] args)
        {
            int n;
            Console.WriteLine("OK" );

            n = Util.GetIntFromConsole();

            if (Palindrom(n))
            {

                Console.WriteLine("Numar este palindrom");
            }
            else
            {
                Console.WriteLine("Numarul nu este palindrom");
            }
            
        }

        private static bool Palindrom(int n)
        {
            if (n== Reverse(n))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
