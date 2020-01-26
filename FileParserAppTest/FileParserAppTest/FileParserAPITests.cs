using FileParser;
using FileParserAPI;
using FileParserAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FileParserTest
{
    [TestClass]
    public class FileParserAPITests
    {
        [TestMethod]
        public void GetTest()
        {
            var controller = new FileParserController();

            var result = controller.Get();
            Assert.AreEqual(result, "FileParser");
        }

        [TestMethod]
        public void GetEmptyTest()
        {
            var controller = new FileParserController();

            var result = controller.GetByName() as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).Count() == 0);

            result = controller.GetByGender() as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).Count() == 0);

            result = controller.GetByBirthDate() as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).Count() == 0);
        }

        [TestMethod]
        public void GetByNameTest()
        {
            var controller = new FileParserController();
            PopulateTestContainer();
            var result = controller.GetByName() as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(0).Equals(RecordContainer.ApiRecords[2]));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(1).Equals(RecordContainer.ApiRecords[3]));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(2).Equals(RecordContainer.ApiRecords[1]));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(3).Equals(RecordContainer.ApiRecords[4]));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(4).Equals(RecordContainer.ApiRecords[0]));
            ClearTestContainer();
        }

        [TestMethod]
        public void GetByGenderTest()
        {
            PopulateTestContainer();
            var controller = new FileParserController();
            var result = controller.GetByGender() as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(0).Equals(RecordContainer.ApiRecords[0]));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(1).Equals(RecordContainer.ApiRecords[2]));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(2).Equals(RecordContainer.ApiRecords[4]));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(3).Equals(RecordContainer.ApiRecords[1]));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(4).Equals(RecordContainer.ApiRecords[3]));
            ClearTestContainer();
        }

        [TestMethod]
        public void GetByDateOfBirthTest()
        {
            PopulateTestContainer();
            var controller = new FileParserController();
            var result = controller.GetByBirthDate() as OkObjectResult;
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(0).Equals(RecordContainer.ApiRecords[4]));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(1).Equals(RecordContainer.ApiRecords[2]));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(2).Equals(RecordContainer.ApiRecords[1]));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(3).Equals(RecordContainer.ApiRecords[3]));
            Assert.IsTrue((result.Value as IOrderedEnumerable<Record>).ElementAt(4).Equals(RecordContainer.ApiRecords[0]));
            ClearTestContainer();
        }

        [TestMethod]
        public void PostSuccessTest()
        {
            
            ClearTestContainer();
        }
        [TestMethod]
        public void PostFailureTest()
        {

            ClearTestContainer();
        }

        [TestMethod]
        public void PostEmptyTest()
        {

            ClearTestContainer();
        }

        private void PopulateTestContainer()
        {
            RecordContainer.ApiRecords.Add(new Record() { LastName = "Leela", FirstName = "Turanga", Gender = 'F', FavoriteColor = "Purple", DOB = new DateTime(2899, 09, 30) });
            RecordContainer.ApiRecords.Add(new Record() { LastName = "Fry", FirstName = "Philip", Gender = 'M', FavoriteColor = "Blue", DOB = new System.DateTime(1980, 02, 09) });
            RecordContainer.ApiRecords.Add(new Record() { LastName = "Archer", FirstName = "Mallory", Gender = 'F', FavoriteColor = "Red", DOB = new DateTime(1952, 10, 31) });
            RecordContainer.ApiRecords.Add(new Record() { LastName = "Archer", FirstName = "Stirling", Gender = 'M', FavoriteColor = "Green", DOB = new System.DateTime(1987, 04, 09) });
            RecordContainer.ApiRecords.Add(new Record() { LastName = "Jones", FirstName = "Martha", Gender = 'F', FavoriteColor = "Orange", DOB = new System.DateTime(1945, 01, 31) });

        }

        private void ClearTestContainer()
        {
            RecordContainer.ApiRecords.Clear();
        }

    }
}
