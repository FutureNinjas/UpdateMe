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
    [Ignore]
    [TestClass]
    public class ReviewCourse_Should
    {
        [Ignore]
        [TestMethod]
        public void ReturnPropperDataWhenPropperIdIsPassed()
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



        }
    }
}
