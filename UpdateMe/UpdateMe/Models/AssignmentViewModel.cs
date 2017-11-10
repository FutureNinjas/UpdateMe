using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using UpdateMe.Data.Models;

namespace UpdateMe.Models
{
    public class AssignmentViewModel
    {
        public int Id { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CompletionDate { get; set; }

        public AssignmentStatus AssignmentStatus { get; set; }

        public bool IsMandatory { get; set; }

        public int CourseId { get; set; }

        public string ApplicationUserId { get; set; }

        public static Expression<Func<Assignment, AssignmentViewModel>> Create
        {
            get
            {
                return a => new AssignmentViewModel()
                {
                    Id = a.Id,
                    DueDate = a.DueDate,
                    CompletionDate = a.CompletionDate,
                    AssignmentStatus = a.AssignmentStatus,
                    IsMandatory = a.IsMandatory,
                    CourseId = a.CourseId,
                    ApplicationUserId = a.ApplicationUserId

                };
            }
        }
    }
}