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
        }

        public virtual IDbSet<Course> Courses { get; set; }

        public virtual IDbSet<Assignment> Assignments { get; set; }

        public virtual IDbSet<Slide> Slides { get; set; }

        public virtual IDbSet<Question> Questions { get; set; }

        public virtual IDbSet<CurrentQuizState> QuizesCurrentState { get; set; }

        public static UpdateMeDbContext Create()
        {
            return new UpdateMeDbContext();
        }
    }
}
