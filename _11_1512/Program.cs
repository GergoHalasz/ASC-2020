using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_1512
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] w = { 3, 6, 2, 1, 5, 7, 2, 4, 1, 9 };
            int C = 10;
            NF(w, C);

            FF(w, C );

            FFD(w, C);
        }

        private static void FFD(int[] w, int C)
        {
           
        }

        /// <summary>
        /// First Fit
        /// </summary>
        /// <param name="w">Lista dimensiunilor obiectelor care trebuie impachetate</param>
        /// <param name="
        private static void FF(int[] w, int C)
        {
            Console.WriteLine("First Fit:"); // scriem pe consola First Fit
            int[] bins = new int[w.Length]; // containere
            int k = 1; // cate containere am deschise la un moment dat
            for (int i = 0; i < w.Length; i++) //parcurgem lista
            {
                bool packed = false;
                for (int j = 0; j < k; j++)
                {
                    if (w[i] + bins[j] <= C) // daca incape w[i] in containerul respectiv
                    {
                        bins[j] += w[i];
                        packed = true;
                        break; // ajunge,nu trebuie verificat alte containere,deoarece obiectul a ajuns in containerul respectiv(din for-ul acesta iesim) 
                       

                    }

                }

                if(!packed) // daca nu a fost pus in containerul respectiv
                {
                    k++; // facem un alt container
                    bins[k - 1] = w[i]; // punem in containerul facut

                }
            }
            Console.WriteLine($"Numarul de containere de capacitate {C} folosite este: {k}");
        }

        /// <summary>
        /// Next Fit
        /// </summary>
        /// <param name="w">Lista dimensiunilor obiectelor care trebuie impachetate</param>
        /// <param name="C">Capacitatea maxima a unui container</param>
        private static void NF(int[] w, int C)
        {
            Console.WriteLine("Next Fit:");
            int bins = 1;
            int current_capacity = 0; // tine evidenta capacitatii umplute in containerului curent
            for (int i = 0; i < w.Length; i++)
            {
                if (w[i] + current_capacity <= C) // daca obiectul curent incape in containerul deschis
                    current_capacity += w[i]; //atunci il adaug in container

                else // altfel deschid un nou container in care pun obiectul curent
                {
                    bins++;
                    current_capacity = w[i];
                }
            }

            Console.WriteLine($"Numarul de containere de capacitate {C} folosite este: {bins}");
        }
    }
}
