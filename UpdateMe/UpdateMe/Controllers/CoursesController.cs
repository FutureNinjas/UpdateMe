using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using UpdateMe.Models;
using UpdateMe.Services.Contracts;

namespace UpdateMe.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly UpdateMeDbContext dbContext;
        private readonly IAssignmentService assignmentService;
        private readonly ICourseService courseService;

        public CoursesController(
            ApplicationUserManager userManager,
            UpdateMeDbContext dbContext,
            IAssignmentService assignmentService,
            ICourseService courseService)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.assignmentService = assignmentService;
            this.courseService = courseService;
        }

        public ActionResult ListUserCourses()
        {
            var currentUserId = this.User.Identity.GetUserId();

            var allAssignments = assignmentService.ListUserAssignments(currentUserId).Select(a => AssignmentViewModel.Create.Compile()(a)).ToList();

            //var assignmentViewModels = allAssignments.Select(a => AssignmentViewModel.Create.Compile()(a)).ToList();

            return View(allAssignments);
        }

        public ActionResult ReviewCourse(int id)
        {
            var courseModel = courseService.ReviewCourse(id, this.User.Identity.GetUserId());

            return this.View(courseModel);
        }
        [HttpGet]
        public ActionResult TakeQuiz(int id)
        {
            //var assignment = this.dbContext.Assignments.Where(c => c.CourseId == id).ToList();

            var courseModel = this.dbContext.Courses.First(c => c.Id == id);

            var questions = this.dbContext.Questions.Where(q => q.CourseId == id).ToList();


            var model = new CourseModel();
            model.Id = courseModel.Id;
            model.Name = courseModel.Name;
            model.PassScore = courseModel.PassScore;
            model.Description = courseModel.Description;
            model.Slides = courseModel.Slides.ToList();
            model.Questions = questions
                .Select(q => new QuestionModel()
                {
                    Id = q.Id,
                    Answers = q.AnswersExternal,
                    QuestionText = q.QuestionText
                })
                .ToList();


            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TakeQuiz(CourseModel courseModel)
        {
            var QuizResults = courseModel;

            return this.View(QuizResults);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult OverdoneAssignments()
        {
            var overdoneAssignements = assignmentService.ListOverdoneAssignments();

            var viewModels = overdoneAssignements.Select(a => OverdoneAssignmentsModel.Create.Compile()(a)).ToList();

            return this.View(viewModels);
        }
    }
}