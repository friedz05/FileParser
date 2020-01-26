using FileParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FileParserTest
{
    [TestClass]
    public class FileParserTests
    {
        Record goodRecord = new Record() { LastName = "Fry", FirstName = "Philip", Gender = 'M', FavoriteColor = "Blue", DOB = new System.DateTime(1980, 02, 09) };
       
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
            Record goodRecord2 = new Record() { LastName = "Leela", FirstName = "Turanga", Gender = 'F', FavoriteColor = "Purple", DOB = new System.DateTime(2899, 09, 30) };
            string[] pipeFile = { "Fry|Philip|M|Blue|02/09/1980", "Leela|Turanga|F|Purple|09-30-2899" };
            var result = FileParser.FileParser.ParseLines(pipeFile);
            Assert.IsTrue(result[0].Equals(goodRecord));
            Assert.IsTrue(result[1].Equals(goodRecord2));
            Assert.IsTrue(result.Count == 2);
        }
    }
}
