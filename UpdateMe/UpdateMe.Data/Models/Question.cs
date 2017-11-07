using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace UpdateMe.Data.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string QuestionText { get; set; }

        [Required]
        public string AnswersInternal { get; set; }     //string in the database

        [NotMapped]
        public string[] AnswersExternal
        {
            get
            {
                return AnswersInternal.Split(';');
            }
            set
            {
                AnswersInternal = string.Join(";", value);  //directly send to db
            }
        }


        public int CorrectAnswerIndex { get; set; }

        public int CourseId { get; set; }           //navigational property

        public virtual Course Course { get; set; }  //connection to course                
    }
}