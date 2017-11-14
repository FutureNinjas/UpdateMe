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
            //foreach(var user in applicationUsers)
            //{
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
            //}
            
            
        }

        public void DeleteAssignment(Assignment assignment)
        {
            dbContext.Assignments.Remove(assignment);
            dbContext.SaveChanges();
        }

        //public IEnumerable<Assignment> FindAssignment(string courseName, string userName)
        //{
        //    return dbContext
        //        .Assignments
        //        .Where(a => a.Course.Name == courseName && a.ApplicationUser.UserName == userName)
        //        .ToList();
        //}

        public IEnumerable<Assignment> ListAllAssignmentsFromUser(string userId)
        {
            return dbContext
                .Assignments
                .Where(a => a.ApplicationUser.Id == userId)
                .ToList();
        }

        public AssignmentViewModel UpdateAssignment(Assignment assignment)
        {
            var updatedAssignment = this.dbContext.Assignments.FirstOrDefault(a => a.Id == assignment.Id);

            return new AssignmentViewModel()
            {
                Id = updatedAssignment.Id,
                CourseId = updatedAssignment.CourseId,
                ApplicationUser = updatedAssignment.ApplicationUser,
                ApplicationUserId = updatedAssignment.ApplicationUserId,
                IsMandatory = updatedAssignment.IsMandatory,
                AssignmentStatus = updatedAssignment.AssignmentStatus,
                //AssignmentDate = updatedAssignment.AssignmentDate,
                //DueDate = updatedAssignment.DueDate,
                //CompletionDate = updatedAssignment.CompletionDate                
            };
        }
    }
}
