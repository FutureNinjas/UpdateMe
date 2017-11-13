using System;
using System.Collections.Generic;
using UpdateMe.Data.Models;
using UpdateMe.Data.Models.DataModels;

namespace UpdateMe.Services.Contracts
{
    public interface IAssignmentService
    {
        void CreateAssignment(DateTime dueDate, bool isMandatory, int courseId, ICollection<ApplicationUser> applicationUsers);

        AssignmentViewModel UpdateAssignment(Assignment assignment);

        IEnumerable<Assignment> FindAssignment(string courseName, string userName);

        IEnumerable<Assignment> ListAllAssignmentsFromUser(string userId);

        void DeleteAssignment(Assignment assignment);
    }
}
