using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using UpdateMe.Data;
using UpdateMe.Data.Models;
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
            var courseViewModel = new CourseViewModel() { Id = id, Name = "testName", Description = "testDescription", PassScore = 1 };

            List<Course> courses = new List<Course>() { course };

            var coursesSetMock = new Mock<DbSet<Course>>().SetupData(courses);
            contextMock.SetupGet(m => m.Courses).Returns(coursesSetMock.Object);
            var courseService = new CourseService(contextMock.Object);

            //Act            
            courseService.EditCourse(course.Id, courseViewModel);

            //Assert
            Assert.AreEqual(courseViewModel.Name, course.Name);
            Assert.AreEqual(courseViewModel.Description, course.Description);
            Assert.AreEqual(courseViewModel.PassScore, course.PassScore);
            contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}