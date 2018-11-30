using System;

namespace eStudent.DTO
{
    public class RequestGetDto
    {
        public int Id { get; set; }
        public UserGetDto User { get; set; }
        public CourseGetDto Course { get; set; }
        public int YearOfStudy { get; set; }
        public DateTime Date { get; set; }
        public bool Accepted { get; set; }
    }
}
