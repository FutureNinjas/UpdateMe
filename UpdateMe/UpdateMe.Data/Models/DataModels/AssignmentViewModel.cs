using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UpdateMe.Data.Models.DataModels
{
    public class AssignmentViewModel
    {
        public int Id { get; set; }

        public DateTime AssignmentDate { get; set; }

        public DateTime DueDate { get; set; }

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
                    Id = a.Id,
                    //AssignmentDate = a.AssignmentDate,
                    //DueDate = a.DueDate,
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
