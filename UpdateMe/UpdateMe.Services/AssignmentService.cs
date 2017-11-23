using System;
using System.Collections.Generic;
using System.Linq;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using UpdateMe.Services.Contracts;

namespace UpdateMe.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly UpdateMeDbContext dbContext;

        public AssignmentService(UpdateMeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateAssignment(DateTime dueDate, bool isMandatory, int courseId, string applicationUserId)
        {
            Assignment assignment = new Assignment()
            {
                DueDate = dueDate,
                AssignmentStatus = AssignmentStatus.Pending,
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

        public Assignment FindAssignment(int courseId, string userId)
        {
            var assignment = this.dbContext
                .Assignments
                .FirstOrDefault(a => a.CourseId == courseId && a.ApplicationUserId == userId);

            return assignment;
        }

        public void DeleteAssignment(Assignment assignment)
        {
            dbContext.Assignments.Remove(assignment);
            dbContext.SaveChanges();
        }

        public void StartAssignedCourse(int courseId, string userId)
        {
            var assignment = this.dbContext
                .Assignments
                .FirstOrDefault(a => a.CourseId == courseId && a.ApplicationUserId == userId);
            if (assignment.AssignmentStatus == AssignmentStatus.Pending)
            {
                assignment.AssignmentStatus = AssignmentStatus.Started;
                dbContext.SaveChanges();
            }
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
