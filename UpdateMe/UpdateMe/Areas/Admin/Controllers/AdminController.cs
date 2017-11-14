using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UpdateMe.Areas.Admin.Models;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using UpdateMe.Data.Models.DataModels;
using UpdateMe.Services.Contracts;

namespace UpdateMe.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly UpdateMeDbContext dbContext;
        private readonly ICourseService courseService;
        private readonly IAssignmentService assignmentService;

        public AdminController(ApplicationUserManager userManager, UpdateMeDbContext dbContext, ICourseService courseService, IAssignmentService assignmentService)
        {
            this.userManager = userManager ?? throw new ArgumentNullException("userManager");
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext");
            this.courseService = courseService ?? throw new ArgumentNullException("courseService");
            this.assignmentService = assignmentService ?? throw new ArgumentNullException("assignmentService");
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult AllUsers()
        {
            var usersViewModel = this.dbContext
                .Users
                .Select(UserViewModel.Create).ToList();

            return this.View(usersViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> EditUser(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var userViewModel = UserViewModel.Create.Compile()(user);
            userViewModel.IsAdmin = await this.userManager.IsInRoleAsync(user.Id, "Admin");

            return this.PartialView("_EditUser", userViewModel);
            //best practice - use underscore            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserViewModel userViewModel)
        {
            if (userViewModel.IsAdmin)
            {
                await this.userManager.AddToRoleAsync(userViewModel.Id, "Admin");
            }
            else
            {
                await this.userManager.RemoveFromRoleAsync(userViewModel.Id, "Admin");
            }

            return this.RedirectToAction("AllUsers");
        }

        //TODO: Default Admin Landing Page

        public ActionResult ListAllCourses()
        {
            var courses = dbContext
               .Courses
               .Select(CourseViewModel.Create)
               .ToList();

            return this.View(courses);
        }

        //TODO: Make async if needed
        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var course = this.dbContext.Courses.Find(id);

            var courseViewModel = CourseViewModel.Create.Compile()(course);

            return this.PartialView("_EditCourse", courseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse(CourseViewModel courseViewModel)
        {
            var course = this.dbContext.Courses.Find(courseViewModel.Id);

            courseService.EditCourse(course.Id, courseViewModel);

            return this.RedirectToAction("ListAllCourses");
        }

        public ActionResult DeleteCourse(int id)
        {
            this.courseService.DeleteCourse(id);

            return this.RedirectToAction("ListAllCourses");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            courseService.JsonHandler(file);

            return RedirectToAction("ListAllCourses");
        }

        //public ActionResult ListUsersAndCoursesForAssignment() //used to list users in nav bar/ assign course
        //{
        //    var usersViewModel = this.dbContext
        //       .Users
        //       .Select(UserViewModelTwo.Create)
        //       .ToList();

        //    var coursesViewModel = this.dbContext
        //        .Courses
        //        .Select(CourseViewModel.Create)
        //        .ToList();

        //    var assignmentFormViewModel = AssignmentFormViewModel.CreateAssignmentFormViewModel(coursesViewModel, usersViewModel);

        //    return this.View(assignmentFormViewModel);
        //}

        [HttpGet]
        public ActionResult AssignCourse()
        { 
            var usersViewModel = this.dbContext
               .Users
               .Select(UserViewModelTwo.Create)
               .ToList();

            var coursesViewModel = this.dbContext
                .Courses
                .Select(CourseViewModel.Create)
                .ToList();

            var assignmentFormViewModel = AssignmentFormViewModel.CreateAssignmentFormViewModel(coursesViewModel, usersViewModel);

            return this.View(assignmentFormViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignCourse(AssignmentFormViewModel assignmentFormViewModel)
        {

            //var assignedUsers = assignmentFormViewModel[0]
            //    .UserViewModelsTwo
            //    .Where(u => u.IsChecked == true)
            //    .Select(u => this.dbContext.Users.Where(au => au.Id == u.Id).ToList())
            //    .ToList();


            //var assignedCourses = assignmentFormViewModel
            //    .CourseViewModels
            //    .Where(c => c.IsChecked == true)
            //    .Select(c => this.dbContext.Courses.Where(dbc => dbc.Id == c.Id).ToList())
            //    .ToList();



            //for (int i = 0; i < assignedUsers.Count(); i++)
            //{
            //    for (int j = 0; j < assignedCourses.Count(); j++)
            //    {
            //        assignmentService.CreateAssignment(
            //            SqlDateTime.MinValue.Value,
            //            AssignmentStatus.Completed,
            //            true,
            //            //assignedCourses[i][j].Id, CourseID
            //            1,
            //            assignmentFormViewModel.UserViewModelsTwo[0].Id);
            //            //"2bf76dfa-8061-4779-af64-a09a21c16bc1");//assignedUsers[0][i].Id
            //        //SqlDateTime.MinValue.Value
            //    }

            //}

            List<UserViewModelTwo> CheckedUsersFromPostRequest = assignmentFormViewModel.UserViewModelsTwo.Where(c => c.IsChecked == true).ToList();

            //List<ApplicationUser> allUsersFromDbContext = this.dbContext.Users.ToList();

            var ids = CheckedUsersFromPostRequest.Select(u => u.Id).ToList();


            for (int i = 0; i < CheckedUsersFromPostRequest.Count(); i++)
            {
               assignmentService.CreateAssignment(
               SqlDateTime.MinValue.Value, //assignmentFormViewModel.CourseViewModels[i].InputDueDate
               AssignmentStatus.Pending,
               true,
               1,
               ids[i]);
            }
            
            //assignmentService.CreateAssignment(
            //    SqlDateTime.MinValue.Value, //assignmentFormViewModel.CourseViewModels[i].InputDueDate
            //    AssignmentStatus.Completed,
            //    true,
            //    1,
            //    usersThatWillGetAnAssignment[1].Id);

            return this.RedirectToAction("ListAllCourses");
        }

    }
}