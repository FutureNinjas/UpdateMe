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
                Slides = assignment.Course.Slides
            };
        }
    }
}
