using System;
using System.Collections.Generic;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using UpdateMe.Services.Contracts;
using System.Linq;
using UpdateMe.Data.Models.DataModels;

namespace UpdateMe.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly UpdateMeDbContext dbContext;

        public AssignmentService(UpdateMeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateAssignment(DateTime dueDate, AssignmentStatus assignmentStatus, bool isMandatory, int courseId, string applicationUserId)
        {
            Assignment assignment = new Assignment()
            {
                DueDate = dueDate,
                AssignmentStatus = assignmentStatus,
                IsMandatory = isMandatory,
                CourseId = courseId,
                ApplicationUserId = applicationUserId,
            };

            dbContext.Assignments.Add(assignment);

            //dbContext.QuizesCurrentState.Add(new CurrentQuizState() { AssignmentId = assignment.Id });

            dbContext.SaveChanges();


        }

        public void DeleteAssignment(int assignmentId)
        {
            var assignment = this.dbContext.Assignments.Where(a => a.Id == assignmentId).FirstOrDefault();
                
            dbContext.Assignments.Remove(assignment);
            dbContext.SaveChanges();
        }

        public IEnumerable<AssignmentViewModel> ListAllAssignmentsFromUser(string userId)
        {
            var allAssignments = this.dbContext
                .Assignments
                .Where(a => a.ApplicationUser.Id == userId)
                .ToList();

            var assignmentViewModels = allAssignments.Select(a => AssignmentViewModel.Create.Compile()(a)).ToList();

            return assignmentViewModels;
        }
    }
}
