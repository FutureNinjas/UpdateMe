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

        public ActionResult ListUsersAndCoursesForAssignment()
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


            //da vzemem kursovete
            //da dobavim 
            return this.View(assignmentFormViewModel);
        }   //used to list users in nav bar/ assign course

        //TODO: implement get request first
        [HttpGet]
        public ActionResult AssignCourse(ICollection<UserViewModel> userViewModels)//predi trieneto
        {
            var ids = userViewModels.Select(x => x.Id).ToList();
            var users = dbContext.Users.Where(user => ids.Contains(user.Id)).ToList();  

            ICollection<ApplicationUser> applicationUsers = new List<ApplicationUser>();

            foreach(var user in users)
            {
                applicationUsers.Add(user);
            }

            ICollection<AssignmentViewModel> assignmentViewModels = new List<AssignmentViewModel>();

            //create an assignment
            foreach (var user in applicationUsers)
            {
                Assignment assignment = new Assignment();
                this.dbContext.Assignments.Add(assignment);

                assignment.ApplicationUser = user;  //model binding
                var assignmentViewModel = AssignmentViewModel.Create.Compile()(assignment);
                assignmentViewModels.Add(assignmentViewModel);
            }
            
            this.dbContext.SaveChanges();

            return this.PartialView("_AssignCourse", assignmentViewModels);
        }

        [HttpPost]
        public ActionResult AssignCourse(List<AssignmentFormViewModel> assignmentFormViewModel)
        {
            var assignedUsers = assignmentFormViewModel[0]
                .UserViewModelsTwo
                .Where(u => u.IsChecked == true)
                .Select(u => this.dbContext.Users.Where(au => au.Id == u.Id).ToList())
                .ToList();

            
            var assignedCourses = assignmentFormViewModel[0]
                .CourseViewModels
                .Where(c => c.IsChecked == true)
                .Select(c => this.dbContext.Courses.Where(dbc => dbc.Id == c.Id).ToList())
                .ToList();

            for (int i = 0; i < assignedUsers.Count(); i++)
            {
                for (int j = 0; j < assignedCourses.Count(); j++)
                {
                    assignmentService.CreateAssignment(SqlDateTime.MinValue.Value, true, 21, assignedUsers[i]);
                }
                
            }


            return this.RedirectToAction("ListAllCourses");
        }

    }
}