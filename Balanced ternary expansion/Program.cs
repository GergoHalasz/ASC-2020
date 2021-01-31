using System;


namespace Balanced_ternary_expansion
{
    static class BTE
    {
        
        static string toBTE(int n)
        {
            return n != 0 ? toBTE((n + new int[] { 0, -1, 1 }[n % 3]) / 3) + "01T"[n % 3] : "";
        }

        static string convertToBTE(int number)
        {
            string result = "";
            if (number == 0)
                return "0";

            while (number != 0)
            {
                int remainder = number % 3;

                if (remainder == 0)
                    result = "0" + result;

                else if (remainder == 1)
                {
                    result = "1" + result;
                    number--;
                }
                else if (remainder == 2)
                {
                    result = "T" + result;
                    number++;
                }


                number /= 3;
            }

            return result;
        }

        static int fromBTE(string number)
        {
            return number.Length != 0 ? (number[number.Length - 1] == 'T' ? -1 : number[number.Length - 1] - '0') + 3 * fromBTE(number.Remove(number.Length - 1, 1)) : 0;
        }

        static int convertFromBTE(string number)
        {
            int result = 0;
            for (int i = 0; i < number.Length; i++)
            {
                char digitType = number[i];
                int digit = 0;

                if (digitType == 'T')
                    digit = -1;

                else if (digitType == '1')
                    digit = 1;

                result = result * 3 + digit;
            }

            return result;
        }

        static string BTEInput()
        {
            string number = "";
            bool correctInput = false;

            while (!correctInput)
            {
                number = Console.ReadLine();
                correctInput = true;

                foreach (var digit in number)
                    if (digit != 'T' && digit != '1' && digit != '0')
                    {
                        correctInput = false;
                        Console.Write("    Input gresit. Incearca din nou.\n    ");
                        break;
                    }
            }

            return number;
        }

        static void Main(string[] args)
        {
            Console.Write("    Introduceti baza numarului pe care vreti sa-l convertiti. (BTE sau 10)\n    ");
            string _base = Console.ReadLine().ToUpper();

            while (_base != "BTE" && _base != "10")
            {
                Console.Write("    Input gresit. Incearca din nou.\n    ");
                _base = Console.ReadLine();
            }

            Console.WriteLine();

            if (_base == "10")
            {
                Console.Write("    Introduceti numarul pe care doriti sa-l converiti din baza 10 in BTE.\n    ");

                int number;
                while (!int.TryParse(Console.ReadLine(), out number))
                    Console.Write("    Input gresit. Incearca din nou.\n    ");

                Console.WriteLine($"    Numarul {number} convertit in BTE este {toBTE(number)}.");
            }
            else
            {
                Console.Write("    Introduceti numarul pe care doriti sa-l converiti din BTE in baza 10.\n    ");

                string number = BTEInput();

                Console.WriteLine($"    Numarul {number} convertit in baza 10 este {fromBTE(number)}.");
            }

            Console.Write("\n    ");
        }
    }
}