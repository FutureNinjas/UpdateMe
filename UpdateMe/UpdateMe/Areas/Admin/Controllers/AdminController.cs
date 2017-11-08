using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UpdateMe.Areas.Admin.Models;
using UpdateMe.Data;

namespace UpdateMe.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly UpdateMeDbContext dbContext;

        public AdminController(ApplicationUserManager userManager, UpdateMeDbContext dbContext)
        {
            this.userManager = userManager ?? throw new ArgumentNullException("userManager");
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext");
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
            .Select(c => new CourseViewModel
            {
                Name = c.Name,
                Description = c.Description
            })
            .ToList();

            return this.View(courses);
        }
    }
}