using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateMe.Data.Models
{
    public class User
    {
        public User()
        {
            //this.Assignements = new HashSet<Assignement>();
        }

        public int Id { get; set; }

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

        [Required]
        public bool IsAdmin { get; set; }

        //public virtual ICollection<Assignement> Assignements { get; set; }
    }
}
