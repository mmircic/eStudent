using eStudent.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStudent.EntityConfig
{
    public class UserCourseConfig : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.HasOne(uc => uc.User)
                .WithMany(uc => uc.UserCourses)
                .HasForeignKey(uc => uc.UserId);

            builder.HasOne(uc => uc.Course)
                .WithMany(uc => uc.UserCourses)
                .HasForeignKey(uc => uc.CourseId);
        }
    }
}
