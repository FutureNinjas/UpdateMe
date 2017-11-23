using System.Collections.Generic;
using UpdateMe.Data.Models;

namespace UpdateMe.Services.Contracts
{
    public interface IQuizService
    {
        IEnumerable<Question> GetCourseQuestions(int courseId);

        bool ResultCheck(int courseId, string userId, int questionsCount, int correctAnswersCount);
    }
}
