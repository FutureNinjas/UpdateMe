using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpdateMe;
using UpdateMe.Controllers;
using UpdateMe.Data;
using Moq;

namespace UpdateMe.UnitTests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var dbContextMock = new Mock<UpdateMeDbContext>();

            // Arrange
            HomeController controller = new HomeController(dbContextMock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            var dbContextMock = new Mock<UpdateMeDbContext>();
            // Arrange
            HomeController controller = new HomeController(dbContextMock.Object);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            var dbContextMock = new Mock<UpdateMeDbContext>();
            // Arrange
            HomeController controller = new HomeController(dbContextMock.Object);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
