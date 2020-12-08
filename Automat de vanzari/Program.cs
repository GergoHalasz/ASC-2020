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
            string coins;
            string productOrRest;
            int coinsState = 0;

            while(true)
            {
                coins = Console.ReadLine().ToUpper();
                if (coins == "N")
                    coinsState += 5;
                else
                if (coins == "D")
                    coinsState += 10;
                else
                if (coins == "Q")
                    coinsState += 25;
                else
                    throw new Exception("The vending machine accepts only 5,10 or 25 cents");

                if(coinsState>=20)
                {
                    if(coinsState == 40)
                    {
                        Console.WriteLine("Do you want 2 products(press X if yes) or 1 product and the rest(press C if yes)?");
                        productOrRest = Console.ReadLine().ToUpper();
                        if(productOrRest == "X")
                        {
                            Console.WriteLine("Here you have the 2 products");
                            break;
                        }
                        if (productOrRest == "C")
                        {
                            Console.WriteLine($"Here you have the 1 product and the rest : {coinsState - 20} coins");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Here you have the 1 product and the rest : {coinsState - 20} coins");
                        break;
                    }
                }

            }

        }
    }

}
