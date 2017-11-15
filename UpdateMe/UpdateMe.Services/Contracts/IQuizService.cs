using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateMe.Services.Contracts
{
    public interface IQuizService
    {
        void CheckAnswer(string answer, int courseId, int questionId, string userId);

        void UpdateState();
    }
}
