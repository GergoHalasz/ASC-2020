using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedTernaryExpansion
{
    class Program
    {
        static void Main(string[] args)
        {
            int i;
            Console.WriteLine("Va rog sa scrieti o valoare in decimal sau bte si o sa fie convertit!");
            Console.WriteLine("Inainte,vreau sa spuneti ca valoarea pe care scrieti este decimal sau bte? (RASPUNS: bte or decimal)");
            
            bool ok = true;
            while (ok)
            {
                string typeOfValoare = Console.ReadLine(); // citim tipul lui valoare
               
     
                    if (typeOfValoare == "bte" || typeOfValoare == "decimal")
                    {
                        ok = false; // infinite loop stop
                        Console.WriteLine("Va rog sa scrieti valoarea!");
                        string valoare = Console.ReadLine(); //citim valoarea
                    string convertToBase3 = "";
                   
                    double valoareConverted = 0;
                    if (typeOfValoare == "bte")
                        for (i = 0; i < valoare.Length; i++)
                        {


                            if (valoare[i] == 'T')
                            {
                                valoareConverted = valoareConverted + -1 * Math.Pow(3, valoare.Length - i - 1);


                            }
                            else
                            {
                                valoareConverted = valoareConverted + int.Parse(valoare[i].ToString()) * Math.Pow(3, valoare.Length - i - 1);
                            }
                        }
                    else
                    {
                        for (i = 0; i < valoare.Length; i++) //decimal , convert to base 3
                        {
                            convertToBase3 += int.Parse(valoare) % 3;
                            valoare = (int.Parse(valoare) / 3).ToString();



                        }
                        char[] reverse = new char[convertToBase3.Length];
                        reverse = convertToBase3.ToArray();
                        Array.Reverse(reverse);
                        convertToBase3 = reverse.ToString();
                        Console.WriteLine(convertToBase3);
                    }
                    
                }
                
                    else
                    Console.WriteLine("Scrieti bte sau decimal!");
            }
        }
    }
}
