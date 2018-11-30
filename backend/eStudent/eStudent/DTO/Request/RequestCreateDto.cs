using System.ComponentModel.DataAnnotations;

namespace eStudent.DTO.Request
{
    public class RequestCreateDto
    {
        [Required]
        public int? UserId { get; set; }
        [Required]
        public int? CourseId { get; set; }
        [Required]
        public int? YearOfStudy { get; set; }
    }
}
