using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace HexViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            bool done = false;

            while(!done)
            {

                try
                {
                    
                    Console.WriteLine("Introduceti calea fisierului pentru a-l vizualiza prin HexViewer:\n");
                    string path = Console.ReadLine();
                    Console.WriteLine();

                    Console.WriteLine("Introduceti cati octeti vreti sa fie pe o singura linie:\n");
                    int nrOcteti = int.Parse(Console.ReadLine());
                    Console.WriteLine();


                    FileStream file = new FileStream(path,FileMode.Open);

                   
                    char[] caractereDeEliminat = new char[] { ' ', '"' };
                    path = path.Trim(caractereDeEliminat);
                    byte[] byteBlock = new byte[nrOcteti];
                    int idx = 0;

                    while(file.Read(byteBlock, 0, nrOcteti)>0)
                    {
                        string hex = BitConverter.ToString(byteBlock);
                        hex = hex.Replace("-", " ");
                        string text = "";
                        for (int i = 0; i < byteBlock.Length; i++)
                       text += byteBlock[i] < ' ' ? "." : ((char)byteBlock[i]).ToString();

                        Console.WriteLine($" {idx:X8} : {hex.PadRight(nrOcteti * 3 - 1)}  | {text}");
                        idx += nrOcteti;





                    }

                    file.Close();
                    done = true; 

                }

                catch(Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }
          
           
           


        }
    }
}
