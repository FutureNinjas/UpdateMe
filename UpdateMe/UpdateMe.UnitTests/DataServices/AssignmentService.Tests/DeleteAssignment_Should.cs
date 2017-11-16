using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestStack.FluentMVCTesting;
using UpdateMe.Areas.Admin.Models;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using UpdateMe.Services;

namespace UpdateMe.UnitTests.DataServices.AssignmentService.Tests
{
    [TestClass]
    public class DeleteAssignment_Should
    {
        [TestMethod]
        public void DeleteAssignmentFromContext_WhenParametersAreValid()
        {
            //Arrange
            var dbContextMock = new Mock<UpdateMeDbContext>();
            var storeMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<ApplicationUserManager>(storeMock.Object);

            int id = 22;
            string name = "ASP.NET MVC Course project";
            string description = "This is a comprehensive course for the ASP.NET project.";
            int passScore = 80;
            DateTime dateCreated = DateTime.Parse("2 Feb 2017");

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

            var user = new ApplicationUser() { UserName = "firstUser" };


            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                user,
            };

            var coursesSetMock = new Mock<DbSet<Course>>().SetupData(courses);


            //DateTime dueDate, AssignmentStatus assignmentStatus, bool isMandatory, int courseId, string applicationUserId
            var assignment = new Assignment()
            {
                Id = 1,
                ApplicationUserId = user.Id,
                DueDate = DateTime.Now.AddDays(5),
                AssignmentStatus = AssignmentStatus.Pending,
                IsMandatory = true,
                CourseId = course.Id
            };

            List<Assignment> assignments = new List<Assignment>()
            {
                assignment
            };

            var assignmentsSetMock = new Mock<DbSet<Assignment>>().SetupData(assignments);

            dbContextMock.SetupGet(m => m.Assignments).Returns(assignmentsSetMock.Object);

            UpdateMe.Services.AssignmentService service = new UpdateMe.Services.AssignmentService(dbContextMock.Object);

            //Act
            service.DeleteAssignment(assignment.Id);

            //Assert
            Assert.IsTrue(dbContextMock.Object.Assignments.Count() == 0);

            dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
