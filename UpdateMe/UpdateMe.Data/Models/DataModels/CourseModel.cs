using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UpdateMe.Data.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string  Description { get; set; }
        
        public int PassScore { get; set; }
        
        public DateTime DateCreated { get; set; }
    }
}
