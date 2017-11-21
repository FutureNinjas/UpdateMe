using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using UpdateMe.Models;
using UpdateMe.Services;

namespace UpdateMe.UnitTests.DataServices.CourseServiceTests
{
    [TestClass]
    public class EditCourse_Should
    {
        [TestMethod]
        public void EditCourseProperties_WhenParametersAreValid()
        {
            //Arrange
            int id = 1;
            
            var contextMock = new Mock<UpdateMeDbContext>();
            var course = new Course() { Id = id };

            string name = "testName";
            string description = "testDescription";
            int passScore = 1;
            
            List<Course> courses = new List<Course>() { course };

            var coursesSetMock = new Mock<DbSet<Course>>().SetupData(courses);
            contextMock.SetupGet(m => m.Courses).Returns(coursesSetMock.Object);
            var courseService = new CourseService(contextMock.Object);

            //Act            
            courseService.EditCourse(course, name, description, passScore);

            //Assert
            Assert.AreEqual(course.Name, name);
            Assert.AreEqual(course.Description, description);
            Assert.AreEqual(course.PassScore, passScore);
            contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}