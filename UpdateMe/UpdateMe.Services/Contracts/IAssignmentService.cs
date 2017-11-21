using System;
using System.Collections.Generic;
using UpdateMe.Data.Models;

namespace UpdateMe.Services.Contracts
{
    public interface IAssignmentService
    {
        void CreateAssignment(DateTime dueDate, bool isMandatory, int courseId, string applicationUserId);

        Assignment FindAssignment(int assignmentId);

        Assignment FindAssignment(int courseId, string userId);

        IEnumerable<Assignment> ListUserAssignments(string userId);

        void StartAssignedCourse(int courseId, string userId);

        void DeleteAssignment(Assignment assignment);

        IEnumerable<Assignment> ListOverdoneAssignments();
    }
}
