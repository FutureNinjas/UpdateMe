using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using UpdateMe.Data.Models;

namespace UpdateMe.Data
{
    public class UpdateMeDbContext : IdentityDbContext<ApplicationUser>
    {
        public UpdateMeDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Course> Courses { get; set; }

        public virtual IDbSet<Assignment> Assignments { get; set; }

        public static UpdateMeDbContext Create()
        {
            return new UpdateMeDbContext();
        }
    }
}
