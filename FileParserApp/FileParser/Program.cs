using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace FileParser
{
    /// <summary>
    /// Program that reads in different format files and outputs them to the screen
    /// Assumptions: 
    /// 1) Files have a header that will be discarded
    /// 2) Bad records will be discarded
    /// 3) Dates are in one of the formats that can be parsed by the system DateTime class
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            List<Record> readRecords = new List<Record>();
            Console.WriteLine("Reading In Files");

            var inputPath = ConfigurationManager.AppSettings.Get("inputPath").ToString();

            try
            { 
                foreach(var file in Directory.GetFiles(inputPath))
                {
                    var lines = File.ReadAllLines(file);
                    readRecords.AddRange(FileParser.ParseLines(lines));
                }
            }
            catch
            {
                Console.WriteLine($"Failure parsing a file in {0}", inputPath );
            }



            Console.WriteLine("Finished Reading In Files");


            Console.WriteLine("Complete");
        }
    }
}
