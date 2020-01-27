using System;
using System.Collections.Generic;
using System.Linq;

namespace FileParser
{
    public class RecordWriter
    {
        public static void OutputRecords(List<Record> records)
        {
            Console.WriteLine("Finished Reading In Files. Output 1 Sort by Gender (F then M) then Last Name");
            foreach (var output1 in records.OrderBy(x => x.Gender).ThenBy(y => y.LastName))
                Console.WriteLine("{0} {1} {2} {3} {4}", output1.LastName, output1.FirstName, output1.Gender, output1.FavoriteColor, output1.DOB.ToString("MM/dd/yyyy"));
            Console.WriteLine("\n Output 2 Sort by Birth Date ascending");
            foreach (var output2 in records.OrderBy(x => x.DOB))
                Console.WriteLine("{0} {1} {2} {3} {4}", output2.LastName, output2.FirstName, output2.Gender, output2.FavoriteColor, output2.DOB.ToString("MM/dd/yyyy"));
            Console.WriteLine("\n Output 3 Sort by Last Name descending");
            foreach (var output3 in records.OrderByDescending(y => y.LastName))
                Console.WriteLine("{0} {1} {2} {3} {4}", output3.LastName, output3.FirstName, output3.Gender, output3.FavoriteColor, output3.DOB.ToString("MM/dd/yyyy"));
            Console.WriteLine("\n Complete Output");
        }
    }
}
