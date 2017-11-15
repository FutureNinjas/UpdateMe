using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UpdateMe.Data.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string  Description { get; set; }
        
        public int PassScore { get; set; }
        
        public DateTime DateCreated { get; set; }

        public List<Slide> Slides { get; set; }

        public List<QuestionModel> Questions { get; set; }
    }

    public class QuestionModel
    {
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public string[] Answers { get; set; }

        public string SelectedAnwser { get; set; }
    }
}
