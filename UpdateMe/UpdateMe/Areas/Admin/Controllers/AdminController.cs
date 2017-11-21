using System;
using System.Collections.Generic;
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
        private readonly IReader reader;


        public AdminController(ApplicationUserManager userManager, UpdateMeDbContext dbContext, ICourseService courseService, IAssignmentService assignmentService, IReader reader)
        {
            this.userManager = userManager ?? throw new ArgumentNullException("userManager");
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext");
            this.courseService = courseService ?? throw new ArgumentNullException("courseService");
            this.assignmentService = assignmentService ?? throw new ArgumentNullException("assignmentService");
            this.reader = reader ?? throw new ArgumentNullException("reader");
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

        public ActionResult ListAllCourses()
        {
            var courses = dbContext
               .Courses
               .Select(CourseViewModel.Create)
               .ToList();

            return this.View(courses);
        }

        // Edited ============================================================================
        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var course = this.courseService.FindCourse(id);

            var courseViewModel = CourseViewModel.Create.Compile()(course);

            return this.PartialView("_EditCourse", courseViewModel);
        }

        // Edited ============================================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse(CourseViewModel courseViewModel)
        {
            var course = this.courseService.FindCourse(courseViewModel.Id);

            courseService.EditCourse(course, courseViewModel.Name, courseViewModel.Description, courseViewModel.PassScore);

            return this.RedirectToAction("ListAllCourses");
        }

        // Edited ============================================================================
        public ActionResult DeleteCourse(int id)
        {
            var course = this.courseService.FindCourse(id);

            this.courseService.DeleteCourse(course);

            return this.RedirectToAction("ListAllCourses");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadCourse(HttpPostedFileBase file)
        {
            var course = this.reader.ReadFile(file);

            this.courseService.AddCourse(course);

            return RedirectToAction("ListAllCourses");
        }

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
            if (this.ModelState.IsValid)
            {
                List<UserViewModelTwo> checkedUsersFromPostRequest = assignmentFormViewModel
                    .UserViewModelsTwo
                    .Where(u => u.IsChecked == true)
                    .ToList();

                List<CourseViewModel> checkedCoursesFromPostRequest = assignmentFormViewModel
                    .CourseViewModels
                    .Where(c => c.IsChecked == true)
                    .ToList();


                var userIds = checkedUsersFromPostRequest.Select(u => u.Id).ToList();
                var courseIds = checkedCoursesFromPostRequest.Select(u => u.Id).ToList();
                var areMandatory = assignmentFormViewModel.Assignments.Select(a => a.IsMandatory).ToList();
                var dueDates = assignmentFormViewModel.Assignments.Select(a => a.DueDate.Value).ToList();

                for (int i = 0; i < checkedUsersFromPostRequest.Count(); i++)
                {
                    for (int j = 0; j < checkedCoursesFromPostRequest.Count(); j++)
                    {
                        assignmentService.CreateAssignment(
                        dueDates[j],
                        AssignmentStatus.Pending,
                        areMandatory[j],
                        courseIds[j],
                        userIds[i]);
                    }

                }

                //TODO: Redirect to view all user assignments
                return this.RedirectToAction("ListAllCourses");
            }

            return this.View(assignmentFormViewModel);
        }


        public ActionResult ListUserAssignments(string currentUserId)
        {
            //var allAssignments = assignmentService.ListAllAssignmentsFromUser(currentUserId);

            //var assignmentViewModels = allAssignments.Select(a => AssignmentViewModel.Create.Compile()(a)).ToList();

            var result = this.assignmentService.ListAllAssignmentsFromUser(currentUserId); //newly added


            return this.PartialView("_Assignments", result);
        }

        public ActionResult DeleteAssignment(int assignmentId)
        {
            this.assignmentService.DeleteAssignment(assignmentId);

            return this.RedirectToAction("ListUserAssignments");
        }

    }
}