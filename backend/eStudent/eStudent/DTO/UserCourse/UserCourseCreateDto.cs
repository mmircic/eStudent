using System.ComponentModel.DataAnnotations;

namespace eStudent.DTO.Request
{
    public class UserCourseCreateDto
    {
        [Required]
        public int? UserId { get; set; }
        [Required]
        public int? CourseId { get; set; }
    }
}
