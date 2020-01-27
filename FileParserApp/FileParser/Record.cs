using System;

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

        /// <summary>
        /// Method to compare to Records
        /// </summary>
        /// <param name="b">Record to compare to</param>
        /// <returns>True if equal, false otherwise</returns>
        public bool Equals(Record b)
        {
            if (LastName == b.LastName && FirstName == b.FirstName && Gender == b.Gender && FavoriteColor == b.FavoriteColor && DOB == b.DOB)
                return true;
            else return false;
        }
    }
}
