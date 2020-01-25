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
        public string Gender { get; set; }
        public string FavoriteColor { get; set; }
        public DateTime DOB { get; set; }
    }
}
