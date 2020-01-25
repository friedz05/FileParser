using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace FileParser
{
    /// <summary>
    /// Program that reads in different format files and outputs them to the screen
    /// Assumptions: 
    /// 1) Files don't have a header
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



            Console.WriteLine("Finished Reading In Files. Output 1 Sort by Gender (F then M) then Last Name");
            foreach (var output1 in readRecords.OrderBy(x => x.Gender).ThenBy(y => y.LastName))
                Console.WriteLine("{0} {1} {2} {3} {4}", output1.LastName, output1.FirstName, output1.Gender, output1.FavoriteColor, output1.DOB.ToString("MM/dd/yyyy"));
            Console.WriteLine();
            Console.WriteLine("Output 2 Sort by Birth Date ascending");
            foreach (var output2 in readRecords.OrderBy(x => x.DOB))
                Console.WriteLine("{0} {1} {2} {3} {4}", output2.LastName, output2.FirstName, output2.Gender, output2.FavoriteColor, output2.DOB.ToString("MM/dd/yyyy"));
            Console.WriteLine();
            Console.WriteLine("Output 3 Sort by Last Name descending");
            foreach (var output3 in readRecords.OrderByDescending(y => y.LastName))
                Console.WriteLine("{0} {1} {2} {3} {4}", output3.LastName, output3.FirstName, output3.Gender, output3.FavoriteColor, output3.DOB.ToString("MM/dd/yyyy"));
            Console.WriteLine();
            Console.WriteLine("Complete");
        }
    }
}
