using Microsoft.AspNet.Identity.EntityFramework;
using UpdateMe.Data.Models;

namespace UpdateMe.Data
{
    public class UpdateMeDbContext : IdentityDbContext<ApplicationUser>
    {
        public UpdateMeDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static UpdateMeDbContext Create()
        {
            return new UpdateMeDbContext();
        }
    }
}
