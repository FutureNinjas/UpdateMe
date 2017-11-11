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

        public void CreateCourse(string name, string description, int passScore, DateTime DateCreated)
        {
            Course course = new Course()
            {
                Name = name,
                Description = description,
                PassScore = passScore,
                DateCreated = DateCreated
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
        public void EditCourse(int id, CourseViewModel courseViewModel)
        {
            var course = dbContext.Courses.Find(id);

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
            var course = this.dbContext
                .Assignments
                .Where(c => c.Id == courseId)
                .FirstOrDefault(c => c.Id == courseId);

            return new CourseModel()
            {
                Name = course.Course.Name,
                Description = course.Course.Description,
                PassScore = course.Course.PassScore,
                DateCreated = course.Course.DateCreated
            };
        }
    }
}
