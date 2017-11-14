using System;
using System.Linq.Expressions;

namespace UpdateMe.Data.Models.DataModels
{
    public class AssignmentViewModel
    {
        public int Id { get; set; }

        public DateTime? AssignmentDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime CompletionDate { get; set; }

        public AssignmentStatus AssignmentStatus { get; set; }

        public bool IsMandatory { get; set; }

        public int CourseId { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }


        public static Expression<Func<Assignment, AssignmentViewModel>> Create
        {
            get
            {
                return a => new AssignmentViewModel()
                {
                    //TODO: Check it
                    Id = a.Id,
                    AssignmentDate = a.AssignmentDate,
                    DueDate = a.DueDate,
                    //CompletionDate = a.CompletionDate,
                    AssignmentStatus = a.AssignmentStatus,
                    ApplicationUserId = a.ApplicationUserId,
                    ApplicationUser = a.ApplicationUser,
                    CourseId = a.CourseId
                };
            }
        }
    }
}
