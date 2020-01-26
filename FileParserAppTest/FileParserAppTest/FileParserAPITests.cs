using FileParser;
using FileParserAPI;
using FileParserAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

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
            //create a fake stream of data to represent request body
            var rawData = "Archer,Stirling,M,Green,04/09/1987";
            var bytes = System.Text.Encoding.UTF8.GetBytes(rawData.ToCharArray());
            var stream = new System.IO.MemoryStream(bytes);

            //create a fake http context to represent the request
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.Request.Body).Returns(stream);

            var sut = new FileParserController();
            //Set the controller context to simulate what the framework populates during a request
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };
            var result = sut.Post();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.IsTrue((result.Result as OkObjectResult).StatusCode == 200);
            Assert.IsTrue(RecordContainer.ApiRecords[0].Equals(new Record() { LastName = "Archer", FirstName = "Stirling", Gender = 'M', FavoriteColor = "Green", DOB = new System.DateTime(1987, 04, 09) }));
            ClearTestContainer();
        }
        [TestMethod]
        public void PostFailureTest()
        {
            //create a fake stream of data to represent request body
            var rawData = "Archer*Stirling*M*Green*04/09/1987";
            var bytes = System.Text.Encoding.UTF8.GetBytes(rawData.ToCharArray());
            var stream = new System.IO.MemoryStream(bytes);

            //create a fake http context to represent the request
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.Request.Body).Returns(stream);

            var sut = new FileParserController();
            //Set the controller context to simulate what the framework populates during a request
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };
            var result = sut.Post();
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.IsTrue((result.Result as BadRequestResult).StatusCode == 400);
            Assert.IsTrue(RecordContainer.ApiRecords.Count == 0);
            ClearTestContainer();
        }

        [TestMethod]
        public void PostEmptyTest()
        {
            var controller = new FileParserController();
            var result = controller.Post();
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            Assert.IsTrue((result.Result as BadRequestResult).StatusCode == 400);
            Assert.IsTrue(RecordContainer.ApiRecords.Count == 0);
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
