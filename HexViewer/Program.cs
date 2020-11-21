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
            string path = @"A:\Untitled.txt";

            if(File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream fs = File.Create(path))
            {
               
               
            }

        }
    }
}
