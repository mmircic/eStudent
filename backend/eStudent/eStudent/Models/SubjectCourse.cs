namespace eStudent.Models
{
    public class SubjectCourse
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
