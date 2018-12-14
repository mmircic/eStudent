using eStudent.EntityConfig;
using eStudent.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eStudent
{
    public class DatabaseContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);




            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new SubjectConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new CourseTypeConfig());
            modelBuilder.ApplyConfiguration(new UserRoleConfig());
            modelBuilder.ApplyConfiguration(new UserCourseConfig());
            modelBuilder.ApplyConfiguration(new UserSubjectConfig());
            modelBuilder.ApplyConfiguration(new SubjectCourseConfig());

        }


        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<SubjectCourse> SubjectCourses { get; set; }
        public DbSet<SubjectCourse> UserSubjects { get; set; }
    }
}
