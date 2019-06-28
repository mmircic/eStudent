using System;

namespace eStudent.DTO.User
{
    public class StudentGetDto
    {
        public int Id { get; set; }
        public string OIB { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Residence { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public RoleGetDto Role { get; set; }
    }
}
