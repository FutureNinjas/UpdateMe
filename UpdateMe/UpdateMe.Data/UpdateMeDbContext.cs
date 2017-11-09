using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using UpdateMe.Data.Migrations;
using UpdateMe.Data.Models;

namespace UpdateMe.Data
{
    public class UpdateMeDbContext : IdentityDbContext<ApplicationUser>
    {
        public UpdateMeDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            var strategy = new MigrateDatabaseToLatestVersion<UpdateMeDbContext, Configuration>();
            Database.SetInitializer(strategy);
        }

        public virtual IDbSet<Course> Courses { get; set; }

        public virtual IDbSet<Assignment> Assignments { get; set; }

        public static UpdateMeDbContext Create()
        {
            return new UpdateMeDbContext();
        }
    }
}
