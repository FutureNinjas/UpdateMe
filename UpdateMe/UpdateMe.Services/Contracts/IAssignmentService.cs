using System;
using System.Collections.Generic;
using System.Linq;
using UpdateMe.Data.Models;

namespace UpdateMe.Services.Contracts
{
    public interface IAssignmentService
    {
        void CreateAssignment(DateTime dueDate, AssignmentStatus assignmentStatus, bool isMandatory, int courseId, string applicationUserId);

        Assignment FindAssignment(int assignmentId);

        IEnumerable<Assignment> ListUserAssignments(string userId);

        //IEnumerable<AssignmentViewModel> ListUserAssignments(string userId);

        void DeleteAssignment(Assignment assignment);

        IEnumerable<Assignment> ListOverdoneAssignments();
    }
}
