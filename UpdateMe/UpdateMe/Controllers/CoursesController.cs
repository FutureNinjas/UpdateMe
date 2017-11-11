﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UpdateMe.Data;
using UpdateMe.Models;
using UpdateMe.Services.Contracts;

namespace UpdateMe.Controllers
{
    public class CoursesController : Controller
    {
        // GET: MyCourses
        private readonly ApplicationUserManager userManager;
        private readonly UpdateMeDbContext dbContext;
        private readonly IAssignmentService assignmentService;
        private readonly ICourseService courseService;

        public CoursesController(
            ApplicationUserManager userManager, 
            UpdateMeDbContext dbContext, 
            IAssignmentService assignmentService,
            ICourseService courseService)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.assignmentService = assignmentService;
            this.courseService = courseService;
        }
        
        public  ActionResult ListUserCourses()
        {        
            
            var currentUserId = this.User.Identity.GetUserId();

            var allAssignments = assignmentService.ListAllAssignmentsFromUser(currentUserId);

            var assignmentViewModels = allAssignments.Select(a => AssignmentViewModel.Create.Compile()(a)).ToList();
            
            return View(assignmentViewModels);
        }

        public ActionResult ReviewCourse(int id)
        {
            var courseModel = courseService.ReviewCourse(id);

            return this.View(courseModel);
        }
    }
}