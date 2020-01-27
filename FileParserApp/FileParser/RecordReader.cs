using System;
using System.Collections.Generic;
using System.IO;

namespace FileParser
{
    public class RecordReader
    {
        public static void ReadRecords(List<Record> records, string inputPath)
        {           
            try
            {
                foreach (var file in Directory.GetFiles(inputPath))
                {
                    var lines = File.ReadAllLines(file);
                    records.AddRange(FileParser.ParseLines(lines));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failure parsing a file in {0}", inputPath);
                throw ex;
            }
        }
    }
}
