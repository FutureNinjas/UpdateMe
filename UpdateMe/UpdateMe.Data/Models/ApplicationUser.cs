using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UpdateMe.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            //this.Assignements = new HashSet<Assignement>();
        }


        //[Required]
        //[MaxLength(20)]
        //public string FirstName { get; set; }

        //[Required]
        //[MaxLength(20)]
        //public string LastName { get; set; }

        //[MaxLength(20)]
        //public string Position { get; set; }

        //[MaxLength(20)]
        //public string Department { get; set; }

        //[Required]
        //public bool IsAdmin { get; set; }

        //public virtual ICollection<Assignement> Assignements { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
