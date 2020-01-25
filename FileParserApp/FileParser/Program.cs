using System;

namespace FileParser
{
    /// <summary>
    /// Program that reads in different format files and outputs them to the screen
    /// Assumptions: 
    /// 1) Files have a header that will be discarded
    /// 2) Bad records will be written to a _bad file
    /// 3) Dates are in one of the formats that can be parsed by the system DateTime class
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reading In Files");



            Console.WriteLine("Finished Reading In Files");


            Console.WriteLine("Complete");
        }
    }
}
