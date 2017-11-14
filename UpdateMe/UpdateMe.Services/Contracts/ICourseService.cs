using System;
using System.Collections.Generic;
using System.Web;
using UpdateMe.Data.Models;

namespace UpdateMe.Services.Contracts
{
    public interface ICourseService
    {

        CourseModel ReviewCourse(int courseId, string userId);

        void EditCourse(int id, CourseViewModel courseViewModel);

        void DeleteCourse(int courseId);
        
        void JsonHandler(HttpPostedFileBase file);
    }
}
