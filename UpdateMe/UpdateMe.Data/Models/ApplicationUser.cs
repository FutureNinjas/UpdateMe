using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UpdateMe.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<Assignment> assignments;

        public ApplicationUser()
        {
            this.Assignments = new HashSet<Assignment>();
        }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(20)]
        public string Position { get; set; }

        [MaxLength(20)]
        public string Department { get; set; }

        public bool IsAdmin { get; set; }

        public virtual ICollection<Assignment> Assignments
        {
            get
            {
                return this.assignments;
            }
            set
            {
                this.assignments = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
