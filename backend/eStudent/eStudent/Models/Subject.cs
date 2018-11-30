using System.ComponentModel.DataAnnotations;

namespace eStudent.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ECTSPoints { get; set; }
        [Required]
        public int YearOfStudy { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
