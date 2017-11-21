using System.Collections.Generic;
using UpdateMe.Data.Models;

namespace UpdateMe.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<ApplicationUser> ListAllUsers();
    }
}
