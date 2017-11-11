using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            int passScore = 99;
            string nameMock = "MockValidateStringTestName";
            string descriptionMock = "MockValidateStringTestDescription";
            int passScoreMock = 1;
            var contextMock = new Mock<UpdateMeDbContext>();
            var course = new Course() { Id = 1 , Name = name, Description = description, PassScore = passScore };
            var courseViewModel = new CourseViewModel() { Id = 1, Name = nameMock, Description = descriptionMock, PassScore = passScoreMock };

            List<Course> courses = new List<Course>();
            courses.Add(course);

            var coursesSetMock = new Mock<DbSet<Course>>().SetupData(courses);
            contextMock.SetupGet(m => m.Courses).Returns(coursesSetMock.Object);
            var courseService = new CourseService(contextMock.Object);

            //Act


            courseService.EditCourse(1, courseViewModel);

            //Assert

            //Assert.AreSame(course.Name, courseViewModel.Name);
            contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
