using System.Web;
using UpdateMe.Data.Models;

namespace UpdateMe.Services.Contracts
{
    public interface ICourseService
    {
        void AddCourse(Course course);

        Course FindCourse(int courseId);

        void EditCourse(Course course, string name, string description, int passScore);

        void DeleteCourse(Course course);

        CourseModel ReviewCourse(int courseId, string userId);
    }
}
