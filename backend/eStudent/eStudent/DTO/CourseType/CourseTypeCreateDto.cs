using System.ComponentModel.DataAnnotations;

namespace eStudent.DTO.CourseType
{
    public class CourseTypeCreateDto
    {
        [Required]
        public string Type { get; set; }
    }
}
