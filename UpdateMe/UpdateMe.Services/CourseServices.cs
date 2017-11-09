using System;
using System.Collections.Generic;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using UpdateMe.Services.Contracts;
using System.Linq;
using Bytes2you.Validation;

namespace UpdateMe.Services
{
    public class CourseServices : ICourseServices
    {
        private readonly UpdateMeDbContext dbContext;

        public CourseServices(UpdateMeDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.dbContext = dbContext;
        }

        public void CreateCourse(string name, string description, int passScore, ICollection<Question> questions)
        {
            Course course = new Course()
            {
                Name = name,
                Description = description,
                PassScore = passScore,
                Questions = questions
            };

            this.dbContext.Courses.Add(course);

            this.dbContext.SaveChanges();
        }

        public void DeleteCourse(int courseId)
        {
            var course = dbContext.Courses.FirstOrDefault(c => c.Id == courseId);

            Guard.WhenArgument(course, "course").IsNull().Throw();

            this.dbContext.Courses.Remove(course);
            this.dbContext.SaveChanges();
        }
        //TODO: EditCourse
        public void EditCourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CourseModel> ListAllCourses()
        {
            return this.dbContext
               .Courses
               .Select(c =>
               new CourseModel()
               {
                   Name = c.Name,
                   Description = c.Description,
                   PassScore = c.PassScore,
                   DateCreated = c.DateCreated
               })
               .ToList();
        }

        public CourseModel ReviewCourse(int courseId)
        {
            var course = this.dbContext
                .Courses
                .Where(c => c.Id == courseId)
                .FirstOrDefault(c => c.Id == courseId);

            return new CourseModel()
            {
                Name = course.Name,
                Description = course.Description,
                PassScore = course.PassScore,
                DateCreated = course.DateCreated
            };
        }
    }
}
