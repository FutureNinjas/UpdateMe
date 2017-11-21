using System;
using System.Collections.Generic;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using UpdateMe.Services.Contracts;
using System.Linq;

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
            dbContext.SaveChanges();
        }

        public Assignment FindAssignment(int assignmentId)
        {
            var assignment = this.dbContext
                .Assignments
                .FirstOrDefault(a => a.Id == assignmentId);

            return assignment;
        }

        public void DeleteAssignment(Assignment assignment)
        {
            dbContext.Assignments.Remove(assignment);
            dbContext.SaveChanges();
        }


        public IEnumerable<Assignment> ListUserAssignments(string userId)
        {
            var userAssignments = this.dbContext
                .Assignments
                .Where(a => a.ApplicationUser.Id == userId)
                .ToList();

            return userAssignments;
        }
        
        public IEnumerable<Assignment> ListOverdoneAssignments()
        {
            var assignments = this.dbContext.Assignments.Where(a => a.DueDate < DateTime.Now).ToList();

            return assignments;
        }
    }
}
