using System;
using System.Linq.Expressions;
using UpdateMe.Data.Models;

namespace UpdateMe.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsChecked { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string Department { get; set; }

        public static Expression<Func<ApplicationUser, UserViewModel>> Create
        {
            get
            {
                return u => new UserViewModel()
                {
                    Id = u.Id,            
                    Username = u.UserName, 
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Position = u.Position,
                    Department = u.Department
                };
            }
        }
    }
}