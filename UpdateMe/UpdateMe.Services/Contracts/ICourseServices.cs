using System.Collections.Generic;
using UpdateMe.Data.Models;

namespace UpdateMe.Services.Contracts
{
    public interface ICourseServices
    {
        void CreateCourse(string name, string description, int passScore, ICollection<Question> questions);

        CourseModel ReviewCourse(int courseId);

        void EditCourse(int courseId);

        void DeleteCourse(int courseId);

        IEnumerable<CourseModel> ListAllCourses();               
    }
}
