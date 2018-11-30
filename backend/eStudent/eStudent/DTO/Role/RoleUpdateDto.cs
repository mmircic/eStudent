using System.ComponentModel.DataAnnotations;

namespace eStudent.DTO
{
    public class RoleUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
