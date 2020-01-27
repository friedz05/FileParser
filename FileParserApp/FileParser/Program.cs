using System;
using System.Collections.Generic;
using System.Configuration;

namespace FileParser
{
    /// <summary>
    /// Program that reads in different format files and outputs them to the screen
    /// Assumptions: 
    /// 1) Files don't have a header
    /// 2) Bad records will be discarded
    /// 3) Dates are in one of the formats that can be parsed by the system DateTime class
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            List<Record> records = new List<Record>();
            RecordReader.ReadRecords(records, ConfigurationManager.AppSettings.Get("inputPath").ToString());
            RecordWriter.OutputRecords(records);
        }
    }
}
