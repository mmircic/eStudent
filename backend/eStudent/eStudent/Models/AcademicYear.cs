using System.ComponentModel.DataAnnotations;

namespace eStudent.Models
{
    public class AcademicYear
    {
        public int Id { get; set; }
        [Required]
        public string Year { get; set; }
    }
}
