using System;
using System.ComponentModel.DataAnnotations;

namespace eStudent.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int YearOfStudy { get; set; }
        [Required]
        public DateTime Date { get; set; } 
        [Required]
        public bool Accepted { get; set; }
    }
}
