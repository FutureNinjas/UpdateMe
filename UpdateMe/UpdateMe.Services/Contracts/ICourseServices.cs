using System;
using System.Collections.Generic;
using System.Web;
using UpdateMe.Data.Models;

namespace UpdateMe.Services.Contracts
{
    public interface ICourseServices
    {
        void CreateCourse(string name, string description, int passScore, DateTime DateCreated);

        CourseModel ReviewCourse(int courseId);

        void EditCourse(int courseId);

        void DeleteCourse(int courseId);

        IEnumerable<CourseModel> ListAllCourses();

        void JsonHandler(HttpPostedFileBase file);
    }
}
