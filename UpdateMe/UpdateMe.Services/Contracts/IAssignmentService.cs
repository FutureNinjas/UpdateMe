using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateMe.Data.Models;

namespace UpdateMe.Services.Contracts
{
    public interface IAssignmentService
    {
        void CreateAssignment(DateTime dueDate, bool isMandatory, int courseId, string applicationUserId);

        void UpdateAssignment(Assignment assignment);

        IEnumerable<Assignment> FindAssignment(string courseName, string userName);

        IEnumerable<Assignment> ListAllAssignmentsFromUser(string userId);

        void DeleteAssignment(Assignment assignment);
    }
}
