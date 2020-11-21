using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_2710
{
    class Util
    {
        public static int GetIntFromConsole()
        {
            string line;
            int n = 0;
            bool flag; //fanion
            do
            {

                flag = true;
                Console.WriteLine("Introduceti un numar intreg");
                line = Console.ReadLine();
                try
                {
                    n = int.Parse(line);
                }
                catch (FormatException ex)
                {

                    Console.WriteLine(ex.Message);
                    flag = false;
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine(ex.Message);
                    flag = false;
                }
                if (!flag)
                {
                    Console.WriteLine("Mai incercati inca o data");
                }


            } while (!flag);
            return n;

        } 
    }
}
