using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;
using UpdateMe.Controllers;
using UpdateMe.Services.Contracts;

namespace UpdateMe.UnitTests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void About()
        {
            var courseServiceMock = new Mock<ICourseService>();
            // Arrange
            HomeController controller = new HomeController(courseServiceMock.Object);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            var courseServiceMock = new Mock<ICourseService>();
            // Arrange
            HomeController controller = new HomeController(courseServiceMock.Object);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
