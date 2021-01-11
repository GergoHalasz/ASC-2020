using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Asamblor
{
    class Program
    {

        static void Main(string[] args)
        {
            TextReader readFile = new StreamReader("data.in"); //citesc din file datele

            string line; //un string pentru citire randurilor din file
            string bits = "00000000000000000000000000000000"; //32 bit
            string valoarereg1 = "", valoarereg2 =""; // valoarea a doua registrii
            StringBuilder bit = new StringBuilder(bits); // pentru schimbarea cu index 
           
            Regex pseudoBegin = new Regex(@"^\s*(.begin)\s*$"); //.begin test
            
            Regex pseudoOrg = new Regex(@"^\s*(.org 2048)\s*$"); //.org 2048 test

            Regex pseudoEnd = new Regex(@"^\s*(.end)\s*$");//.end test

            int lineCount = LinesCount(pseudoEnd); // randuri din file si .end check

            if (lineCount != -1)
            {
                int[] valori = new int[lineCount]; // valori vector pentru valori care au x,y,z
                                                   //.begin check                         
                line = readFile.ReadLine();
                Match matchBegin = pseudoBegin.Match(line);
                //.org 2048 check
                line = readFile.ReadLine();
                Match matchOrg = pseudoOrg.Match(line);
                if (!matchBegin.Success) // if .begin is not in the first line
                    Console.WriteLine("N-ati folosit pseudooperatie .begin!");
                else
                    if (!matchOrg.Success) // if .org 2048 is not in the second line
                    Console.WriteLine("N-ati folosit pseudooperatie .org 2048!");
                else
                {
                    Regex Instructiune = new Regex(@"^\s*([A-z,0-9]+[:]{1}){0,1}\s*(ld|addcc|jmpl|st){1}\s*(\[\w+]|%r\d+|%r\d+\+4){1}[,]{1}\s*(\[\w+]|%r\d+){1}[,]{0,1}\s*(\[\w+]|%r\d+){0,1}\s*$");//test pentru instructiunile
                    Regex ReaderVariabile = new Regex(@"^\s*([A-z,0-9]+){1}[:]{1}\s*(\d+)$");//test pentru valori care primesc numere(x: 20,y:15)

                    for (int i = 2; i < lineCount - 1; i++)//de la 2 pana la ultima randa din file
                    {
                        bit = new StringBuilder(bits); // trebuie sa fie curat dupa fiecare linie(curat=tot stringul egal cu 0)
                        line = readFile.ReadLine(); //citesc
                        Match instructiuni = Instructiune.Match(line); // test check regex instructiune
                        Match variabile = ReaderVariabile.Match(line);//test check readervariable regex
                        if (instructiuni.Success)
                        {
                            //stringuri urmatori reprezint valori din regex pe care am obtinut din grupuri "(exp)".
                            string typeOfMnemonica = instructiuni.Groups[2].Value; // ld, addcc, jmpl sau st // numele mnemonicei
                            string OperandSursa1 = instructiuni.Groups[3].Value; // [word] sau %r\d+ //registru 1
                            string OperandSursa2 = instructiuni.Groups[4].Value;// [word] sau %r\d+ //registru rd/2
                            string OperandSursa3 = instructiuni.Groups[5].Value;// [word] sau %r\d+ //registru 3

                            implementMnemonicaInBits(bit, typeOfMnemonica, OperandSursa1, OperandSursa2, OperandSursa3, lineCount, ref valoarereg1, ref valoarereg2, ref valori); //metoda pentru scriere pe consola pe bituri


                        }
                        else
                        if (variabile.Success) //daca gaseste x:20
                        {
                            char[] reverse = new char[32];
                            string binaryValoare = "";
                            string valoareOfEticheta = variabile.Groups[2].Value; // primim valoare (de ex. x = 20) primim atunci 20 cu ajutorul regexului
                            binaryValoare = valoareConvertToBinary(int.Parse(valoareOfEticheta)); // decimal to binary
                            addValoareInFormat(bit, binaryValoare, 0); // i add in the 32bit "word" binaryul
                            reverse = bit.ToString().ToArray(); // trebuie reverse pt ca de la 0 incepe string si convertit pt ca numai cu char array se poate face asa ceva
                            Array.Reverse(reverse);
                            Console.WriteLine(reverse);

                        }
                        else
                            Console.WriteLine("Instructiuni nu sunt corecte!");


                    }

                }
            }   
        }
       
        private static void implementMnemonicaInBits(StringBuilder bit, string typeOfMnemonica, string operandSursa1, string operandSursa2, string operandSursa3, int lineCount, ref string valoarereg1, ref string valoarereg2, ref int[] valori)
        {
            char[] reverse = new char[32];
            int count = 0;
            string binaryValoare;
            string valoare = "";
            if (typeOfMnemonica == "ld")
            {
                bit = bitsToOne(bit, 0, 2);
                if (operandSursa1[0] == '[') //daca primul operand sursa este [exp] 
                {
                    bit[13] = '1';
                    
                   findContinutulLocatiei(operandSursa1, lineCount , ref valoare , ref count);
                    
                    binaryValoare = valoareConvertToBinary(2048 + 4 * count);//2048* count * 4 byte
                    
                    addValoareInFormat(bit, binaryValoare, 0); //simm13 este la sfarsit adica 0

                    operandSursa2 = operandSursa2.Trim('r', '%'); // ramana numai digit
                    valori[int.Parse(operandSursa2) - 1] = int.Parse(valoare);//stochez valoarea primita
                    binaryValoare = valoareConvertToBinary(int.Parse(operandSursa2));
                    addValoareInFormat(bit, binaryValoare, 25); //registru destinatie
                    reverse = bit.ToString().ToArray();
                    Array.Reverse(reverse);
                    Console.WriteLine(reverse);
                    
                }
            }
            else
            if (typeOfMnemonica == "st")
            {
                bit = bitsToOne(bit, 0, 2);
                bit[21] = '1';

                if(operandSursa2[0]=='[')
                {
                    bit[13] = '1';
                    operandSursa1 = operandSursa1.Trim('%', 'r');
                    binaryValoare = valoareConvertToBinary(int.Parse(operandSursa1));
                    addValoareInFormat(bit, binaryValoare, 25); //rd


                    findContinutulLocatiei(operandSursa2, lineCount, ref valoare, ref count);
                    binaryValoare = valoareConvertToBinary(2048 + 4 * count);

                    addValoareInFormat(bit, binaryValoare, 0); //simm13 
         

                }
                reverse = bit.ToString().ToArray();
                Array.Reverse(reverse);
                Console.WriteLine(reverse);
            }
            else
            if(typeOfMnemonica == "addcc")
            {
                bit = bitsToOne(bit, 0, 1);
                bit[23] = '1';
                
                operandSursa1 = operandSursa1.Trim('%', 'r');
                operandSursa2 = operandSursa2.Trim('%', 'r');
                operandSursa3 = operandSursa3.Trim('%', 'r');
                valori[int.Parse(operandSursa3) - 1] = valori[int.Parse(operandSursa1) - 1] + valori[int.Parse(operandSursa2) - 1];//valoarea lui %r3

                binaryValoare = valoareConvertToBinary(int.Parse(operandSursa1));
                addValoareInFormat(bit, binaryValoare, 14); //rs1

                binaryValoare = valoareConvertToBinary(int.Parse(operandSursa2));
                addValoareInFormat(bit, binaryValoare, 0);//rs2

                binaryValoare = valoareConvertToBinary(int.Parse(operandSursa3));
                addValoareInFormat(bit, binaryValoare, 25);//rd

            
                reverse = bit.ToString().ToArray();
                Array.Reverse(reverse);
                Console.WriteLine(reverse);
                

            }
            else
                if(typeOfMnemonica == "jmpl")
            {
                bit = bitsToOne(bit, 0, 1);
                bit = bitsToOne(bit, 7, 10);
                bit[13] = '1';
                
                operandSursa2 = operandSursa2.Trim('%', 'r');

                binaryValoare = valoareConvertToBinary(int.Parse(operandSursa2));
                addValoareInFormat(bit, binaryValoare, 29); //rd

                addValoareInFormat(bit, "1111", 14);//rs1

                addValoareInFormat(bit, "100", 0); //simm13

                reverse = bit.ToString().ToArray();
                Array.Reverse(reverse);
                Console.WriteLine(reverse);
            }

                

        }
       
        private static void addValoareInFormat(StringBuilder bit, string binaryValoare, int EndIndex)
        {
            int j = 0;
          
                for (int i = EndIndex+binaryValoare.Length -1 ; i >= EndIndex; i--)
                {
                    bit[i] = binaryValoare[j];
                    j++;
                }
         
                
        }

       
        private static string valoareConvertToBinary(int valoare)
        {
            int rest;
            string converted = "";
            while(valoare!=0)
            {
                rest = valoare % 2;
                converted = rest.ToString() + converted;
                valoare /= 2;
            }
            return converted;
        }

        private static void findContinutulLocatiei(string operandSursa1, int lineCount , ref string valoare, ref int count)
        {
             count = -1;
         
            operandSursa1 = operandSursa1.Trim('[', ']');

            TextReader textReader = new StreamReader("data.in");

            string line;

            Regex continutReader = new Regex(@"^\s*([A-z,0-9]+){1}[:]{1}\s*(\d+)$"); // daca gasesc o forma ca x: 20 in file
            line = textReader.ReadLine();//.begin escape pt ca numerez instructiuni
            line = textReader.ReadLine();//.org escape
            for (int i = 2; i < lineCount; i++)
            {
                line = textReader.ReadLine();
                Match continutMatch = continutReader.Match(line);

                string continutNume = continutMatch.Groups[1].Value;
                count++;
                if (continutNume == operandSursa1)
                { valoare = continutMatch.Groups[2].Value; break; }
            }
        }



    

        private static StringBuilder bitsToOne(StringBuilder bit, int startIndex, int EndIndex)
        {
            for (int i = 31-startIndex; i >31 - EndIndex; i--)
            {
                bit[i] = '1';
            }

            return bit;
        }

       

        public static int LinesCount(Regex pseudoEnd)
        {
            TextReader code = new StreamReader("data.in");
            int count = 0, tempCount =0;
            string line = "";
            bool ok = true;
            while (ok)
            {
                line = code.ReadLine();
                count++;
                
                if (line == null)
                {
                    if (count - tempCount == 1) // if .end is at the end
                        return count - 1;
                    else
                    {

                        Console.WriteLine("N -ati folosit pseudooperatie .end deloc/la loc buna!");
                        return -1;
                    }
                }
                else
                {
                    Match matchEnd = pseudoEnd.Match(line); // daca gasesc .end oriunde in file
                    if (matchEnd.Success)
                        tempCount = count;
                }
            }
            return -1;
        }
    }
}
