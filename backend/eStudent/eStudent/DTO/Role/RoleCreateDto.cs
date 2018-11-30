using System.ComponentModel.DataAnnotations;

namespace eStudent.DTO.Role
{
    public class RoleCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
