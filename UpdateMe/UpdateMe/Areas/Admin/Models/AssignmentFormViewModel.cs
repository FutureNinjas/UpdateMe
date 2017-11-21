using System.Collections.Generic;
using UpdateMe.Models;

namespace UpdateMe.Areas.Admin.Models
{
    public class AssignmentFormViewModel
    {
        public List<CourseViewModel> CourseViewModels { get; set; }

        public List<UserViewModel> UserViewModels { get; set; }

        //public List<bool> IsMandatory { get; set; }

        public List<AssignmentViewModel> Assignments { get; set; }

        //public List<DateTime> DueDate { get; set; }

        public static AssignmentFormViewModel CreateAssignmentFormViewModel(List<CourseViewModel> listCourses, List<UserViewModel> listUsers)
        {
            return new AssignmentFormViewModel()
            {
                CourseViewModels = listCourses,
                UserViewModels = listUsers
            };
        }

    }
}
