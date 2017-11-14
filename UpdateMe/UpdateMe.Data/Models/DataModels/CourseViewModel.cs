using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using UpdateMe.Data.Models;

namespace UpdateMe.Data.Models
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
}