using System;

namespace UpdateMe.Data.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        public DateTime AssignmentDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CompletionDate { get; set; }

        public AssignmentStatus AssignmentStatus { get; set; }

        public bool IsMandatory { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
