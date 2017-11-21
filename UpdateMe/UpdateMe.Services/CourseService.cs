using Bytes2you.Validation;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Course> ListAllCourses()
        {
            var courses = dbContext
               .Courses
               .ToList();

            return courses;
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
    }
}
