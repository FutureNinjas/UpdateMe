﻿using System.Collections.Generic;
using UpdateMe.Data.Models;

namespace UpdateMe.Services.Contracts
{
    public interface ICourseService
    {
        void AddCourse(Course course);

        Course FindCourse(int courseId);

        IEnumerable<Course> ListAllCourses();

        void EditCourse(Course course, string name, string description, int passScore);

        void DeleteCourse(Course course);
    }
}
