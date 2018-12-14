using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eStudent.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        public string OIB { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]

        public DateTime BirthDate { get; set; }
        [Required]
        public string Residence { get; set; }
        public virtual ICollection<UserRole> Roles { get; } = new List<UserRole>();
        public ICollection<UserCourse> UserCourses { get; set; }
        public ICollection<UserSubject> UserSubjects { get; set; }

    }
}
