using System;
using System.Collections.Generic;
using System.Linq;
using UpdateMe.Data.Models;
using UpdateMe.Data.Models.DataModels;

namespace UpdateMe.Services.Contracts
{
    public interface IAssignmentService
    {
        void CreateAssignment(DateTime dueDate, AssignmentStatus assignmentStatus, bool isMandatory, int courseId, string applicationUserId);

        IEnumerable<AssignmentViewModel> ListAllAssignmentsFromUser(string userId);

        void DeleteAssignment(int assignmentId);

        IEnumerable<Assignment> ListOverdoneAssignments();
    }
}
