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
            string name = "ValidateStringTestName";
            string description = "ValidateStringTestDescription";
            int id = 1;
            int passScore = 99;

            string nameMock = "MockValidateStringTestName";
            string descriptionMock = "MockValidateStringTestDescription";
            int passScoreMock = 1;

            var contextMock = new Mock<UpdateMeDbContext>();
            var course = new Course() { Id = id, Name = name, Description = description, PassScore = passScore };
            var courseViewModel = new CourseViewModel() { Id = id, Name = nameMock, Description = descriptionMock, PassScore = passScoreMock };

            List<Course> courses = new List<Course>() { course };
            // courses.Add(course);

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