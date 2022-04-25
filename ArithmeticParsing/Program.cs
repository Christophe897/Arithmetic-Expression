using System;
using System.IO;
using ArithmeticParsing.Properties.Parsing;

namespace ArithmeticParsing
{
    /// <summary>
    /// MainClass
    /// </summary>
    public class MainClass
    {
        /// <summary>
        /// Main: Starting point
        /// </summary>
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello \n");
                Console.WriteLine("This program will read a csv file to evaluate one expression per line");
                Console.WriteLine("Each expression should be a combination of integers with the operators +, -, *, /, {}, [], or ()");

                string path;
                if (args.Length == 0)
                {
                    Console.WriteLine("Input the path and file name, and then press enter");                  
                    path = Console.ReadLine();
                    //if (path.Trim() == "")
                    //{
                    //    path = @"/Users/christophe/Projects/testFormules.csv";
                    //}
                }
                else
                {
                    path = args[0];
                }
                

                int count = 0;
                using (var reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string strExpression = reader.ReadLine();
                        //Compute the arithmetic expression
                        NumberInt resultNumber = ParsingInteger.Evaluate(strExpression);

                        //Display the result
                        Console.WriteLine("'{0}' = {1}", strExpression, resultNumber.ToString());

                        count++;
                    }
                }
                Console.WriteLine("\n End: {0} lines evaluated \n", count);
                return;
            }
            catch(Exception ex)
            {
                Console.WriteLine("\n Sorry, there is an unexpected error: {0}", ex.Message);
            }
        }
    }
}
