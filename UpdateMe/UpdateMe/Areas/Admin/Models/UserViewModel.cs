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

        //static because we don't need to decouple from it - this is used for unit testing
        public static Expression<Func<ApplicationUser, UserViewModel>> Create
        {
            get
            {
                return u => new UserViewModel()
                {
                    Id = u.Id,            //vzimame dve koloni ot tablicata -Id
                    Username = u.UserName // i username
                };
            }
        }
    }
}