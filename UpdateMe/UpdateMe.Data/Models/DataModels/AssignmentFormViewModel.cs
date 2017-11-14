using System;
using System.Collections.Generic;

namespace UpdateMe.Data.Models.DataModels
{
    public class AssignmentFormViewModel
    {
        public List<CourseViewModel> CourseViewModels { get; set; }

        public List<UserViewModelTwo> UserViewModelsTwo { get; set; }

        public List<bool> IsMandatory { get; set; }

        public List<DateTime> DueDate { get; set; }

        public static AssignmentFormViewModel CreateAssignmentFormViewModel(List<CourseViewModel> listCourses, List<UserViewModelTwo> listUsers)
        {
            return new AssignmentFormViewModel()
            {
                CourseViewModels = listCourses,
                UserViewModelsTwo = listUsers
            };
        }

    }
}
