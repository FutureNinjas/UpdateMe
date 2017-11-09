using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using System.Data.Entity;
using UpdateMe.Services;

namespace UpdateMe.UnitTests.DataServices.CourseServiceTests
{
    [TestClass]
    public class DeleteCourseShould
    {
        [TestMethod]
        public void DeleteCourseFromContext_WHenParametersAreValid()
        {
            //Arrange
            var dbContextMock = new Mock<UpdateMeDbContext>();
            int id = 22;
            string name = "C# Advanced";
            string description = "This is a comprehensive C# Advanced course.";
            int passScore = 80;
            DateTime dateCreated = DateTime.Parse("2 Jan 2015");

            List<Course> courses = new List<Course>()
            {
                new Course()
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    PassScore = passScore,
                    DateCreated = dateCreated
                }
            };

            var coursesSetMock = new Mock<DbSet<Course>>().SetupData(courses);

            dbContextMock.SetupGet(m => m.Courses).Returns(coursesSetMock.Object);

            CourseServices service = new CourseServices(dbContextMock.Object);

            //Act
            service.DeleteCourse(id);

            //Assert
            Assert.IsTrue(dbContextMock.Object.Courses.Count() == 0);

            dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
