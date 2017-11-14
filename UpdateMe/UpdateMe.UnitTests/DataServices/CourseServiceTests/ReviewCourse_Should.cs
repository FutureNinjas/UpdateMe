using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateMe.Data;
using UpdateMe.Data.Models;

namespace UpdateMe.UnitTests.DataServices.CourseServiceTests
{
    [TestClass]
    public class ReviewCourse_Should
    {
        [TestMethod]

        public void ReturnPropperDataWhenPropperIdIsPassed()
        {
            //Arrange
            string name = "ValidateStringTestName";
            string description = "ValidateStringTestDescription";
            int id = 1;
            int passScore = 99;
            

            //var contextMock = new Mock<UpdateMeDbContext>();
            //var course = new CourseModel() { Id = id, Name = name, Description = description, PassScore = passScore };
            //var courseViewModel = new CourseViewModel() { Id = id, Name = nameMock, Description = descriptionMock, PassScore = passScoreMock };

            //List<Course> courses = new List<Course>() { course };
        }
    }
}
