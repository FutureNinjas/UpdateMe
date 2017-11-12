using System;
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

        public ActionResult ListUsers()
        {
            var usersViewModel = this.dbContext
               .Users
               .Select(UserViewModel.Create)
               .ToList();

            return this.View(usersViewModel);
        }   //used to list users in nav bar/ assign course

        [HttpGet]
        public ActionResult AssignCourse(string userId)
        {
            //select a user for an assignment
            var user = this.userManager.Users.FirstOrDefault(u => u.Id == userId);  // pass user object directly
            //create an assignment
            Assignment assignment = new Assignment();
            assignment.ApplicationUser = user;  //model binding

            //assignment.ApplicationUserId = userId; //delete this line

            var assignmentViewModel = AssignmentViewModel.Create.Compile()(assignment);

            this.dbContext.Assignments.Add(assignment);

            //this.dbContext.SaveChanges();

            return this.PartialView("_AssignCourse", assignmentViewModel);
        }

        [HttpPost]
        public ActionResult AssignCourse(AssignmentViewModel assignmentViewModel)
        {
            var user = this.dbContext.Users.FirstOrDefault(a => a.Id == assignmentViewModel.ApplicationUserId);

            assignmentService.CreateAssignment(1, SqlDateTime.MinValue.Value, true, 21, assignmentViewModel.ApplicationUserId);

            return this.RedirectToAction("ListAllCourses");
        }

    }
}