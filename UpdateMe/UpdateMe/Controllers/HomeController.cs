using System;
using System.Linq;
using System.Web.Mvc;
using UpdateMe.Data;
using UpdateMe.Data.Models;

namespace UpdateMe.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly UpdateMeDbContext dbContext;

        public HomeController(UpdateMeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var courses = dbContext
               .Courses
               .Select(CourseViewModel.Create)
               .ToList();

            return this.View(courses);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}