using System;
using System.Collections.Generic;

namespace UpdateMe.Data.Models
{
    public class Course
    {
        private ICollection<Slide> slides;
        private ICollection<Question> questions;
         
        public Course()
        {
            this.slides = new HashSet<Slide>();
            this.questions = new HashSet<Question>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string  Description { get; set; }

        public int PassScore { get; set; }
        
        public DateTime DateCreated { get; set; }

        //public virtual ICollection<Slide> Slides
        //{
        //    get
        //    {
        //        return this.slides;
        //    }
        //    set
        //    {
        //        this.slides = value;
        //    }
        //}
        //public virtual ICollection<Question> Questions
        //{
        //    get
        //    {
        //        return this.questions;
        //    }
        //    set
        //    {
        //        this.questions = value;
        //    }
        //}

    }
}
