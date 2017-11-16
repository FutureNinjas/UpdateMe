using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UpdateMe.Data;
using UpdateMe.Data.Models;

namespace UpdateMe.UnitTests.DataServices.AssignmentService.Tests
{
    [TestClass]
    public class ListAllAssignmentsFromUser_Should
    {
        [Ignore]
        [TestMethod]
        public void ReturnAllUserAssignmentsFromDb_WhenParametersAreValid()
        {
            //Arrange
            var dbContextMock = new Mock<UpdateMeDbContext>();

            var storeMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<ApplicationUserManager>(storeMock.Object);

            var course = new Course()
            {
                Id = 90,
                Name = "JavaScript 6 hour course",
                Description = "This is a comprehensive course for JavaScript.",
                PassScore = 65,
                DateCreated = DateTime.Parse("9 Aug 2017")
            };
            List<Course> courses = new List<Course>() { course };
            var coursesSetMock = new Mock<DbSet<Course>>().SetupData(courses);
            dbContextMock.SetupGet(c => c.Courses).Returns(coursesSetMock.Object);


            var user = new ApplicationUser() { UserName = "firstuser" };
            List<ApplicationUser> users = new List<ApplicationUser>() { user };
            var usersSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(users);
            dbContextMock.SetupGet(m => m.Users).Returns(usersSetMock.Object);

            var assignment = new Assignment()
            {
                Id = 1,
                ApplicationUserId = user.Id,
                DueDate = DateTime.Now.AddDays(5),
                AssignmentStatus = AssignmentStatus.Pending,
                IsMandatory = true,
                CourseId = course.Id
            };
            List<Assignment> assignments = new List<Assignment>() { assignment };
            var assignmentsSetMock = new Mock<DbSet<Assignment>>().SetupData(assignments);
            dbContextMock.SetupGet(a => a.Assignments).Returns(assignmentsSetMock.Object);

            var assignmentInDb = dbContextMock.Object.Assignments.FirstOrDefault();


            UpdateMe.Services.AssignmentService service = new UpdateMe.Services.AssignmentService(dbContextMock.Object);

            //Act
            var executedService = service.ListAllAssignmentsFromUser(user.Id);

            //Assert
            dbContextMock.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
