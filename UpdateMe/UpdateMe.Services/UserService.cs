using System.Collections.Generic;
using System.Linq;
using UpdateMe.Data;
using UpdateMe.Data.Models;
using UpdateMe.Services.Contracts;

namespace UpdateMe.Services
{
    public class UserService : IUserService
    {
        private readonly UpdateMeDbContext dbContext;

        public UserService(UpdateMeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<ApplicationUser> ListAllUsers()
        {
            var users = dbContext.Users.ToList();

            return users;
        }

    }
}
