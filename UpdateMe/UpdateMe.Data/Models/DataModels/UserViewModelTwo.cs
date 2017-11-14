using System;
using System.Linq.Expressions;

namespace UpdateMe.Data.Models
{
    public class UserViewModelTwo
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsChecked { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public static Expression<Func<ApplicationUser, UserViewModelTwo>> Create
        {
            get
            {
                return u => new UserViewModelTwo()
                {
                    Id = u.Id,            
                    Username = u.UserName, 
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Position = u.Position,
                };
            }
        }
    }
}