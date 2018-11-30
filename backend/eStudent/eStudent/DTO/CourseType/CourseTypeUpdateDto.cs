using System.ComponentModel.DataAnnotations;

namespace eStudent.DTO
{
    public class CourseTypeUpdateDto
    {
        [Required]
        public string Type { get; set; }
    }
}
