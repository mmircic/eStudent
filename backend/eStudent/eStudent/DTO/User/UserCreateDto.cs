﻿using System;
using System.ComponentModel.DataAnnotations;

namespace eStudent.DTO.User
{
    public class UserCreateDto
    {
        [Required]
        public string OIB { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        public string Residence { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
