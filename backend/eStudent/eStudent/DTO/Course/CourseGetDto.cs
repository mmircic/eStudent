namespace eStudent.DTO
{
    public class CourseGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CourseTypeGetDto CourseType { get; set; }
    }
}
