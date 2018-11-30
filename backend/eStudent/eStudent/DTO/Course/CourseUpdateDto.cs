using System.ComponentModel.DataAnnotations;

namespace eStudent.DTO
{
    public class CourseUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int? CourseTypeId { get; set; }
    }
}
