using System.ComponentModel.DataAnnotations;

namespace eStudent.Models
{
    public class CourseType
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
