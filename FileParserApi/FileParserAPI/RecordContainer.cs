using FileParser;
using System.Collections.Generic;

namespace FileParserAPI
{
    /// <summary>
    /// Just for simplicity and to not have persistent data.  
    /// Would be replaced with a database or some other data storage
    /// </summary>
    public static class RecordContainer
    {

        private static List<Record> cache;
        private static object cacheLock = new object();
        public static List<Record> ApiRecords
        {
            get
            {
                lock (cacheLock)
                {
                    if (cache == null)
                    {
                        cache = new List<Record>();
                    }
                    return cache;
                }
            }
        }
    }
}
