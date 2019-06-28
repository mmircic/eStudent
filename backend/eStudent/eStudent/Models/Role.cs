
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace eStudent.Models
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> Roles { get; set; }
    }
}
