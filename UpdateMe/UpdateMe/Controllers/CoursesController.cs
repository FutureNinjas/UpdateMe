using Microsoft.AspNet.Identity;
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
        private readonly IAssignmentService assignmentServices;

        public CoursesController(ApplicationUserManager userManager, UpdateMeDbContext dbContext, IAssignmentService assignmentServices)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.assignmentServices = assignmentServices;
        }
        
        public  ActionResult ListUserCourses()
        {
            //1. var userAssignments = ListAllAssignmentsFromUser(string id)
            //2.

            var currentUserId = this.User.Identity.GetUserId();

            var allAssignments = assignmentServices.ListAllAssignmentsFromUser(currentUserId);

            var assignmentViewModels = allAssignments.Select(a => AssignmentViewModel.Create.Compile()(a)).ToList();

            //foreach(var a in allAssignments)
            //{
            //    var courseViewModel = AssignmentViewModel.Create.Compile()(a);
            //}


            return View(assignmentViewModels);
        }
    }
}