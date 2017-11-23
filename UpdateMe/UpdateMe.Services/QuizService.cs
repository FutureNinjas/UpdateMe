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

        public bool ResultCheck(int courseId, string userId, int questionsCount, int correctAnswersCount)
        {
            var score = (int)(correctAnswersCount / (double)questionsCount * 100);
            var passScore = this.dbContext.Courses.Find(courseId).PassScore;
            var result = score >= passScore;
            if (result)
            {
                var assignment = this.dbContext.Assignments
                .FirstOrDefault(a => a.CourseId == courseId && a.ApplicationUserId == userId);
                assignment.AssignmentStatus = AssignmentStatus.Completed;
                assignment.CompletionDate = DateTime.Now;
                dbContext.SaveChanges();
            }

            return result;
        }
    }
}
