using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eStudent.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CourseTypeId { get; set; }
        public CourseType CourseType { get; set; }
        public ICollection<SubjectCourse> SubjectCourses { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
    }
}
