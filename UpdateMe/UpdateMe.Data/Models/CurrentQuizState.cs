using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateMe.Data.Models
{
    public class CurrentQuizState
    {
        [Key, ForeignKey("Assignment")]
        public int AssignmentId { get; set; }

        public int CurrentUserResult { get; set; }

        public int CurrentQuestion { get; set; }
        
        public virtual Assignment Assignment { get; set; }
    }
}
