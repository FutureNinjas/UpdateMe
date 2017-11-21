﻿using UpdateMe.Data.Models;

namespace UpdateMe.Services.Contracts
{
    public interface IQuizService
    {
        void CheckAnswer(string answer, int courseId, int questionId, string userId);

        void ResultCheck(int result, int courseId, Assignment assignment);
        
        void UpdateState();
    }
}
