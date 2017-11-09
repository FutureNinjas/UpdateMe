using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UpdateMe.Areas.Admin.Models;
using UpdateMe.Data;
using UpdateMe.Services.Contracts;

namespace UpdateMe.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly UpdateMeDbContext dbContext;
        private readonly ICourseServices courseService;

        public AdminController(ApplicationUserManager userManager, UpdateMeDbContext dbContext, ICourseServices courseService)
        {
            this.userManager = userManager ?? throw new ArgumentNullException("userManager");
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext");
            this.courseService = courseService ?? throw new ArgumentNullException("courseService");
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

        [HttpGet]//може и да не се пише щото се подразбира
        public async Task<ActionResult> EditUser(string username)
        {
            var user = await this.userManager.FindByNameAsync(username); //namiraneto po user name e burza operaciq, щото има индексация, търси се в едно индексно дърво
            //на ниво entity framework има атрибут индекс
            //rebuilding an index tree is a slow operation
            //добавянето на произволен нов запис също не е бърза операция
            //те са за сметка на това да бъде бързо търсенето
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
            var course = dbContext.Courses.Find(courseViewModel.Id);

            course.Name = courseViewModel.Name;
            course.Description = courseViewModel.Description;
            course.PassScore = courseViewModel.PassScore;

            this.dbContext.SaveChanges();

            return this.RedirectToAction("ListAllCourses");
        }

        public ActionResult DeleteCourse(int id)
        {
            this.courseService.DeleteCourse(id);

            return this.RedirectToAction("ListAllCourses");
        }
    }
}