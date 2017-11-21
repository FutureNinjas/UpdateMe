using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using UpdateMe.Data;
using UpdateMe.Data.Models;

namespace UpdateMe.UnitTests.DataServices.AssignmentService.Tests
{
    [TestClass]
    public class CreateAssignment_Should
    {
        [TestMethod]
        public void AddAssignmentToContext_WhenParametersAreValid()
        {
            //Arrange
            var dbContextMock = new Mock<UpdateMeDbContext>();
            var storeMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<ApplicationUserManager>(storeMock.Object);
            

            int id = 90;
            string name = "JavaScript 6 hour course";
            string description = "This is a comprehensive course for JavaScript.";
            int passScore = 60;
            DateTime dateCreated = DateTime.Parse("9 Aug 2017");

            var course = new Course()
            {
                Id = id,
                Name = name,
                Description = description,
                PassScore = passScore,
                DateCreated = dateCreated
            };


            List<Course> courses = new List<Course>()
            {
                course
            };

            var user = new ApplicationUser() { UserName = "firstuser" };

            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                user,
            };

            var usersSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(users);
            
            var assignmentsSetMock = new Mock<DbSet<Assignment>>();

            var coursesSetMock = new Mock<DbSet<Course>>().SetupData(courses);

            dbContextMock.SetupGet(m => m.Users).Returns(usersSetMock.Object);
            dbContextMock.SetupGet(a => a.Assignments).Returns(assignmentsSetMock.Object);

            string applicationUserId = user.Id;
            DateTime dueDate = DateTime.Now.AddDays(3);
            bool isMandatory = true;
            int courseId = course.Id;

            UpdateMe.Services.AssignmentService service = new UpdateMe.Services.AssignmentService(dbContextMock.Object);

            //Act
            service.CreateAssignment(dueDate, isMandatory, courseId, applicationUserId);

            //Assert
            Assert.IsNotNull(dbContextMock.Object.Assignments);


            dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
