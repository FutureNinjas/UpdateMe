using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UpdateMe.Data.Models;

namespace UpdateMe.Models
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PassScore { get; set; }

        public bool IsChecked { get; set; }

        public DateTime InputDueDate { get; set; }

        public static Expression<Func<Course, CourseViewModel>> Create
        {
            get
            {
                return c => new CourseViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    PassScore = c.PassScore,
                };
            }
        }
    }

    public class CourseReviewViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PassScore { get; set; }

        public DateTime DateCreated { get; set; }

        public List<Slide> Slides { get; set; }

        public List<QuestionModel> Questions { get; set; }

        public static Expression<Func<Course, CourseReviewViewModel>> Create
        {
            get
            {
                return c => new CourseReviewViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    PassScore = c.PassScore,
                    DateCreated = c.DateCreated,
                    Slides = c.Slides.ToList(),
                    Questions = c.Questions
                    .ToList()
                    .Select(q => new QuestionModel()
                    {
                        Id = q.Id,
                        QuestionText = q.QuestionText,
                        Answers = q.AnswersExternal
                    }).ToList()
                };
            }
        }
    }

    public class QuestionModel
    {
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public string[] Answers { get; set; }

        public string SelectedAnwser { get; set; }
    }
}