using System.ComponentModel.DataAnnotations;

namespace eStudent.DTO
{
    public class RequestUpdateDto
    {

        [Required]
        public int? CourseId { get; set; }
        [Required]
        public int? YearOfStudy { get; set; }
    }
}
