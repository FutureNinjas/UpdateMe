using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace UpdateMe.Data.Models.DataModels
{
    public class AssignmentViewModel
    {
        public int Id { get; set; }

        public DateTime? AssignmentDate { get; set; }

        [Required(ErrorMessage = "Date is not selected!")]
        [Date(ErrorMessage = "Invalid date!")]
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

    public class DateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null ? (DateTime)value >= DateTime.Now.AddDays(-1) : false;
        }
    }
}
