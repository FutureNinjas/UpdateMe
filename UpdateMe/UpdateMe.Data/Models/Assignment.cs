﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UpdateMe.Data.Models
{
    public class Assignment
    {
        public Assignment()
        {
            this.AssignmentDate = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime? AssignmentDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public AssignmentStatus AssignmentStatus { get; set; }

        public bool IsMandatory { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual CurrentQuizState CurrentQuizState { get; set; }
    }
}
