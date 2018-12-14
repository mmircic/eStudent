using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eStudent.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ECTSPoints { get; set; }
        public ICollection<SubjectCourse> SubjectCourses { get; set; }
        public ICollection<UserSubject> UserSubjects { get; set; }
    }
}
