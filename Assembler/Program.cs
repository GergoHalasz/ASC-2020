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
            TextReader readInstructionsSet = new StreamReader("data.in"); 

            string InstructionsOneByOne; 
            string oneInstructionToAWord32bit = "00000000000000000000000000000000"; 
            string registerNr1Value = "", registerNr2Value =""; 
            
           
            Regex checkIfPseudoOperationBeginIsInTheSet = new Regex(@"^\s*(.begin)\s*$");
            
            Regex goCheckIfOperationPseudoOrg2048IsInTheSet = new Regex(@"^\s*(.org 2048)\s*$"); 

            Regex checkingPseudoOperationEndIsInTheSet = new Regex(@"^\s*(.end)\s*$");

            int ALL_INSTRUCTIONS_IN_THE_SET = countInstructionsInTheSetAndCheckPseudoOperationEnd(checkingPseudoOperationEndIsInTheSet);
            
            int PSEUDO_OPERATION_NOT_EXIST = -1;

            if (ALL_INSTRUCTIONS_IN_THE_SET != PSEUDO_OPERATION_NOT_EXIST)
            {

                int[] storeValuesThatAreInTheSet = new int[ALL_INSTRUCTIONS_IN_THE_SET]; 
                                                                            
                InstructionsOneByOne = readInstructionsSet.ReadLine();
                Match pseudoOperationBeginIsInTheSet = checkIfPseudoOperationBeginIsInTheSet.Match(InstructionsOneByOne);
                //.org 2048 check
                InstructionsOneByOne = readInstructionsSet.ReadLine();
                Match pseudoOperationOrgIsInTheSet = goCheckIfOperationPseudoOrg2048IsInTheSet.Match(InstructionsOneByOne);
                if (!pseudoOperationBeginIsInTheSet.Success) 
                    Console.WriteLine("N-ati folosit pseudooperatie .begin!");
                else
                    if (!pseudoOperationOrgIsInTheSet.Success) 
                    Console.WriteLine("N-ati folosit pseudooperatie .org 2048!");
                else
                {
                    Regex checkingIfInstructionIsCorrect = new Regex(@"^\s*([A-z,0-9]+[:]{1}){0,1}\s*(ld|addcc|jmpl|st){1}\s*(\[\w+]|%r\d+|%r\d+\+4){1}[,]{1}\s*(\[\w+]|%r\d+){1}[,]{0,1}\s*(\[\w+]|%r\d+){0,1}\s*$");
                    Regex checkingIfInstructionsWithValuesAreCorrect = new Regex(@"^\s*([A-z,0-9]+){1}[:]{1}\s*(\d+)$");
                    int startReadingInstructionsAfterPseudoOperations = 2;
                    for (int i = startReadingInstructionsAfterPseudoOperations; i < ALL_INSTRUCTIONS_IN_THE_SET - 1; i++)
                    {
                        StringBuilder addingValuesToAWord32bit = new StringBuilder(oneInstructionToAWord32bit); 
                        InstructionsOneByOne = readInstructionsSet.ReadLine(); 
                        Match ifInstructionIsCorrect = checkingIfInstructionIsCorrect.Match(InstructionsOneByOne);
                        Match ifValueInstructionIsCorrect = checkingIfInstructionsWithValuesAreCorrect.Match(InstructionsOneByOne);//test check readervariable regex
                        if (ifInstructionIsCorrect.Success)
                        {
                            int ld_addcc_jmpl_or_st = 2;
                            int value_or_register_number_one = 3;
                            int value_or_register_number_two = 4;
                            int value_or_register_number_three = 5;
                            string typeOfMnemonica = ifInstructionIsCorrect.Groups[ld_addcc_jmpl_or_st].Value; 
                            string OperandSursa1 = ifInstructionIsCorrect.Groups[value_or_register_number_one].Value; 
                            string OperandSursa2 = ifInstructionIsCorrect.Groups[value_or_register_number_two].Value;
                            string OperandSursa3 = ifInstructionIsCorrect.Groups[value_or_register_number_three].Value;

                            AddingOperationsAsValuesToAWord32bit(addingValuesToAWord32bit, typeOfMnemonica, OperandSursa1, OperandSursa2, OperandSursa3, ALL_INSTRUCTIONS_IN_THE_SET, ref registerNr1Value, ref registerNr2Value, ref storeValuesThatAreInTheSet); 


                        }
                        else
                        if (ifValueInstructionIsCorrect.Success) //daca gaseste x:20
                        {
                            char[] reverseCharactersInAWord32bit = new char[32];
                            string convertedValueInBinary = "";
                            int ETICHETA_NAME = 2;
                            string valueOfEticheta = ifValueInstructionIsCorrect.Groups[ETICHETA_NAME].Value; 
                            convertedValueInBinary = valueConvertToBinary(int.Parse(valueOfEticheta)); 
                            addSpecificValuesInAWord32Bit(addingValuesToAWord32bit, convertedValueInBinary, 0); 
                            reverseCharactersInAWord32bit = addingValuesToAWord32bit.ToString().ToArray(); 
                            Console.WriteLine(reverseCharactersInAWord32bit);

                        }
                        else
                            Console.WriteLine("Instructiuni nu sunt corecte!");


                    }

                }
            }   
        }
       
        private static void AddingOperationsAsValuesToAWord32bit(StringBuilder addingValuesToAWord32bit, string typeOfMnemonica, string operandSursa1, string operandSursa2, string operandSursa3, int howManyInstructionsAreInTheSet, ref string registerNr1Value, ref string registerNr2Value, ref int[] storeValuesThatAreInTheSet)
        {
            char[] reverseCharactersInAWord32bit = new char[32];
            int count = 0;
            string binaryValoare;
            string valoare = "";
            if (typeOfMnemonica == "ld")
            {
                addingValuesToAWord32bit = bitsToOne(addingValuesToAWord32bit, 0, 2);
                if (operandSursa1[0] == '[') 
                {
                    addingValuesToAWord32bit[13] = '1';
                    
                   findContinutulLocatiei(operandSursa1, howManyInstructionsAreInTheSet , ref valoare , ref count);
                    
                    binaryValoare = valueConvertToBinary(2048 + 4 * count);
                    
                    addSpecificValuesInAWord32Bit(addingValuesToAWord32bit, binaryValoare, 0); 

                    operandSursa2 = operandSursa2.Trim('r', '%');
                    storeValuesThatAreInTheSet[int.Parse(operandSursa2) - 1] = int.Parse(valoare);
                    binaryValoare = valueConvertToBinary(int.Parse(operandSursa2));
                    addSpecificValuesInAWord32Bit(addingValuesToAWord32bit, binaryValoare, 25); 
                    reverseCharactersInAWord32bit = addingValuesToAWord32bit.ToString().ToArray();
                    Array.Reverse(reverseCharactersInAWord32bit);
                    Console.WriteLine(reverseCharactersInAWord32bit);
                    
                }
            }
            else
            if (typeOfMnemonica == "st")
            {
                addingValuesToAWord32bit = bitsToOne(addingValuesToAWord32bit, 0, 2);
                addingValuesToAWord32bit[21] = '1';

                if(operandSursa2[0]=='[')
                {
                    addingValuesToAWord32bit[13] = '1';
                    operandSursa1 = operandSursa1.Trim('%', 'r');
                    binaryValoare = valueConvertToBinary(int.Parse(operandSursa1));
                    addSpecificValuesInAWord32Bit(addingValuesToAWord32bit, binaryValoare, 25); //rd


                    findContinutulLocatiei(operandSursa2, howManyInstructionsAreInTheSet, ref valoare, ref count);
                    binaryValoare = valueConvertToBinary(2048 + 4 * count);

                    addSpecificValuesInAWord32Bit(addingValuesToAWord32bit, binaryValoare, 0); //simm13 
         

                }
                reverseCharactersInAWord32bit = addingValuesToAWord32bit.ToString().ToArray();
                Array.Reverse(reverseCharactersInAWord32bit);
                Console.WriteLine(reverseCharactersInAWord32bit);
            }
            else
            if(typeOfMnemonica == "addcc")
            {
                addingValuesToAWord32bit = bitsToOne(addingValuesToAWord32bit, 0, 1);
                addingValuesToAWord32bit[23] = '1';
                
                operandSursa1 = operandSursa1.Trim('%', 'r');
                operandSursa2 = operandSursa2.Trim('%', 'r');
                operandSursa3 = operandSursa3.Trim('%', 'r');
                storeValuesThatAreInTheSet[int.Parse(operandSursa3) - 1] = storeValuesThatAreInTheSet[int.Parse(operandSursa1) - 1] + storeValuesThatAreInTheSet[int.Parse(operandSursa2) - 1];//valoarea lui %r3

                binaryValoare = valueConvertToBinary(int.Parse(operandSursa1));
                addSpecificValuesInAWord32Bit(addingValuesToAWord32bit, binaryValoare, 14); //rs1

                binaryValoare = valueConvertToBinary(int.Parse(operandSursa2));
                addSpecificValuesInAWord32Bit(addingValuesToAWord32bit, binaryValoare, 0);//rs2

                binaryValoare = valueConvertToBinary(int.Parse(operandSursa3));
                addSpecificValuesInAWord32Bit(addingValuesToAWord32bit, binaryValoare, 25);//rd

            
                reverseCharactersInAWord32bit = addingValuesToAWord32bit.ToString().ToArray();
                Array.Reverse(reverseCharactersInAWord32bit);
                Console.WriteLine(reverseCharactersInAWord32bit);
                

            }
            else
                if(typeOfMnemonica == "jmpl")
            {
                addingValuesToAWord32bit = bitsToOne(addingValuesToAWord32bit, 0, 1);
                addingValuesToAWord32bit = bitsToOne(addingValuesToAWord32bit, 7, 10);
                addingValuesToAWord32bit[13] = '1';
                
                operandSursa2 = operandSursa2.Trim('%', 'r');

                binaryValoare = valueConvertToBinary(int.Parse(operandSursa2));
                addSpecificValuesInAWord32Bit(addingValuesToAWord32bit, binaryValoare, 29); //rd

                addSpecificValuesInAWord32Bit(addingValuesToAWord32bit, "1111", 14);//rs1

                addSpecificValuesInAWord32Bit(addingValuesToAWord32bit, "100", 0); //simm13

                reverseCharactersInAWord32bit = addingValuesToAWord32bit.ToString().ToArray();
                Array.Reverse(reverseCharactersInAWord32bit);
                Console.WriteLine(reverseCharactersInAWord32bit);
            }

                

        }
       
        private static void addSpecificValuesInAWord32Bit(StringBuilder bit, string binaryValoare, int EndIndex)
        {
            int j = 0;
          
                for (int i = EndIndex+binaryValoare.Length -1 ; i >= EndIndex; i--)
                {
                    bit[i] = binaryValoare[j];
                    j++;
                }
         
                
        }

       
        private static string valueConvertToBinary(int valoare)
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

       

        public static int countInstructionsInTheSetAndCheckPseudoOperationEnd(Regex checkingPseudoOperationEndIsInTheSet)
        {
            TextReader readInstructionsOneByOne = new StreamReader("data.in");
            int countingInstructionsInSet = 0, checkingIfEndOperationIsInTheEndOfTheSet =0;
            string instruction = "";
            bool ok = true;
            while (ok)
            {
                instruction = readInstructionsOneByOne.ReadLine();
                countingInstructionsInSet++;
                
                if (instruction == null)
                {
                    if (countingInstructionsInSet - checkingIfEndOperationIsInTheEndOfTheSet == 1) 
                        return countingInstructionsInSet - 1;
                    else
                    {

                        Console.WriteLine("N -ati folosit pseudooperatie .end deloc/la loc buna!");
                        return -1;
                    }
                }
                else
                {
                    Match matchEnd = checkingPseudoOperationEndIsInTheSet.Match(instruction);
                    if (matchEnd.Success)
                        checkingIfEndOperationIsInTheEndOfTheSet = countingInstructionsInSet;
                }
            }
            return -1;
        }
    }
}
