using System;
using System.Collections.Generic;

namespace FileParser
{
    public class FileParser
    {
        /// <summary>
        /// Grabs all of the lines in the file, assigns the delimiter, and creates new 
        /// Records
        /// </summary>
        /// <param name="fileLines"></param>
        /// <returns>Any records able to be parsed from the file </returns>
        public static List<Record> ParseLines (string[] fileLines)
        {
            List<Record> temp = new List<Record>();
            char delimiter;
            // Assume the first line is the header
            // Make sure the file has a first line and get the delimiter.  Return if not in the right format
            if (fileLines.Length > 0)
            {
                if (fileLines[0].IndexOf('|') > -1)
                    delimiter = '|';
                else if (fileLines[0].IndexOf(',') > -1)
                    delimiter = ',';
                else if (fileLines[0].IndexOf(' ') > -1)
                    delimiter = ' ';
                else
                    return temp;
            }
            else
            {
                return temp;
            }

            for (int i = 0; i < fileLines.Length; i++)
            {
                var fields = fileLines[i].Split(delimiter);
                if (fields.Length != 5)
                {
                    continue;
                }
                temp.Add(new Record()
                {
                    LastName = fields[0],
                    FirstName = fields[1],
                    // Try to Parse the gender, expecting a character only, assign to Unknown if it's more
                    Gender = char.TryParse(fields[2], out _) ? char.Parse(fields[2]) : 'U',
                    FavoriteColor = fields[3],
                    // Try to Parse the Date of Birth, _ is a discard value.  If it fails assign to min date
                    DOB = DateTime.TryParse(fields[4], out _) ? DateTime.Parse(fields[4]) : DateTime.MinValue
                });
            }
            return temp;
        }

    }
}
