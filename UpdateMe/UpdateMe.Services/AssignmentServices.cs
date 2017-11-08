using System;
using System.Collections.Generic;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using UpdateMe.Services.Contracts;
using System.Linq;

namespace UpdateMe.Services
{
    public class AssignmentServices : IAssignmentServices
    {
        private readonly UpdateMeDbContext dbContext;

        public AssignmentServices(UpdateMeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateAssignment(DateTime dueDate, bool isMandatory, int courseId, string applicationUserId)
        {
            Assignment assignment = new Assignment()
            {
                DueDate = dueDate,
                IsMandatory = isMandatory,
                CourseId = courseId,
                ApplicationUserId = applicationUserId
            };

            dbContext.Assignments.Add(assignment);
            dbContext.SaveChanges();
        }

        public void DeleteAssignment(Assignment assignment)
        {
            dbContext.Assignments.Remove(assignment);
            dbContext.SaveChanges();
        }

        public IEnumerable<Assignment> FindAssignment(string courseName, string userName)
        {
            return dbContext
                .Assignments
                .Where(a => a.Course.Name == courseName && a.ApplicationUser.UserName == userName)
                .ToList();
        }

        public IEnumerable<Assignment> ListAllAssignmentsFromUser(string userId)
        {
            return dbContext
                .Assignments
                .Where(a => a.ApplicationUser.Id == userId)
                .ToList();
        }

        public void UpdateAssignment(Assignment assignment)
        {
            throw new NotImplementedException();
        }
    }
}
