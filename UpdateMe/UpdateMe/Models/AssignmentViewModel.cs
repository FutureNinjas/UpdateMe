using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using UpdateMe.Data.Models;

namespace UpdateMe.Models
{
    public class AssignmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is not selected!")]
        [Date(ErrorMessage = "Invalid date!")]
        public DateTime? DueDate { get; set; }

        public DateTime? AssignmentDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public AssignmentStatus AssignmentStatus { get; set; }

        public bool IsMandatory { get; set; }

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int PassScore { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public static Expression<Func<Assignment, AssignmentViewModel>> Create
        {
            get
            {
                return a => new AssignmentViewModel()
                {
                    Id = a.Id,
                    AssignmentDate = a.AssignmentDate,
                    DueDate = a.DueDate,
                    CompletionDate = a.CompletionDate,
                    AssignmentStatus = a.AssignmentStatus,
                    IsMandatory = a.IsMandatory,
                    CourseId = a.CourseId,
                    ApplicationUserId = a.ApplicationUserId,
                    ApplicationUser = a.ApplicationUser,
                    CourseName = a.Course.Name,
                    PassScore = a.Course.PassScore

                };
            }
        }
    }

    public class OverdoneAssignmentsModel
    {
        public string CourseName { get; set; }

        public DateTime? AssignmentDate { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsMandatory { get; set; }

        public AssignmentStatus AssignmentStatus { get; set; }

        public string ApplicationUserName { get; set; }

        public static Expression<Func<Assignment, OverdoneAssignmentsModel>> Create
        {
            get
            {
                return a => new OverdoneAssignmentsModel()
                {
                    CourseName = a.Course.Name,
                    AssignmentDate = a.AssignmentDate,
                    DueDate = a.DueDate,
                    IsMandatory = a.IsMandatory,
                    AssignmentStatus = a.AssignmentStatus,
                    ApplicationUserName = a.ApplicationUser.UserName
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