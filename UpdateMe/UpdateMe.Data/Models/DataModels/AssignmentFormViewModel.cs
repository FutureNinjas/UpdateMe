using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UpdateMe.Data.Models.DataModels
{
    public class AssignmentFormViewModel
    {
        public List<CourseViewModel> CourseViewModels { get; set; }

        public List<UserViewModelTwo> UserViewModelsTwo { get; set; }

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
