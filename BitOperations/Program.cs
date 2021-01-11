using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            BitOperationsDemo();
        }

        private static void BitOperationsDemo()
        {
            //0b - baza 2
            //0x - baza 16
            //0 - baza 8
            uint x = 0b11011;

           
            
            byte pos = 2;
            SetBit(ref x, pos);
            
            // afisarea numarului x in baza 2  
            string binary = Convert.ToString(x, 2);
             Console.WriteLine(binary);

            ResetBit(ref x, pos);
            binary = Convert.ToString(x, 2);
            Console.WriteLine(binary);

            uint bits = 123u;
            byte count = 8;
            pos = 8;
            SetBits(ref x, pos, count, bits);


        }
        /// <summary>
        /// Seteaza count biti incepand de la pozitia pos.
        /// </summary>
        /// <param name="x">Valoarea in care se seteaza biti</param>
        /// <param name="pos">Pozitia de la care incepe setarea bitilor</param>
        /// <param name="count">Numarul de biti care se vor seta</param>
        /// <param name="bits">Valoarea din care se iau bioti care se seteaza</param>
        /// <example>
        /// x = 11001100[110]0 intre paranteze drepte sunt biti care vor fi inlocuiti
        /// Bitii pusi intre paranteze drepte in bits vor inlocui bitii intre paranteze drepte din x;
        /// pos = 1
        /// count = 3
        /// bits = 000...00000[011] - biti folositi pentru setare
        /// </example>
        private static void SetBits(ref uint x, byte pos, byte count, uint bits)
        {
            
        }

        /// <summary>
        /// Reseteaza bitul de pe pozitia pos (la valoarea 0)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="pos"></param>
        private static void ResetBit(ref uint x, byte pos)
        {
            // 11111 &
            // 11011
            //--------
            // 11011

            uint pattern = 1u; //asta-i unu
            x = ~(pattern << pos) & x;

        }

        /// <summary>
        /// Seteaza bitul de pe pozitia pos (la valoarea 1)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="pos"></param>
        private static void SetBit(ref uint x, byte pos)
        {
            //11011
            //00100 //bitPattern
            //-------
            //11111

            uint pattern = 1u;
            x= (pattern << pos) | x;

        }
    }
}
