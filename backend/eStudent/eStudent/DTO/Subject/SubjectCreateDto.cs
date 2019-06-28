using System.ComponentModel.DataAnnotations;

namespace eStudent.DTO.Subject
{
    public class SubjectCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int? ECTSPoints { get; set; }
    }
}
