using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestStack.FluentMVCTesting;
using UpdateMe.Areas.Admin.Controllers;
using UpdateMe.Areas.Admin.Models;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using UpdateMe.Services;

namespace UpdateMe.UnitTests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class AllUsers_Should
    {
        [TestMethod]
        public void ReturnDefaultViewWithCorrectViewModel()
        {
            //Arrange
            var storeMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<ApplicationUserManager>(storeMock.Object);
            var dbContextMock = new Mock<UpdateMeDbContext>();
            var courseServiceMock = new Mock<CourseService>(dbContextMock.Object);
            var assignmentServiceMock = new Mock<AssignmentService>(dbContextMock.Object);


            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser() { UserName = "firstUser"},
                new ApplicationUser() { UserName = "secondUser"},
            };

            var usersSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(users);

            var resultViewModel = users.AsQueryable().Select(UserViewModel.Create).ToList();

            dbContextMock.SetupGet(m => m.Users).Returns(usersSetMock.Object);

            

            AdminController controller = new AdminController(userManagerMock.Object, dbContextMock.Object, courseServiceMock.Object, assignmentServiceMock.Object);

            //Act & Assert  //стандартен UnitTest за един контролер. 
            //така е заради API-то FluentMVCTesting - извиква нещо и директно assert-ва върху него
            controller
                .WithCallTo(c => c.AllUsers())
                .ShouldRenderDefaultView()
                .WithModel<List<UserViewModel>>(viewModel =>//подсигурява, че моделът ще comply-ва на някакви условия, които бихме искали да напишем
                {
                    for (int i = 0; i < viewModel.Count; i++)
                    {
                        Assert.AreEqual(resultViewModel[i].Username, viewModel[i].Username);
                    }
                });
        }
    }
}
