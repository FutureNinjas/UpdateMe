using System;
using System.Collections.Generic;
using System.Linq;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using UpdateMe.Services.Contracts;

namespace UpdateMe.Services
{
    public class QuizService : IQuizService
    {
        private readonly UpdateMeDbContext dbContext;

        public QuizService(UpdateMeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Question> GetCourseQuestions(int courseId)
        {
            var questions = dbContext.Questions.Where(q => q.CourseId == courseId).ToList();

            return questions;
        }

        public void CheckAnswer(string answer, int courseId, int questionId, string userId)
        {
            var courseQuestions = this.dbContext.Questions.Where(c => c.CourseId == courseId).ToList();

            var question = courseQuestions.FirstOrDefault(q => q.Id == questionId);

            var correctAnswer = question.CorrectAnswer;

            var assignment = this.dbContext.Assignments.Where(a => a.CourseId == courseId && a.ApplicationUserId == userId).FirstOrDefault();

            var currentQuizState = this.dbContext.QuizesCurrentState.Find(assignment);

            if (answer == correctAnswer)
            {
                currentQuizState.CurrentUserResult += PointsPerAnswer(courseQuestions.Count());
                this.dbContext.SaveChanges();
            }
            if (questionId == courseQuestions.Last().Id)
            {
                ResultCheck(currentQuizState.CurrentUserResult, courseId, assignment);
            }
        }

        public void ResultCheck(int result, int courseId, Assignment assignment)
        {
            var coursePassScore = this.dbContext.Courses.Find(courseId).PassScore;

            if (result >= coursePassScore)
            {
                assignment.AssignmentStatus = AssignmentStatus.Completed;
                assignment.CompletionDate = DateTime.Now;
            }
        }

        public void UpdateState()
        {
            throw new NotImplementedException();
        }

        public int PointsPerAnswer(int numberOfQuestions)
        {
            return 100 / numberOfQuestions;
        }
    }
}
