using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateMe.Data;
using UpdateMe.Data.Models;

namespace UpdateMe.UnitTests.Areas.Admin.Controllers
{
    [TestClass]
    class ListAllCourses_Should
    {
        [TestMethod]
        public void ListAllCourses_WhenParametersAreValid()
        {
            //Arrange
            var dbContextMock = new Mock<UpdateMeDbContext>();
            var coursesMock = new Mock<Course>();
            var slidesMock = new Mock<Slide>();
            var questionsMock = new Mock<Question>();
            var course = new Course()
            {
                Name = "Test Course Title",
                Id = 1,
                DateCreated = DateTime.Parse("10 Jan 2015"),
                Description = "This is just some random text for testing purposes",
                PassScore = 45,                
            };

        }
    }
}
