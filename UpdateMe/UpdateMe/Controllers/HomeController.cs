using System.Linq;
using System.Web.Mvc;
using UpdateMe.Models;
using UpdateMe.Services.Contracts;

namespace UpdateMe.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ICourseService courseService;

        public HomeController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public ActionResult Index()
        {
            var courses = courseService
                .ListAllCourses()
                .Select(c => CourseViewModel.Create.Compile()(c))
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