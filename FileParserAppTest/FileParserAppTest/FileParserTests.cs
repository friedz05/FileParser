using FileParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FileParserTest
{
    [TestClass]
    public class FileParserTests
    {
        Record goodRecord = new Record() { LastName = "Fry", FirstName = "Philip", Gender = 'M', FavoriteColor = "Blue", DOB = new System.DateTime(1980, 02, 09) };
        Record goodRecord2 = new Record() { LastName = "Leela", FirstName = "Turanga", Gender = 'F', FavoriteColor = "Purple", DOB = new System.DateTime(2899, 09, 30) };
        Record goodRecord3 = new Record() { LastName = "Fry", FirstName = "Yancey", Gender = 'M', FavoriteColor = "Blue", DOB = new System.DateTime(1980, 02, 09) };
        Record goodRecord4 = new Record() { LastName = "Fry", FirstName = "Philip", Gender = 'F', FavoriteColor = "Blue", DOB = new System.DateTime(1980, 02, 09) };
        Record goodRecord5 = new Record() { LastName = "Fry", FirstName = "Philip", Gender = 'M', FavoriteColor = "Pink", DOB = new System.DateTime(1980, 02, 09) };
        Record goodRecord6 = new Record() { LastName = "Fry", FirstName = "Philip", Gender = 'M', FavoriteColor = "Blue", DOB = new System.DateTime(1981, 02, 09) };
        Record readRecord1 = new Record() { LastName = "Smith", FirstName = "John", Gender = 'M', FavoriteColor = "Blue", DOB = new System.DateTime(1920, 01, 01) };
        Record readRecord2 = new Record() { LastName = "Funny", FirstName = "Doug", Gender = 'M', FavoriteColor = "Green", DOB = new System.DateTime(1988, 03, 04) };
        Record readRecord3 = new Record() { LastName = "Mayo", FirstName = "Patricia", Gender = 'F', FavoriteColor = "Red", DOB = new System.DateTime(1990, 11, 15) };
        [TestMethod]
        public void CommaDelimiterTest()
        {
            string[] commaFile = { "Fry,Philip,M,Blue,02/09/1980" };
            var result = FileParser.FileParser.ParseLines(commaFile);
            Assert.IsTrue(result[0].Equals(goodRecord));
            Assert.IsTrue(result.Count == 1);
        }
        [TestMethod]
        public void PipeDelimiterTest()
        {
            string[] pipeFile = { "Fry|Philip|M|Blue|02/09/1980" };
            var result = FileParser.FileParser.ParseLines(pipeFile);
            Assert.IsTrue(result[0].Equals(goodRecord));
            Assert.IsTrue(result.Count == 1);
        }
        [TestMethod]
        public void SpaceDelimiterTest()
        {
            string[] spaceFile = { "Fry Philip M Blue 02/09/1980" };
            var result = FileParser.FileParser.ParseLines(spaceFile);
            Assert.IsTrue(result[0].Equals(goodRecord));
            Assert.IsTrue(result.Count == 1);
        }
        [TestMethod]
        public void BadDelimiterTest()
        {
            string[] starFile = { "Fry*Philip*M*Blue*02/09/1980" };
            var result = FileParser.FileParser.ParseLines(starFile);
            Assert.IsTrue(result.Count == 0);
        }
        [TestMethod]
        public void NotEnoughFieldsTest()
        {
            string[] pipeFile = { "Fry|Philip|M|Blue" };
            var result = FileParser.FileParser.ParseLines(pipeFile);
            Assert.IsTrue(result.Count == 0);
        }
        [TestMethod]
        public void TooManyFieldsTest()
        {
            string[] pipeFile = { "Data|Fry|Philip|M|Blue|02/09/1980" };
            var result = FileParser.FileParser.ParseLines(pipeFile);
            Assert.IsTrue(result.Count == 0);
        }
        [TestMethod]
        public void BadDateandBadGender()
        {
            Record badFormatRecord = new Record() { LastName = "Fry", FirstName = "Philip", Gender = 'U', FavoriteColor = "Blue", DOB = DateTime.MinValue };
            string[] pipeFile = { "Fry|Philip|Gender|Blue|31-12/1999" };
            var result = FileParser.FileParser.ParseLines(pipeFile);
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].Equals(badFormatRecord));

        }
        [TestMethod]
        public void EmptyFileTest()
        {
            string[] pipeFile = { };
            var result = FileParser.FileParser.ParseLines(pipeFile);
            Assert.IsTrue(result.Count == 0);
        }
        [TestMethod]
        public void MultiLineTest()
        {
            string[] pipeFile = { "Fry|Philip|M|Blue|02/09/1980", "Leela|Turanga|F|Purple|09-30-2899" };
            var result = FileParser.FileParser.ParseLines(pipeFile);
            Assert.IsTrue(result[0].Equals(goodRecord));
            Assert.IsTrue(result[1].Equals(goodRecord2));
            Assert.IsTrue(result.Count == 2);
        }
        [TestMethod]
        public void RecordCompareTest()
        {
            Assert.IsTrue(goodRecord.Equals(goodRecord));
            Assert.IsFalse(goodRecord.Equals(goodRecord2));
            Assert.IsFalse(goodRecord.Equals(goodRecord3));
            Assert.IsFalse(goodRecord.Equals(goodRecord4));
            Assert.IsFalse(goodRecord.Equals(goodRecord5));
            Assert.IsFalse(goodRecord.Equals(goodRecord6));
        }
        [TestMethod]
        public void RecordReaderGoodTest()
        {
            List<Record> records = new List<Record>();
            RecordReader.ReadRecords(records, ".\\ReadTest\\");
            Assert.IsTrue(records.Count == 3);
            Assert.IsTrue(records[0].Equals(readRecord1));
            Assert.IsTrue(records[1].Equals(readRecord2));
            Assert.IsTrue(records[2].Equals(readRecord3));
        }
        [TestMethod]
        public void RecordReaderExceptionTest()
        {
            try
            {
                RecordReader.ReadRecords(null, ".\\ReadTest\\");
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is Exception);
            }
        }

        [TestMethod]
        public void RecordOutputTest()
        {
            List<Record> records = new List<Record>();
            RecordReader.ReadRecords(records, ".\\ReadTest\\");
            Assert.IsTrue(records.Count == 3);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                RecordWriter.OutputRecords(records);
                Assert.AreNotEqual(string.Empty, sw.ToString());
            }
        }

        [TestMethod]
        public void EmptyOutputTest()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                List<Record> records = new List<Record>();
                RecordWriter.OutputRecords(records);

                Assert.AreNotEqual(string.Empty, sw.ToString());
            }
        }
    }
}
