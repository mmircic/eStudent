using System.ComponentModel.DataAnnotations;

namespace eStudent.DTO
{
    public class UserCourseUpdateDto
    {

        [Required]
        public bool Accepted { get; set; }

    }
}
