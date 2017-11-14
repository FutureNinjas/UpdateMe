namespace UpdateMe.Data.Models
{
    public class Slide
    {
        public int Id { get; set; }

        public string Base64Img { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
