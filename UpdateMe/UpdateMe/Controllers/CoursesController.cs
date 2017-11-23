using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UpdateMe.Models;
using UpdateMe.Services.Contracts;

namespace UpdateMe.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly IAssignmentService assignmentService;
        private readonly ICourseService courseService;
        private readonly IQuizService quizService;

        public CoursesController(
            ApplicationUserManager userManager,
            IAssignmentService assignmentService,
            ICourseService courseService,
            IQuizService quizService)
        {
            this.userManager = userManager;
            this.assignmentService = assignmentService;
            this.courseService = courseService;
            this.quizService = quizService;
        }

        public ActionResult ListUserCourses()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var allAssignments = assignmentService.ListUserAssignments(currentUserId).Select(a => AssignmentViewModel.Create.Compile()(a)).ToList();

            return View(allAssignments);
        }

        public ActionResult ReviewCourse(int id)
        {
            assignmentService.StartAssignedCourse(id, this.User.Identity.GetUserId());

            var course = courseService.FindCourse(id);
            var reviewedCourse = CourseReviewViewModel.Create.Compile()(course);

            return this.View(reviewedCourse);
        }

        [HttpGet]
        public ActionResult TakeQuiz(int id)
        {
            var courseModel = this.courseService.FindCourse(id);
            var questions = this.quizService
                .GetCourseQuestions(id)
                .Select(q => QestionViewModel.Create.Compile()(q))
                .ToList();

            return this.View(questions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TakeQuiz(List<QestionViewModel> questionsWithAnswers)
        {

            var passScore = this.courseService
                .FindCourse(questionsWithAnswers.FirstOrDefault().CourseId)
                .PassScore;
            var questionsCount = questionsWithAnswers.Count();
            var correctAnswersCount = questionsWithAnswers
                .Where(q => q.SelectedAnwser.Equals(q.CorrectAnswer))
                .Count();
            var score = (int)(correctAnswersCount / (double)questionsCount * 100);
            var result = score > passScore ? "You have passed!" : "Try again.";

            return this.Content($"You have answered {correctAnswersCount}/{questionsCount}. Your Score is {score}. {result}");
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