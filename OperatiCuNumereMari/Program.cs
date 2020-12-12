using System;
using System.Numerics;
using System.Text.RegularExpressions;


namespace OperatiCuNumereMari
{
    class Program
    {
        static void Main(string[] args)
        {
            bool ok = false;
            string numar1 = "", numar2 = "";
            Console.WriteLine("Write down 2 long numbers");
      
            string onlyNumbers = @"^\d+$";
           
            
            //first number reading
            while(!ok)
            {

                numar1 = Console.ReadLine();
                Match result = Regex.Match(numar1, onlyNumbers);
                if (result.Success)
                    ok = true;
                else
                {
                    ok = false;
                    Console.WriteLine("Please write again the number!Only use numbers!");
                }
            }
            //second number reading
            ok = false;
            while (!ok)
            {

                numar2 = Console.ReadLine();
                Match result = Regex.Match(numar2, onlyNumbers);
                if (result.Success)
                    ok = true;
                else
                {
                    ok = false;
                    Console.WriteLine("Please write again the number!Only use numbers!");
                }
            }

            BigInteger b1 = BigInteger.Parse(numar1);
            BigInteger b2 = BigInteger.Parse(numar2);
            Console.WriteLine("Adunare:");
            Console.WriteLine(b1 + b2);

            Console.WriteLine("Scadere:");
            Console.WriteLine(b1 - b2);

            Console.WriteLine("Inmultire:");
            Console.WriteLine(b1 * b2);

            Console.WriteLine("Impartire:");
            Console.WriteLine(b1 / b2);

            int puterea;
            BigInteger b3 = 1;
            Console.WriteLine("La cat vrei sa ridici puterea?O sa folosim primul numar");
            puterea = int.Parse(Console.ReadLine());
            if (puterea == 1 || puterea == 0)
                b3 = 1;
            else
            {
                b3 = b1;
                for (int i = 1; i < puterea; i++)
                {
                    b3 = b3 * b1;
                }
            }
            Console.WriteLine(b3);

            Console.WriteLine("Radacina patrata.O sa folosim primul numar");
            Console.WriteLine(Math.Pow(Math.E, BigInteger.Log(b1) / 2)); 


        }
    }
}
