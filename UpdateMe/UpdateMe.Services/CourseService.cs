using Bytes2you.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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


        public void DeleteCourse(int courseId)
        {
            var course = dbContext.Courses.FirstOrDefault(c => c.Id == courseId);

            Guard.WhenArgument(course, "course").IsNull().Throw();

            this.dbContext.Courses.Remove(course);
            this.dbContext.SaveChanges();
        }
     
        public void EditCourse(int courseId, CourseViewModel courseViewModel)
        {
            var course = dbContext.Courses.FirstOrDefault(c => c.Id == courseId);

            course.Name = courseViewModel.Name;
            course.Description = courseViewModel.Description;
            course.PassScore = courseViewModel.PassScore;

            this.dbContext.SaveChanges();
        }

        public void JsonHandler(HttpPostedFileBase file)
        {
            try
            {
                using (StreamReader reader = new StreamReader(file.InputStream))//research
                {
                    string readFile = reader.ReadToEnd();

                    Course course = JsonConvert.DeserializeObject<Course>(readFile);

                    course.DateCreated = DateTime.Now;

                    this.dbContext.Courses.Add(course);

                    this.dbContext.SaveChanges();
                }
            }
            catch (FileNotFoundException)
            {

            }
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
            var assignment = this.dbContext
                .Assignments
                .Where(c => c.Id == courseId)
                .FirstOrDefault(c => c.Id == courseId);

            return new CourseModel()
            {
                Name = assignment.Course.Name,
                Description = assignment.Course.Description,
                PassScore = assignment.Course.PassScore,
                DateCreated = assignment.Course.DateCreated,
                Slides = assignment.Course.Slides
            };
        }
    }
}
