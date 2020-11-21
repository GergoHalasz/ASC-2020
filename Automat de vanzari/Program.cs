using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_de_vanzari
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int cent = 0, sumaCent = 0;
            Boolean b = false;
            Console.WriteLine("Va rog sa puneti centuri(nickel,dime,quarter only)!20 cent trebuie pentru a cumpara o cafea!");
            Console.WriteLine();
            while (!b || sumaCent < 20) // citire centuri
            {
                try
                {
                    Console.WriteLine("SUMA CARE AVETI: " + sumaCent +" cent");
                    cent = int.Parse(Console.ReadLine());
                    b = true;
                    if (cent % 5 != 0)
                    {
                        b = false;
                        Console.WriteLine("Va rog sa puneti inca o data!Numai nickel,dime sau quarter se poate!");
                    }
                    else
                        sumaCent = sumaCent + cent;
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message + "Va rog sa puneti inca o data!");
                    b = false;
                    


                }
            }

            Console.WriteLine("Poftiti cafea :)!");
            if(sumaCent>20)
                Console.WriteLine("REST: "+(sumaCent-20));
        }







    }

}
