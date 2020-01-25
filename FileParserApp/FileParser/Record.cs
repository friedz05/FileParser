using System;
using System.Collections.Generic;
using System.Text;

namespace FileParser
{
    /// <summary>
    /// Container class for records
    /// </summary>
    public class Record
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public char Gender { get; set; }
        public string FavoriteColor { get; set; }
        public DateTime DOB { get; set; }

        public bool Equals(Record b)
        {
            if (LastName == b.LastName && FirstName == b.FirstName && Gender == b.Gender && FavoriteColor == b.FavoriteColor && DOB == b.DOB)
                return true;
            else return false;
        }
    }
}
