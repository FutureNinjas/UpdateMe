using Bytes2you.Validation;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Web;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using UpdateMe.Services.Contracts;

namespace UpdateMe.Services
{
    public class CourseService : ICourseService
    {
        private readonly UpdateMeDbContext dbContext;

        public CourseService(UpdateMeDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.dbContext = dbContext;
        }

        public void AddCourse(Course course)
        {
            Guard.WhenArgument(course, "course").IsNull().Throw();

            this.dbContext.Courses.Add(course);
            this.dbContext.SaveChanges();
        }

        public Course FindCourse(int courseId)
        {
            var course = dbContext.Courses.FirstOrDefault(c => c.Id == courseId);

            return course;
        }

        public void EditCourse(Course course, string name, string description, int passScore)
        {
            course.Name = name;
            course.Description = description;
            course.PassScore = passScore;

            this.dbContext.SaveChanges();
        }

        public void DeleteCourse(Course course)
        {            
            Guard.WhenArgument(course, "course").IsNull().Throw();

            this.dbContext.Courses.Remove(course);
            this.dbContext.SaveChanges();
        }
        

        public CourseModel ReviewCourse(int courseId, string userId)
        {
            var assignment = this.dbContext
                .Assignments
                .FirstOrDefault(c => c.CourseId == courseId && c.ApplicationUser.Id == userId);

            assignment.AssignmentStatus = AssignmentStatus.Started;

            this.dbContext.SaveChanges();

            return new CourseModel()
            {
                Id = assignment.Course.Id,
                Name = assignment.Course.Name,
                Description = assignment.Course.Description,
                PassScore = assignment.Course.PassScore,
                DateCreated = assignment.Course.DateCreated,
                Slides = assignment.Course.Slides.ToList()
            };
        }
    }
}
